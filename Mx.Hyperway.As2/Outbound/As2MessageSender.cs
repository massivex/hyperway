namespace Mx.Hyperway.As2.Outbound
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Text.RegularExpressions;

    using log4net;

    using MimeKit;
    using MimeKit.Cryptography;

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

    using Trace = zipkin4net.Trace;

    public class As2MessageSender : IMessageSender
    {

        private static readonly ILog LOGGER = LogManager.GetLogger(typeof(As2MessageSender));


        private readonly SMimeMessageFactory sMimeMessageFactory;

        /**
         * Timestamp provider used to create timestamp "t3" (time of reception of transport specific receipt, MDN).
         */
        private readonly ITimestampProvider timestampProvider;

        private readonly Func<HyperwaySecureMimeContext> secureMimeContext;

        /**
         * Identifier from sender's certificate used during transmission in "AS2-From" header.
         */
        private readonly String fromIdentifier;

        private ITransmissionRequest transmissionRequest;

        private TransmissionIdentifier transmissionIdentifier;

        private Trace root;

        private Digest outboundMic;

        /**
         * Constructor expecting resources needed to perform transmission using AS2. All task required to be done once for
         * all requests using this instance is done here.
         */
        // @Inject
        public As2MessageSender(
            X509Certificate certificate,
            SMimeMessageFactory sMimeMessageFactory,
            ITimestampProvider timestampProvider,
            Func<HyperwaySecureMimeContext> secureMimeContext)
        {
            this.sMimeMessageFactory = sMimeMessageFactory;
            this.timestampProvider = timestampProvider;
            this.secureMimeContext = secureMimeContext;

            // Establishes our AS2 System Identifier based upon the contents of the CN= field of the certificate
            this.fromIdentifier = CertificateUtils.extractCommonName(certificate);
        }

        public ITransmissionResponse Send(ITransmissionRequest transmissionRequest)
        {
            return this.Send(transmissionRequest, this.root);
        }

        public ITransmissionResponse Send(ITransmissionRequest transmissionRequest, Trace root)
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
            try
            {
                HttpPost httpPost;

                // Create the body part of the MIME message containing our content to be transmitted.
                MimeEntity mimeBodyPart = MimeMessageHelper.createMimeBodyPart(
                    this.transmissionRequest.GetPayload(),
                    "application/xml");

                // Digest method to use.
                SMimeDigestMethod digestMethod =
                    SMimeDigestMethod.findByTransportProfile(
                        this.transmissionRequest.GetEndpoint().getTransportProfile());




                // Create a complete S/MIME message using the body part containing our content as the
                // signed part of the S/MIME message.
                MimeMessage signedMimeMessage =
                    this.sMimeMessageFactory.createSignedMimeMessage(mimeBodyPart, digestMethod);

                var signedMultipart = (signedMimeMessage.Body as MultipartSigned);
                Debug.Assert(signedMultipart != null, nameof(signedMultipart) + " != null");
                this.outboundMic = MimeMessageHelper.calculateMic(signedMultipart[0], digestMethod);
                span.Record(Annotations.Tag("mic", this.outboundMic.ToString()));
                span.Record(Annotations.Tag("endpoint url", this.transmissionRequest.GetEndpoint().getAddress().ToString()));

                // Initiate POST request
                httpPost = new HttpPost(this.transmissionRequest.GetEndpoint().getAddress());

                List<String> headerNames = signedMimeMessage.Headers
                    // Tag for tracing.
                    .Peek(x => span.Record(Annotations.Tag(x.Field, x.Value))) // span.tag(h.getName(), h.getValue()))
                    // Add headers to httpPost object (remove new lines according to HTTP 1.1).
                    .Peek(x => httpPost.AddHeader(x.Field, x.Value.Replace("\r\n\t", string.Empty))).Map(x => x.Field)
                    .ToList();

                signedMimeMessage.Headers.Clear();

                this.transmissionIdentifier = TransmissionIdentifier.FromHeader(httpPost.Headers[As2Header.MESSAGE_ID]);

                // Write content to OutputStream without headers.
                using (var m = new MemoryStream())
                {
                    signedMultipart.WriteTo(m);
                    httpPost.Entity = m.ToBuffer();
                }

                var contentType = signedMultipart.Headers[HeaderId.ContentType];

                // Set all headers specific to AS2 (not MIME).
                httpPost.Host = "skynet.sediva.it";
                httpPost.Headers.Add("Content-Type", this.NormalizeHeaderValue(contentType));
                httpPost.Headers.Add(As2Header.AS2_FROM, this.fromIdentifier);
                httpPost.Headers.Add(
                    As2Header.AS2_TO,
                    CertificateUtils.extractCommonName(this.transmissionRequest.GetEndpoint().getCertificate()));
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
                throw new HyperwayTransmissionException(
                    "Unable to stream S/MIME message into byte array output stream");
            }
            finally
            {
                span.Record(Annotations.ClientRecv());
            }
        }


        private string NormalizeHeaderValue(string value)
        {
            var result = Regex.Replace(value, @"[\t\r\n]", string.Empty);
            return result;
        }

        protected ITransmissionResponse sendHttpRequest(HttpPost httpPost)
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
                throw new HyperwayTransmissionException(this.transmissionRequest.GetEndpoint().getAddress(), e);
            }
            finally
            {
                span.Record(Annotations.ClientRecv());
            }
        }

        protected ITransmissionResponse handleResponse(HttpResponse httpResponse)
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
                        this.transmissionRequest.GetEndpoint().getAddress());

                    // Throws exception
                    this.handleFailedRequest(response);
                }

                // handle normal HTTP OK response
                LOGGER.DebugFormat(
                    "AS2 transmission to {0} returned HTTP OK, verify MDN response",
                    this.transmissionRequest.GetEndpoint().getAddress());

                string contentTypeHeader = response.Headers["Content-Type"];
                if (string.IsNullOrWhiteSpace(contentTypeHeader))
                {
                    throw new HyperwayTransmissionException(
                        "No Content-Type header in response, probably a server error.");
                }

                // Read MIME Message
                MimeMessage mimeMessage;
                SignedMimeMessage signedMimeMessage;
                using (var m = new MemoryStream())
                {
                    // Add headers to MIME Message
                    foreach (var headerName in response.Headers.AllKeys)
                    {
                        var headerText = $"{headerName}: {response.Headers[headerName]}";
                        var headerData = Encoding.ASCII.GetBytes(headerText);
                        m.Write(headerData, 0, headerData.Length);
                        m.Write(new byte[] { 13, 10 }, 0, 2);
                    }
                    m.Write(new byte[] { 13, 10 }, 0, 2);

                    var messageData = response.Entity.Content;
                    m.Write(messageData, 0, messageData.Length);


                    m.Seek(0, SeekOrigin.Begin);
                    mimeMessage = MimeMessage.Load(m);
                    mimeMessage.Headers[HeaderId.ContentType] = response.Headers["Content-Type"];
                    signedMimeMessage = new SignedMimeMessage(mimeMessage);
                }

                
                SMimeReader sMimeReader = new SMimeReader(mimeMessage);
                // Timestamp of reception of MDN
                Timestamp t3 = this.timestampProvider.Generate(sMimeReader.getSignature(), Direction.OUT);

                MultipartSigned signedMessage = mimeMessage.Body as MultipartSigned;
                using (var ctx = this.secureMimeContext())
                {
                    Debug.Assert(signedMessage != null, nameof(signedMessage) + " != null");

                    var signatures = signedMessage.Verify();
                    var signature = signatures.First();
                    var mimeCertificate = signature.SignerCertificate as SecureMimeDigitalCertificate;


                    // Verify if the certificate used by the receiving Access Point in
                    // the response message does not match its certificate published by the SMP
                    X509Certificate certificate = mimeCertificate.Certificate;
                    if (!this.transmissionRequest.GetEndpoint().getCertificate().Equals(certificate))
                    {
                        throw new HyperwayTransmissionException(
                            String.Format(
                                "Certificate in MDN ('{0}') does not match certificate from SMP ('{1}').",
                                certificate.SubjectDN.ToString(), // .getSubjectX500Principal().getName(),
                                this.transmissionRequest.GetEndpoint().getCertificate()
                                    .SubjectDN)); // .getSubjectX500Principal().getName()));
                    }

                    LOGGER.Debug("MDN signature was verified for : " + certificate.SubjectDN);
                }


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
                DateTime date = t3.GetDate();
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
