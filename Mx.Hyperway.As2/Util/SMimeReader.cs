namespace Mx.Hyperway.As2.Util
{
    using System;
    using System.IO;
    using System.Text;

    using MimeKit;
    using MimeKit.Cryptography;

    using Mx.Tools;

    using ContentType = System.Net.Mime.ContentType;

    public class SMimeReader : IDisposable
    {

        private Multipart mimeMultipart;

        private byte[] signature;

        private SMimeDigestMethod sMimeDigestMethod;

        public SMimeReader(MimeMessage mimeMessage)
        {
            this.mimeMultipart = mimeMessage.Body as MultipartSigned;
            if (this.mimeMultipart == null)
            {
                throw new InvalidOperationException("Mime multipart is not a MultipartSigned");
            }
            MimeEntity mime = this.mimeMultipart[1];
            
            // Extracting signature
            using (var m = new MemoryStream())
            {
                mime.WriteTo(m, true);
                m.Seek(0, SeekOrigin.Begin);
                this.signature = m.ToBuffer();

            }

            var contentType = new ContentType(mimeMessage.Headers[HeaderId.ContentType]);
            String algorithm = contentType.Parameters?["micalg"];
            if (algorithm == null)
            {
                throw new InvalidOperationException(
                    "micalg parameter not found in Content-Type header: " + contentType);
            }

            this.sMimeDigestMethod = SMimeDigestMethod.FindByIdentifier(algorithm);
        }

        /// <summary>
        /// Extracts headers of body MIME part. Creates headers as done by Bouncycastle. 
        /// </summary>
        /// <returns></returns>
        public byte[] GetBodyHeader() // throws MessagingException, IOException
        {
            MimeEntity body = this.mimeMultipart[0];
            var sb = new StringBuilder();
            foreach (var header in body.Headers)
            {
                sb.AppendLine(header.ToString());
            }

            sb.AppendLine();
            return Encoding.Default.GetBytes(sb.ToString());
        }

        /// <summary>
        /// Extracts content in body MIME part. 
        /// </summary>
        /// <returns>Content</returns>
        public Stream GetBodyInputStream()
        {
            return this.GetBody().ToStream();
        }

        public byte[] GetBody()
        {
            byte[] result;
            using (var m = new MemoryStream())
            {
                this.mimeMultipart[0].WriteTo(m, true);
                m.Seek(0, SeekOrigin.Begin);
                result = m.ToBuffer();
            }

            return result;
        }


        public SMimeDigestMethod GetDigestMethod()
        {
            return this.sMimeDigestMethod;
        }

        /// <summary>
        /// Extracts signature in signature MIME part. 
        /// </summary>
        /// <returns>Signature</returns>
        public byte[] GetSignature()
        {
            return this.signature;
        }

        public void Dispose()
        {
            this.mimeMultipart = null;
            this.signature = null;
        }
    }

}
