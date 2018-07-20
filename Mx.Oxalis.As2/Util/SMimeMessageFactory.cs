using System;
using System.Text;

namespace Mx.Oxalis.As2.Util
{
    using Mx.Tools;

    using Org.BouncyCastle.Crypto;
    using Org.BouncyCastle.X509;

    using System.IO;
    using System.Linq;

    using MimeKit;
    using MimeKit.Cryptography;

    public class SMimeMessageFactory
    {

        private readonly AsymmetricKeyParameter privateKey;

        private readonly X509Certificate ourCertificate;

        private readonly Func<OxalisSecureMimeContext> secContentFactory;

        public SMimeMessageFactory(AsymmetricKeyParameter privateKey, X509Certificate ourCertificate, Func<OxalisSecureMimeContext> secContentFactory)
        {
            this.privateKey = privateKey;
            this.ourCertificate = ourCertificate;
            this.secContentFactory = secContentFactory;
        }

        /**
         * Creates an S/MIME message from the supplied String, having the supplied MimeType as the "content-type".
         */
        public MimeMessage createSignedMimeMessage(
                string msg,
                string mimeType,
                SMimeDigestMethod digestMethod) // throws OxalisTransmissionException
        {
            var msgData = Encoding.Default.GetBytes(msg);
            return createSignedMimeMessage(msgData.ToStream(), mimeType, digestMethod);
        }

        /**
         * Creates a new S/MIME message having the supplied MimeType as the "content-type"
         */
        public MimeMessage createSignedMimeMessage(
            Stream inputStream,
            string mimeType,
            SMimeDigestMethod digestMethod) // throws OxalisTransmissionException
        {
            MimeEntity mimeBodyPart = MimeMessageHelper.createMimeBodyPart(inputStream, mimeType);
            return createSignedMimeMessage(mimeBodyPart, digestMethod);
        }

        /**
         * Creates an S/MIME message using the supplied MimeBodyPart. The signature is generated using the private key
         * as supplied in the constructor. Our certificate, which is required to verify the signature is enclosed.
         */
        public MimeMessage createSignedMimeMessage(MimeEntity mimeBodyPart, SMimeDigestMethod digestMethod)
            // throws OxalisTransmissionException
        {
            MimeMessage message;
            using (var ctx = this.secContentFactory())
            {
                // Algorithm lookup
                DigestAlgorithm algorithm;
                if (digestMethod.Equals(SMimeDigestMethod.sha1))
                {
                    algorithm = DigestAlgorithm.Sha1;
                }
                else if (digestMethod.Equals(SMimeDigestMethod.sha512))
                {
                    algorithm = DigestAlgorithm.Sha512;
                }
                else
                {
                    throw new NotSupportedException($"Algorithm {digestMethod.getAlgorithm()} not supported");
                }

                // Signer identification
                var cmsSigner = new CmsSigner(this.ourCertificate, this.privateKey)
                {
                    DigestAlgorithm = algorithm
                };

                // Create and sign message
                message = new MimeMessage { Body = mimeBodyPart };
                message.Body = MultipartSigned.Create(ctx, cmsSigner, mimeBodyPart);

                // Force signed content to be transferred in binary format
                MimePart xml = (MimePart) message.BodyParts.First();
                xml.ContentTransferEncoding = ContentEncoding.Binary;
            }


            // Remove unused headers
            foreach (var header in message.Headers.Select(x => x.Id).ToList())
            {
                if (header != HeaderId.MessageId && header != HeaderId.MimeVersion && header != HeaderId.ContentType)
                {
                    message.Headers.Remove(header);
                }

            }

            return message;
        }
    }
}