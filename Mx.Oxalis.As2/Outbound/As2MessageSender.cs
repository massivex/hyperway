namespace Mx.Hyperway.As2.Outbound
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;

    using log4net;

    using MimeKit;

    using Mx.Hyperway.Api.Lang;
    using Mx.Hyperway.Api.Model;
    using Mx.Hyperway.Api.Outbound;
    using Mx.Hyperway.Api.Timestamp;
    using Mx.Hyperway.As2.Code;
    using Mx.Hyperway.As2.Model;
    using Mx.Hyperway.As2.Util;
    using Mx.Hyperway.Commons.BouncyCastle;
    using Mx.Hyperway.Commons.Security;
    using Mx.Peppol.Common.Model;
    using Mx.Tools;
    using Mx.Tools.Http;

    using Org.BouncyCastle.X509;

    using zipkin4net;

    public class As2MessageSender : MessageSender
    {

        private static readonly ILog LOGGER = LogManager.GetLogger(typeof(As2MessageSender));


        private readonly SMimeMessageFactory sMimeMessageFactory;

        /**
         * Timestamp provider used to create timestamp "t3" (time of reception of transport specific receipt, MDN).
         */
        private readonly TimestampProvider timestampProvider;

        /**
         * Identifier from sender's certificate used during transmission in "AS2-From" header.
         */
        private readonly String fromIdentifier;

        private TransmissionRequest transmissionRequest;

        private TransmissionIdentifier transmissionIdentifier;

        private Trace root;

        private Digest outboundMic;

        /**
         * Constructor expecting resources needed to perform transmission using AS2. All task required to be done once for
         * all requests using this instance is done here.
         *
         * @param httpClientProvider  Provider of HTTP clients.
         * @param certificate         Certificate of sender.
         * @param sMimeMessageFactory Factory prepared to create S/MIME messages using our private key.
         * @param timestampProvider   Provider used to fetch timestamps.
         * @param tracer              Tracing tool.
         */
        // @Inject
        public As2MessageSender(
            X509Certificate certificate,
            SMimeMessageFactory sMimeMessageFactory,
            TimestampProvider timestampProvider)
        {
            this.sMimeMessageFactory = sMimeMessageFactory;
            this.timestampProvider = timestampProvider;

            // Establishes our AS2 System Identifier based upon the contents of the CN= field of the certificate
            this.fromIdentifier = CertificateUtils.extractCommonName(certificate);
        }

        public TransmissionResponse send(TransmissionRequest transmissionRequest)
        {
            return this.send(transmissionRequest, this.root);
        }

        public TransmissionResponse send(TransmissionRequest transmissionRequest, Trace root)
        {
            this.transmissionRequest = transmissionRequest;

            this.root = root.Child();
            this.root.Record(Annotations.ServiceName("Send AS2 message"));
            this.root.Record(Annotations.ClientSend());
            try
            {
                return this.sendHttpRequest(this.prepareHttpRequest());
            }
            catch (HyperwayTransmissionException e)
            {
                this.root.Record(Annotations.Tag("exception", e.Message));
                throw e;
            }
            finally
            {
                this.root.Record(Annotations.ClientRecv());
            }
        }

        protected HttpPost prepareHttpRequest()
        {
            Trace span = this.root.Child();
            span.Record(Annotations.ServiceName("request"));
            span.Record(Annotations.ClientSend());
            // tracer.newChild(root.context()).name("request").start();
            try
            {
                HttpPost httpPost;

                // Create the body part of the MIME message containing our content to be transmitted.
                MimeEntity mimeBodyPart = MimeMessageHelper.createMimeBodyPart(
                    this.transmissionRequest.getPayload(),
                    "application/xml");

                // Digest method to use.
                SMimeDigestMethod digestMethod =
                    SMimeDigestMethod.findByTransportProfile(
                        this.transmissionRequest.getEndpoint().getTransportProfile());
                var dataTemp = this.transmissionRequest.getPayload().ToBuffer();
                
                this.outboundMic = MimeMessageHelper.calculateMic(mimeBodyPart, digestMethod);
                span.Record(Annotations.Tag("mic", this.outboundMic.ToString()));
                span.Record(
                    Annotations.Tag("endpoint url", this.transmissionRequest.getEndpoint().getAddress().ToString()));

                // Create a complete S/MIME message using the body part containing our content as the
                // signed part of the S/MIME message.
                MimeMessage signedMimeMessage =
                    this.sMimeMessageFactory.createSignedMimeMessage(mimeBodyPart, digestMethod);
                

                // Initiate POST request
                httpPost = new HttpPost(this.transmissionRequest.getEndpoint().getAddress());

                // Get all headers in S/MIME message.
                // var headers = signedMimeMessage.Headers;
                // List<javax.mail.Header> headers = Collections.list(signedMimeMessage.getAllHeaders());


                List<String> headerNames = signedMimeMessage.Headers
                    // Tag for tracing.
                    .Peek(
                        x => span.Record(
                            Annotations.Tag(x.Field, x.Value))) // span.tag(h.getName(), h.getValue()))
                    // Add headers to httpPost object (remove new lines according to HTTP 1.1).
                    .Peek(x => httpPost.AddHeader(x.Field, x.Value.Replace("\r\n\t", string.Empty)))
                    // Collect header names....
                    .Map(x => x.Field)
                    // ... in a list.
                    .ToList();

                signedMimeMessage.Headers.Clear();
                
                this.transmissionIdentifier = TransmissionIdentifier.fromHeader(httpPost.Headers[As2Header.MESSAGE_ID]);


                // Write content to OutputStream without headers.
                using (var m = new MemoryStream())
                {
                    signedMimeMessage.WriteTo(m);
                    httpPost.Entity = m.ToBuffer();
                }

                // signedMimeMessage.writeTo(byteArrayOutputStream, headerNames.toArray(new String[headerNames.size()]));


                // Inserts the S/MIME message to be posted. Make sure we pass the same content type as the
                // SignedMimeMessage, it'll end up as content-type HTTP header

                // setEntity(new ByteArrayEntity(m.ToBuffer()));

                // Set all headers specific to AS2 (not MIME).
                httpPost.Host = "skynet.sediva.it";
                httpPost.Headers.Add(As2Header.AS2_FROM, this.fromIdentifier);
                httpPost.Headers.Add(
                    As2Header.AS2_TO,
                    CertificateUtils.extractCommonName(this.transmissionRequest.getEndpoint().getCertificate()));
                httpPost.Headers.Add(As2Header.DISPOSITION_NOTIFICATION_TO, "not.in.use@difi.no");
                httpPost.Headers.Add(
                    As2Header.DISPOSITION_NOTIFICATION_OPTIONS,
                    As2DispositionNotificationOptions.getDefault(digestMethod).toString());
                httpPost.Headers.Add(As2Header.AS2_VERSION, As2Header.VERSION);
                httpPost.Headers.Add(As2Header.SUBJECT, "AS2 message from HYPERWAY");
                httpPost.Headers.Add(As2Header.DATE, As2DateUtil.RFC822.getFormat(DateTime.Now));
                return httpPost;
            }
            catch (Exception e)
            {
                throw new HyperwayTransmissionException("Unable to stream S/MIME message into byte array output stream");
            }
            finally
            {
                span.Record(Annotations.ClientRecv());
            }
        }

        protected TransmissionResponse sendHttpRequest(HttpPost httpPost)
        {
            Trace span = this.root.Child();
            span.Record(Annotations.ServiceName("execute"));

            try
            {
                var httpClient = new HttpClient();
                var response = httpClient.Execute(httpPost);


                var result = this.handleResponse(response);
                span.Record(Annotations.ClientRecv());
                return result;
            }
            catch (Exception e)
            {
                span.Record(Annotations.Tag("exception", e.Message));
                throw new HyperwayTransmissionException(this.transmissionRequest.getEndpoint().getAddress(), e);
            }
            finally
            {
                span.Record(Annotations.ClientRecv());
            }
        }

        protected TransmissionResponse handleResponse(HttpResponse httpResponse)
        {
            Trace span = this.root.Child();
            // tracer.newChild(root.context()).name("response").start();
            span.Record(Annotations.ServiceName("response"));
            span.Record(Annotations.ClientSend());

            try
            {
                HttpResponse response = httpResponse;
                span.Record(Annotations.Tag("code", response.StatusCode.ToString()));

                // span.tag("code", String.valueOf(response.getStatusLine().getStatusCode()));

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    LOGGER.ErrorFormat(
                        "AS2 HTTP POST expected HTTP OK, but got : {0} from {1}",
                        response.StatusCode,
                        this.transmissionRequest.getEndpoint().getAddress());

                    // Throws exception
                    this.handleFailedRequest(response);
                }

                // handle normal HTTP OK response
                LOGGER.DebugFormat(
                    "AS2 transmission to {0} returned HTTP OK, verify MDN response",
                    this.transmissionRequest.getEndpoint().getAddress());

                string contentTypeHeader = response.Headers["Content-Type"];
                if (string.IsNullOrWhiteSpace(contentTypeHeader))
                {
                    throw new HyperwayTransmissionException(
                        "No Content-Type header in response, probably a server error.");
                }

                // Read MIME Message
                MimeMessage mimeMessage = MimeMessageHelper.parseMultipart(
                    response.Entity.Content.ToStream(),
                    contentTypeHeader);

                // Add headers to MIME Message
                foreach (var headerName in response.Headers.AllKeys)
                {
                    mimeMessage.Headers.Add(headerName, response.Headers[headerName]);
                }

                SMimeReader sMimeReader = new SMimeReader(mimeMessage);

                // Timestamp of reception of MDN
                Timestamp t3 = this.timestampProvider.generate(sMimeReader.getSignature(), Direction.OUT);

                // Extract signed digest and digest algorithm
                SMimeDigestMethod digestMethod = sMimeReader.getDigestMethod();

                // Preparing calculation of digest
                byte[] digest = BcHelper.Hash(sMimeReader.getBody(), digestMethod.getAlgorithm());
                //MessageDigest messageDigest = BcHelper.getMessageDigest(digestMethod.getIdentifier());
                //InputStream digestInputStream = new DigestInputStream(sMimeReader.getBodyInputStream(), messageDigest);

                // Reading report
                Multipart mimeMultipart = new Multipart();
                mimeMultipart.Add(MimeEntity.Load(digest.ToStream()));
                // mimeMultipart.SetContentType(mimeMessage.GetContentType());
                // mimeMultipart.LoadBody(digest, Encoding.ASCII);


                // new ByteArrayDataSource(digestInputStream, mimeMessage.getContentType()));

                // Create digest object
                // Digest digest = Digest.of(digestMethod.getDigestMethod(), messageDigest.digest());

                // Verify signature
                /*
                X509Certificate certificate = SMimeBC.verifySignature(
                        ImmutableMap.of(digestMethod.getOid(), digest.getValue()),
                        sMimeReader.getSignature()
                );
                */

                // verify the signature of the MDN, we warn about dodgy signatures
                SignedMimeMessage signedMimeMessage = new SignedMimeMessage(mimeMessage);
                X509Certificate certificate = signedMimeMessage.getSignersX509Certificate();

                // Verify if the certificate used by the receiving Access Point in
                // the response message does not match its certificate published by the SMP
                if (!this.transmissionRequest.getEndpoint().getCertificate().Equals(certificate))
                {
                    throw new HyperwayTransmissionException(
                        String.Format(
                            "Certificate in MDN ('{0}') does not match certificate from SMP ('{1}').",
                            certificate.SubjectDN.ToString(), // .getSubjectX500Principal().getName(),
                            this.transmissionRequest.getEndpoint().getCertificate()
                                .SubjectDN)); // .getSubjectX500Principal().getName()));
                }

                LOGGER.Debug("MDN signature was verified for : " + certificate.SubjectDN);

                // Verifies the actual MDN
                MdnMimeMessageInspector mdnMimeMessageInspector = new MdnMimeMessageInspector(mimeMessage);
                String msg = mdnMimeMessageInspector.getPlainTextPartAsText();

                if (!mdnMimeMessageInspector.isOkOrWarning(new Mic(this.outboundMic)))
                {
                    LOGGER.ErrorFormat("AS2 transmission failed with some error message '{0}'.", msg);
                    throw new HyperwayTransmissionException(String.Format("AS2 transmission failed : {0}", msg));
                }

                // Read structured content
                MimeEntity mimeBodyPart = mdnMimeMessageInspector.getMessageDispositionNotificationPart();
                var internetHeaders = mimeBodyPart.Headers;
                // InternetHeaders internetHeaders = new InternetHeaders((InputStream)mimeBodyPart.getContent());

                // Fetch timestamp if set
                DateTime date = t3.getDate();
                if (internetHeaders.Any(x => x.Field == MdnHeader.DATE))
                {
                    var dateText = internetHeaders.First(x => x.Field == MdnHeader.DATE).Value;
                    date = As2DateUtil.RFC822.parse(dateText);
                }


                // Return TransmissionResponse
                return new As2TransmissionResponse(
                    this.transmissionIdentifier,
                    this.transmissionRequest,
                    this.outboundMic,
                    MimeMessageHelper.toBytes(mimeMessage),
                    t3,
                    date);
            }
            catch (TimestampException e)
            {
                throw new HyperwayTransmissionException(e.Message, e);
            }
            catch (Exception e)
            {
                throw new HyperwayTransmissionException("Unable to parse received content.", e);
            }
            finally
            {
                span.Record(Annotations.ClientRecv());

            }
        }

        protected void handleFailedRequest(HttpResponse response)
        {
            byte[] entity = response.Entity.Content; // Any results?
            try
            {
                if (entity == null)
                {
                    // No content returned
                    throw new HyperwayTransmissionException(
                        String.Format(
                            "Request failed with rc={0}, no content returned in HTTP response",
                            response.StatusCode));
                }
                else
                {
                    String contents = response.Entity.GetText();
                    throw new HyperwayTransmissionException(
                        String.Format(
                            "Request failed with rc={0}, contents received ({1} characters): {2}",
                            response.StatusCode,
                            contents.Trim().Length,
                            contents));
                }
            }
            catch (IOException e)
            {
                throw new HyperwayTransmissionException(
                    String.Format(
                        "Request failed with rc={0}, ERROR while retrieving the contents of the response: {1}",
                        response.StatusCode,
                        e.Message),
                    e);
            }
        }
    }
}
