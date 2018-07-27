namespace Mx.Hyperway.As2.Util
{
    using System;
    using System.IO;
    using System.Text;

    using MimeKit;
    using MimeKit.Cryptography;

    using Mx.Hyperway.As2.Code;
    using Mx.Tools;

    using ContentType = System.Net.Mime.ContentType;

    public class SMimeReader : IDisposable
    {

        private Multipart mimeMultipart;

        private byte[] signature;

        private SMimeDigestMethod sMimeDigestMethod;

        public SMimeReader(MimeMessage mimeMessage) // throws MessagingException, IOException
        {
            this.mimeMultipart = mimeMessage.Body as MultipartSigned;

            MimeEntity mime = this.mimeMultipart[1];
            // Extracting signature
            using (var m = new MemoryStream())
            {
                mime.WriteTo(m, true);
                m.Seek(0, SeekOrigin.Begin);
                this.signature = m.GetBuffer();

            }
            // signature = this.mimeMultipart[1].WriteTo(). GetBuffer(); //  ByteStreams.toByteArray(((InputStream)mimeMultipart.getBodyPart(1).getContent()));

            // Extracting DNO
            String[] dno = mimeMessage.Headers[As2Header.DISPOSITION_NOTIFICATION_OPTIONS].Split(new[] { "\\r\\n" }, StringSplitOptions.None);

            // if (dno == null)
            // throw new IllegalStateException("Unable to extract dno.");
            var contentType = new ContentType(mimeMessage.Headers[HeaderId.ContentType]);
            String algorithm = contentType.Parameters?["micalg"];
            if (algorithm == null)
            {
                throw new InvalidOperationException(
                    "micalg parameter not found in Content-Type header: " + contentType);
            }

            this.sMimeDigestMethod = SMimeDigestMethod.findByIdentifier(algorithm);
        }

        /**
         * Extracts headers of body MIME part. Creates headers as done by Bouncycastle.
         */
        public byte[] getBodyHeader() // throws MessagingException, IOException
        {
            MimeEntity body = this.mimeMultipart[0];
            var sb = new StringBuilder();
            foreach (var header in body.Headers)
            {
                sb.AppendLine(header.ToString());
            }

            sb.AppendLine();
            return Encoding.Default.GetBytes(sb.ToString());
            //ByteArrayOutputStream outputStream = new ByteArrayOutputStream();
            //LineOutputStream los = new LineOutputStream(outputStream);

            //Enumeration hdrLines =
            //    ((MimeBodyPart)mimeMultipart.getBodyPart(0)).getNonMatchingHeaderLines(new String[] { });
            //while (hdrLines.hasMoreElements())
            //    los.writeln((String)hdrLines.nextElement());

            //// The CRLF separator between header and content
            //los.writeln();
            //los.close();

            //return outputStream.toByteArray();
        }

        /**
         * Extracts content in body MIME part.
         *
         * @return Content
         */
        public Stream getBodyInputStream() // throws MessagingException, IOException
        {
            return this.getBody().ToStream();
        }

        public byte[] getBody() // throws MessagingException, IOException
        {
            byte[] result;
            using (var m = new MemoryStream())
            {
                this.mimeMultipart[0].WriteTo(m, true);
                m.Seek(0, SeekOrigin.Begin);
                result = m.GetBuffer();
            }

            return result;
        }


        public SMimeDigestMethod getDigestMethod()
        {
            return this.sMimeDigestMethod;
        }

        /**
         * Extracts signature in signature MIME part.
         *
         * @return Signature
         */
        public byte[] getSignature() // throws MessagingException, IOException
        {
            return this.signature;
        }

        public void Dispose() // throws IOException
        {
            this.mimeMultipart = null;
            this.signature = null;
        }
    }

}
