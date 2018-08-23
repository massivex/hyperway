using System;
using System.Collections.Generic;
using System.Text;

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
    using Mx.Hyperway.Api.Statistics;
    using Mx.Hyperway.Api.Timestamp;
    using Mx.Hyperway.Api.Transmission;
    using Mx.Hyperway.As2.Code;
    using Mx.Hyperway.As2.Model;
    using Mx.Hyperway.As2.Util;
    using Mx.Hyperway.Commons.BouncyCastle;
    using Mx.Hyperway.Commons.IO;
    using Mx.Peppol.Common.Code;
    using Mx.Peppol.Common.Model;
    using Mx.Peppol.Sbdh;
    using Mx.Peppol.Security.Api;
    using Mx.Tools;

    using Header = MimeKit.Header;

    /**
     * Main entry point for receiving AS2 messages.
     */
    public class As2InboundHandler
    {

        public static readonly ILog LOGGER = LogManager.GetLogger(typeof(As2InboundHandler));

        private readonly StatisticsService statisticsService;

        private readonly TimestampProvider timestampProvider;

        private readonly PersisterHandler persisterHandler;

        private readonly TransmissionVerifier transmissionVerifier;

        private readonly CertificateValidator certificateValidator;

        private readonly SMimeMessageFactory sMimeMessageFactory;

        private readonly Func<HyperwaySecureMimeContext> secureContextFactory;

        public As2InboundHandler(
            StatisticsService statisticsService,
            TimestampProvider timestampProvider,
            CertificateValidator certificateValidator,
            PersisterHandler persisterHandler,
            TransmissionVerifier transmissionVerifier,
            SMimeMessageFactory sMimeMessageFactory,
            Func<HyperwaySecureMimeContext> secureContextFactory)
        {
            this.statisticsService = statisticsService;
            this.timestampProvider = timestampProvider;
            this.certificateValidator = certificateValidator;

            this.persisterHandler = persisterHandler;
            this.transmissionVerifier = transmissionVerifier;

            this.sMimeMessageFactory = sMimeMessageFactory;
            this.secureContextFactory = secureContextFactory;
        }

        /**
         * Receives an AS2 Message in the form of a map of headers together with the payload,
         * which is made available in an input stream
         * <p>
         * If persisting message to the Message Repository fails, we have to return negative MDN.
         *
         * @param httpHeaders the http headers received
         * @param mimeMessage supplies the MIME message
         * @return MDN object to signal if everything is ok or if some error occurred while receiving
         */
        public MimeMessage receive(IHeaderDictionary httpHeaders, MimeMessage mimeMessage)
        {
            LOGGER.Debug("Receiving message ..");

            //try
            //{
            SMimeReader sMimeReader = new SMimeReader(mimeMessage);

            // Get timestamp using signature as input
            Timestamp t2 = timestampProvider.generate(sMimeReader.getSignature(), Direction.IN);

            // Initiate MDN
            MdnBuilder mdnBuilder = MdnBuilder.newInstance(mimeMessage);
            mdnBuilder.addHeader(MdnHeader.DATE, t2.getDate());


            // Extract Message-ID
            TransmissionIdentifier transmissionIdentifier =
                TransmissionIdentifier.fromHeader(httpHeaders[As2Header.MESSAGE_ID]);
            mdnBuilder.addHeader(MdnHeader.ORIGINAL_MESSAGE_ID, httpHeaders[As2Header.MESSAGE_ID]);


            // Extract signed digest and digest algorithm
            SMimeDigestMethod digestMethod = sMimeReader.getDigestMethod();


            // Extract content headers
            byte[] headerBytes = sMimeReader.getBodyHeader();
            Stream bodyStream = sMimeReader.getBodyInputStream();
            byte[] bodyBytes = bodyStream.ToBuffer();

            mdnBuilder.addHeader(MdnHeader.ORIGINAL_CONTENT_HEADER, headerBytes);

            using (var fs = File.Create("C:\\temp\\hyperway-input-mic-content.eml"))
            {
                fs.Write(headerBytes, 0, headerBytes.Length);
                fs.Write(bodyBytes, 0, bodyBytes.Length);
            }

            // Extract SBDH
            Mx.Peppol.Common.Model.Header header;
            bodyStream.Seek(0, SeekOrigin.Begin);
            using (var sbdReader = SbdReader.newInstance(bodyStream))
            {
                header = sbdReader.getHeader();

                // Perform validation of SBDH
                transmissionVerifier.verify(header, Direction.IN);

                // Extract "fresh" InputStream
                FileInfo payloadPath;
                using (Stream payloadInputStream = sMimeReader.getBodyInputStream())
                {
                    // Persist content
                    payloadPath = this.persisterHandler.persist(
                        transmissionIdentifier,
                        header,
                        new UnclosableInputStream(payloadInputStream));

                    // Exhaust InputStream
                    // ByteStreams.exhaust(payloadInputStream);
                }

                // Fetch calculated digest
                var s = SHA1.Create();
                var hash = s.ComputeHash(headerBytes.Concat(bodyBytes).ToArray());
                var hashText = BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();

                Digest calculatedDigest = Digest.of(DigestMethod.SHA1, hash);
                mdnBuilder.addHeader(MdnHeader.RECEIVED_CONTENT_MIC, new Mic(calculatedDigest));

                DigitalSignatureCollection signatures;
                var check = this.VerifySignature(mimeMessage.Body as MultipartSigned, out signatures);
                if (!check || signatures.Count != 1)
                {
                    throw new NotSupportedException("Firma non valida");
                }

                var signature = signatures[0];
                var certificate = signature.SignerCertificate as SecureMimeDigitalCertificate;
                Debug.Assert(certificate != null, nameof(certificate) + " != null");
                this.certificateValidator.validate(Service.AP, certificate.Certificate);

                // Create receipt (MDN)
                mdnBuilder.addHeader(MdnHeader.DISPOSITION, Disposition.PROCESSED);
                MimeMessage mdn = sMimeMessageFactory.createSignedMimeMessage(mdnBuilder.build(), digestMethod);
                mdn.Headers.Add(As2Header.AS2_VERSION, As2Header.VERSION);
                mdn.Headers.Add(As2Header.AS2_FROM, httpHeaders[As2Header.AS2_TO]);
                mdn.Headers.Add(As2Header.AS2_TO, httpHeaders[As2Header.AS2_FROM]);

                // Prepare MDN
                // mdn.WriteTo();
                //ByteArrayOutputStream mdnOutputStream = new ByteArrayOutputStream();
                //mdn.writeTo(mdnOutputStream);

                //// Persist metadata
                //As2InboundMetadata inboundMetadata = new As2InboundMetadata(transmissionIdentifier, header, t2,
                //    digestMethod.getTransportProfile(), calculatedDigest, signer, mdnOutputStream.toByteArray());
                //persisterHandler.persist(inboundMetadata, payloadPath);

                //// Persist statistics
                //statisticsService.persist(inboundMetadata);
                return mdn;
            }
        }


        public bool VerifySignature(MultipartSigned multipartSigned, out DigitalSignatureCollection signatures)
        {
            var signed = multipartSigned;
            if (signed != null)
            {
                using (var ctx = this.secureContextFactory())
                {
                    signatures = signed.Verify(ctx);
                    foreach (var signature in signatures)
                    {
                        bool valid = signature.Verify();
                        return true;
                    }
                }
            }

            signatures = null;
            return false;
        }
    }
}
