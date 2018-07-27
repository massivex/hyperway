using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Hyperway.As2.Inbound
{
    using System.IO;
    using System.Security.Cryptography;

    using log4net;

    using Microsoft.AspNetCore.Http;

    using MimeKit;

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

        public As2InboundHandler(
            StatisticsService statisticsService,
            TimestampProvider timestampProvider,
            CertificateValidator certificateValidator,
            PersisterHandler persisterHandler,
            TransmissionVerifier transmissionVerifier,
            SMimeMessageFactory sMimeMessageFactory)
        {
            this.statisticsService = statisticsService;
            this.timestampProvider = timestampProvider;
            this.certificateValidator = certificateValidator;

            this.persisterHandler = persisterHandler;
            this.transmissionVerifier = transmissionVerifier;

            this.sMimeMessageFactory = sMimeMessageFactory;
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
            mdnBuilder.addHeader(MdnHeader.ORIGINAL_CONTENT_HEADER, headerBytes);

            // TODO: calculate hash including headers
            // Prepare calculation of digest
            // MessageDigest messageDigest = BcHelper.getMessageDigest(digestMethod.getIdentifier());
            // InputStream digestInputStream = new DigestInputStream(sMimeReader.getBodyInputStream(), messageDigest);

            // Add header to calculation of digest
            // messageDigest.update(headerBytes);

            var bodyStream = sMimeReader.getBodyInputStream();

            // Prepare content for reading of SBDH
            // PeekingInputStream peekingInputStream = new PeekingInputStream(digestInputStream);

            // Extract SBDH
            Mx.Peppol.Common.Model.Header header;
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
                //            }

                // Fetch calculated digest
                var s = SHA512.Create();
                using (Stream payloadInputStream = sMimeReader.getBodyInputStream())
                {
                    s.ComputeHash(payloadInputStream);
                    var hash = s.ComputeHash(headerBytes);
                    var hashText = BitConverter.ToString(hash).Replace("-", "");

                    Digest calculatedDigest = Digest.of(DigestMethod.SHA512, hash);
                    mdnBuilder.addHeader(MdnHeader.RECEIVED_CONTENT_MIC, new Mic(calculatedDigest));
                }


                //Digest calculatedDigest = Digest.of(digestMethod.getDigestMethod(), messageDigest.digest());


                //// Validate signature using calculated digest
                //X509Certificate signer = SMimeBC.verifySignature(
                //        ImmutableMap.of(digestMethod.getOid(), calculatedDigest.getValue()),
                //        sMimeReader.getSignature()
                //);

            }

            return null;


            //// Validate certificate
            //certificateValidator.validate(Service.AP, signer);

            //            // Create receipt (MDN)
            //            mdnBuilder.addHeader(MdnHeader.DISPOSITION, Disposition.PROCESSED);
            //            MimeMessage mdn = sMimeMessageFactory.createSignedMimeMessage(mdnBuilder.build(), digestMethod);
            //// MimeMessage mdn = sMimeMessageFactory.createSignedMimeMessageNew(mdnBuilder.build(), calculatedDigest, digestMethod);
            //mdn.setHeader(As2Header.AS2_VERSION, As2Header.VERSION);
            //            mdn.setHeader(As2Header.AS2_FROM, httpHeaders.getHeader(As2Header.AS2_TO)[0]);
            //            mdn.setHeader(As2Header.AS2_TO, httpHeaders.getHeader(As2Header.AS2_FROM)[0]);

            //            // Prepare MDN
            //            ByteArrayOutputStream mdnOutputStream = new ByteArrayOutputStream();
            //mdn.writeTo(mdnOutputStream);

            //            // Persist metadata
            //            As2InboundMetadata inboundMetadata = new As2InboundMetadata(transmissionIdentifier, header, t2,
            //                    digestMethod.getTransportProfile(), calculatedDigest, signer, mdnOutputStream.toByteArray());
            //persisterHandler.persist(inboundMetadata, payloadPath);

            //            // Persist statistics
            //            statisticsService.persist(inboundMetadata);

            //            return mdn;
            //        } catch (SbdhException e) {
            //            throw new OxalisAs2InboundException(Disposition.UNSUPPORTED_FORMAT, e.getMessage(), e);
            //        } catch (NoSuchAlgorithmException e) {
            //            throw new OxalisAs2InboundException(Disposition.UNSUPPORTED_MIC_ALGORITHMS, e.getMessage(), e);
            //        } catch (VerifierException e) {
            //            throw new OxalisAs2InboundException(Disposition.fromVerifierException(e), e.getMessage(), e);
            //        } catch (PeppolSecurityException e) {
            //            throw new OxalisAs2InboundException(Disposition.AUTHENTICATION_FAILED, e.getMessage(), e);
            //        } catch (OxalisSecurityException e) {
            //            throw new OxalisAs2InboundException(Disposition.INTEGRITY_CHECK_FAILED, e.getMessage(), e);
            //        } catch (IOException | TimestampException | MessagingException | OxalisTransmissionException e) {
            //            throw new OxalisAs2InboundException(Disposition.UNEXPECTED_PROCESSING_ERROR, e.getMessage(), e);
            //        }

        }
    }
}
