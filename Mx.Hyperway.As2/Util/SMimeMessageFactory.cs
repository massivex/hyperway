namespace Mx.Hyperway.As2.Util
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;

    using MimeKit;
    using MimeKit.Cryptography;
    using MimeKit.IO;

    using Mx.Tools;

    using Org.BouncyCastle.Crypto;
    using Org.BouncyCastle.X509;

    public class SMimeMessageFactory
    {

        private readonly AsymmetricKeyParameter privateKey;

        private readonly X509Certificate ourCertificate;

        private readonly Func<HyperwaySecureMimeContext> secContentFactory;

        public SMimeMessageFactory(AsymmetricKeyParameter privateKey, X509Certificate ourCertificate, Func<HyperwaySecureMimeContext> secContentFactory)
        {
            this.privateKey = privateKey;
            this.ourCertificate = ourCertificate;
            this.secContentFactory = secContentFactory;
        }

        /// <summary>
        /// Creates an S/MIME message from the supplied String, having the supplied MimeType as the "content-type". 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="mimeType"></param>
        /// <param name="digestMethod"></param>
        /// <returns></returns>
        public MimeMessage CreateSignedMimeMessage(
                string msg,
                string mimeType,
                SMimeDigestMethod digestMethod)
        {
            var msgData = Encoding.Default.GetBytes(msg);
            return this.CreateSignedMimeMessage(msgData.ToStream(), mimeType, digestMethod);
        }

        /// <summary>
        /// Creates a new S/MIME message having the supplied MimeType as the "content-type"
        /// </summary>
        /// <param name="inputStream"></param>
        /// <param name="mimeType"></param>
        /// <param name="digestMethod"></param>
        /// <returns></returns>
        public MimeMessage CreateSignedMimeMessage(
            Stream inputStream,
            string mimeType,
            SMimeDigestMethod digestMethod)
        {
            MimeEntity mimeBodyPart = MimeMessageHelper.CreateMimeBodyPart(inputStream, mimeType);
            return this.CreateSignedMimeMessage(mimeBodyPart, digestMethod);
        }

        public MimeMessage CreateSignedMimeMessage(MimeEntity mimeBodyPart, SMimeDigestMethod digestMethod)
        {
            if (mimeBodyPart is MimePart)
            {
                ((MimePart) mimeBodyPart).ContentTransferEncoding = ContentEncoding.Binary;
            }

            MultipartSigned multipart;
            using (var ctx = this.secContentFactory())
            {
                // Algorithm lookup
                DigestAlgorithm algorithm;
                if (digestMethod.Equals(SMimeDigestMethod.Sha1))
                {
                    algorithm = DigestAlgorithm.Sha1;
                }
                else if (digestMethod.Equals(SMimeDigestMethod.Sha512))
                {
                    algorithm = DigestAlgorithm.Sha512;
                }
                else
                {
                    throw new NotSupportedException($"Algorithm {digestMethod.GetAlgorithm()} not supported");
                }

                var micalg = ctx.GetDigestAlgorithmName(algorithm);
                var signed = new MultipartSigned();

                // set the protocol and micalg Content-Type parameters
                signed.ContentType.Parameters["protocol"] = ctx.SignatureProtocol;
                signed.ContentType.Parameters["micalg"] = micalg;


                // add the modified/parsed entity as our first part
                signed.Add(mimeBodyPart);

                var cmsSigner = new CmsSigner(this.ourCertificate, this.privateKey) { DigestAlgorithm = algorithm };
                ApplicationPkcs7Signature signature;
                using (var memory = new MemoryBlockStream())
                {
                    // var prepared = Prepare(entity, memory);
                    mimeBodyPart.WriteTo(memory);
                    memory.Position = 0;

                    // sign the cleartext content
                    signature = ctx.Sign(cmsSigner, memory);
                }

                // add the detached signature as the second part
                signed.Add(signature);

                // Create and sign message
                multipart = signed;
            }

            var message = new MimeMessage();
            message.Body = multipart;

            return message;
        }

        /// <summary>
        /// Creates an S/MIME message using the supplied MimeBodyPart. The signature is generated using the private key
        /// as supplied in the constructor. Our certificate, which is required to verify the signature is enclosed.
        /// </summary>
        /// <param name="mimeBodyPart"></param>
        /// <param name="digestMethod"></param>
        /// <returns></returns>
        public MimeMessage CreateSignedMimeMessage(MimeMessage mimeBodyPart, SMimeDigestMethod digestMethod)
        {
            MimeMessage message = new MimeMessage();
            using (var ctx = this.secContentFactory())
            {
                // Algorithm lookup
                DigestAlgorithm algorithm;
                if (digestMethod.Equals(SMimeDigestMethod.Sha1))
                {
                    algorithm = DigestAlgorithm.Sha1;
                }
                else if (digestMethod.Equals(SMimeDigestMethod.Sha512))
                {
                    algorithm = DigestAlgorithm.Sha512;
                }
                else
                {
                    throw new NotSupportedException($"Algorithm {digestMethod.GetAlgorithm()} not supported");
                }

                // Signer identification
                var cmsSigner = new CmsSigner(this.ourCertificate, this.privateKey)
                {
                    DigestAlgorithm = algorithm
                };

                // Create and sign message
                message.Body = MultipartSigned.Create(ctx, cmsSigner, mimeBodyPart.Body);

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