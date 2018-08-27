using System;

namespace Mx.Hyperway.As2.Inbound
{
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Security.Cryptography;

    using log4net;

    using Microsoft.AspNetCore.Http;

    using MimeKit;
    using MimeKit.Cryptography;

    using Mx.Hyperway.Api.Model;
    using Mx.Hyperway.Api.Persist;
    using Mx.Hyperway.Api.Timestamp;
    using Mx.Hyperway.Api.Transmission;
    using Mx.Hyperway.As2.Code;
    using Mx.Hyperway.As2.Model;
    using Mx.Hyperway.As2.Util;
    using Mx.Hyperway.Commons.IO;
    using Mx.Peppol.Common.Code;
    using Mx.Peppol.Common.Model;
    using Mx.Peppol.Sbdh;
    using Mx.Peppol.Security.Api;
    using Mx.Tools;

    /// <summary>
    /// Main entry point for receiving AS2 messages
    /// </summary>
    public class As2InboundHandler
    {

        public static readonly ILog Logger = LogManager.GetLogger(typeof(As2InboundHandler));

        private readonly ITimestampProvider timestampProvider;

        private readonly IPersisterHandler persisterHandler;

        private readonly ITransmissionVerifier transmissionVerifier;

        private readonly CertificateValidator certificateValidator;

        private readonly SMimeMessageFactory sMimeMessageFactory;

        private readonly Func<HyperwaySecureMimeContext> secureContextFactory;

        public As2InboundHandler(
            ITimestampProvider timestampProvider,
            CertificateValidator certificateValidator,
            IPersisterHandler persisterHandler,
            ITransmissionVerifier transmissionVerifier,
            SMimeMessageFactory sMimeMessageFactory,
            Func<HyperwaySecureMimeContext> secureContextFactory)
        {
            this.timestampProvider = timestampProvider;
            this.certificateValidator = certificateValidator;

            this.persisterHandler = persisterHandler;
            this.transmissionVerifier = transmissionVerifier;

            this.sMimeMessageFactory = sMimeMessageFactory;
            this.secureContextFactory = secureContextFactory;
        }

        /// <summary>
        /// Receives an AS2 Message in the form of a map of headers together with the payload,
        /// which is made available in an input stream
        /// <p>If persisting message to the Message Repository fails, we have to return negative MDN.</p>
        /// </summary>
        /// <param name="httpHeaders">the http headers received</param>
        /// <param name="mimeMessage">supplies the MIME message</param>
        /// <returns>MDN object to signal if everything is ok or if some error occurred while receiving</returns>
        public MimeMessage Receive(IHeaderDictionary httpHeaders, MimeMessage mimeMessage)
        {
            Logger.Debug("Receiving message ..");

            SMimeReader sMimeReader = new SMimeReader(mimeMessage);

            // Get timestamp using signature as input
            Timestamp t2 = this.timestampProvider.Generate(sMimeReader.GetSignature(), Direction.IN);

            // Initiate MDN
            MdnBuilder mdnBuilder = MdnBuilder.NewInstance(mimeMessage);
            mdnBuilder.AddHeader(MdnHeader.Date, t2.GetDate());


            // Extract Message-ID
            TransmissionIdentifier transmissionIdentifier =
                TransmissionIdentifier.FromHeader(httpHeaders[As2Header.MessageId]);
            mdnBuilder.AddHeader(MdnHeader.OriginalMessageId, httpHeaders[As2Header.MessageId]);


            // Extract signed digest and digest algorithm
            SMimeDigestMethod digestMethod = sMimeReader.GetDigestMethod();

            // Extract content headers
            byte[] headerBytes = sMimeReader.GetBodyHeader();
            Stream bodyStream = sMimeReader.GetBodyInputStream();
            byte[] bodyBytes = bodyStream.ToBuffer();

            mdnBuilder.AddHeader(MdnHeader.OriginalContentHeader, headerBytes);


            // Extract SBDH
            Mx.Peppol.Common.Model.Header header;
            bodyStream.Seek(0, SeekOrigin.Begin);
            using (var sbdReader = SbdReader.newInstance(bodyStream))
            {
                header = sbdReader.getHeader();

                // Perform validation of SBDH
                this.transmissionVerifier.Verify(header, Direction.IN);

                // Extract "fresh" InputStream
                using (Stream payloadInputStream = sMimeReader.GetBodyInputStream())
                {
                    // Persist content
                    this.persisterHandler.Persist(
                        transmissionIdentifier,
                        header,
                        new UnclosableInputStream(payloadInputStream));
                }

                // Fetch calculated digest
                var s = SHA1.Create();
                var hash = s.ComputeHash(headerBytes.Concat(bodyBytes).ToArray());
                Digest calculatedDigest = Digest.of(DigestMethod.SHA1, hash);
                mdnBuilder.AddHeader(MdnHeader.ReceivedContentMic, new Mic(calculatedDigest));

                var check = this.VerifySignature(mimeMessage.Body as MultipartSigned, out var signatures);
                if (!check || signatures.Count != 1)
                {
                    throw new NotSupportedException("Firma non valida");
                }

                var signature = signatures[0];
                var certificate = signature.SignerCertificate as SecureMimeDigitalCertificate;
                Debug.Assert(certificate != null, nameof(certificate) + " != null");
                this.certificateValidator.validate(Service.AP, certificate.Certificate);

                // Create receipt (MDN)
                mdnBuilder.AddHeader(MdnHeader.Disposition, Disposition.Processed);
                MimeMessage mdn = this.sMimeMessageFactory.CreateSignedMimeMessage(mdnBuilder.Build(), digestMethod);
                mdn.Headers.Add(As2Header.As2Version, As2Header.Version);
                mdn.Headers.Add(As2Header.As2From, httpHeaders[As2Header.As2To]);
                mdn.Headers.Add(As2Header.As2To, httpHeaders[As2Header.As2From]);

                return mdn;
            }
        }


        public bool VerifySignature(MultipartSigned multipartSigned, out DigitalSignatureCollection signatures)
        {
            var signed = multipartSigned;
            signatures = null;
            var valid = true;
            if (signed != null)
            {
                using (var ctx = this.secureContextFactory())
                {
                    signatures = signed.Verify(ctx);
                    foreach (var signature in signatures)
                    {
                        valid = signature.Verify();
                        if (!valid)
                        {
                            break;
                        }
                    }
                }
            }
            return valid;
        }
    }
}
