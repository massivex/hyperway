namespace Mx.Hyperway.As2.Util
{
    using System;
    using System.Diagnostics;
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

        /**
         * Creates an S/MIME message from the supplied String, having the supplied MimeType as the "content-type".
         */
        public MimeMessage createSignedMimeMessage(
                string msg,
                string mimeType,
                SMimeDigestMethod digestMethod)
        {
            var msgData = Encoding.Default.GetBytes(msg);
            return this.createSignedMimeMessage(msgData.ToStream(), mimeType, digestMethod);
        }

        /**
         * Creates a new S/MIME message having the supplied MimeType as the "content-type"
         */
        public MimeMessage createSignedMimeMessage(
            Stream inputStream,
            string mimeType,
            SMimeDigestMethod digestMethod)
        {
            MimeEntity mimeBodyPart = MimeMessageHelper.createMimeBodyPart(inputStream, mimeType);
            return this.createSignedMimeMessage(mimeBodyPart, digestMethod);
        }

        public MimeMessage createSignedMimeMessage(MimeEntity mimeBodyPart, SMimeDigestMethod digestMethod)
        {
            if (mimeBodyPart is MimePart)
            {
                // ((MimePart)mimeBodyPart).Content.Encoding = ContentEncoding.Default;
                ((MimePart) mimeBodyPart).ContentTransferEncoding = ContentEncoding.Binary;
            }
            //static MultipartSigned Create(CryptographyContext ctx, DigestAlgorithm digestAlgo, MimeEntity entity, MimeEntity signature)
            //{


            //    return signed;
            //}


            MultipartSigned multipart;
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


                // Signer identification


                // Create and sign message
                multipart = signed;
            }

            var message = new MimeMessage();
            message.Body = multipart;

            return message;
        }
        /**
         * Creates an S/MIME message using the supplied MimeBodyPart. The signature is generated using the private key
         * as supplied in the constructor. Our certificate, which is required to verify the signature is enclosed.
         */
        public MimeMessage createSignedMimeMessage(MimeMessage mimeBodyPart, SMimeDigestMethod digestMethod)
        {
            MimeMessage message = new MimeMessage();
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