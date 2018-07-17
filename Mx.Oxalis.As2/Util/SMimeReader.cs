using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Oxalis.As2.Util
{
    using System.Collections;
    using System.Diagnostics;
    using System.IO;
    using System.Net.Mime;

    using Mx.Mime;
    using Mx.Oxalis.As2.Code;
    using Mx.Tools;

    public class SMimeReader : IDisposable
    {

        private List<MimeBody> mimeMultipart;

        private byte[] signature;

        private SMimeDigestMethod sMimeDigestMethod;

        public SMimeReader(MimeMessage mimeMessage) // throws MessagingException, IOException
        {
            this.mimeMultipart = mimeMessage.GetBodyPartList();

            // Extracting signature
            signature = this.mimeMultipart[1].GetBuffer(); //  ByteStreams.toByteArray(((InputStream)mimeMultipart.getBodyPart(1).getContent()));

            // Extracting DNO
            String[] dno = mimeMessage.GetAllFieldValue(As2Header.DISPOSITION_NOTIFICATION_OPTIONS);

            // if (dno == null)
            // throw new IllegalStateException("Unable to extract dno.");
            var contentType = new ContentType(mimeMessage.GetContentType());
            String algorithm = contentType.Parameters?["micalg"];
            if (algorithm == null)
            {
                throw new InvalidOperationException(
                    "micalg parameter not found in Content-Type header: " + mimeMessage.GetContentType());
            }

            sMimeDigestMethod = SMimeDigestMethod.findByIdentifier(algorithm);
        }

        /**
         * Extracts headers of body MIME part. Creates headers as done by Bouncycastle.
         *
         * @return Headers
         */
        //public byte[] getBodyHeader() // throws MessagingException, IOException
        //{
        //    ByteArrayOutputStream outputStream = new ByteArrayOutputStream();
        //    LineOutputStream los = new LineOutputStream(outputStream);

        //    Enumeration hdrLines =
        //        ((MimeBodyPart)mimeMultipart.getBodyPart(0)).getNonMatchingHeaderLines(new String[] { });
        //    while (hdrLines.hasMoreElements())
        //        los.writeln((String)hdrLines.nextElement());

        //    // The CRLF separator between header and content
        //    los.writeln();
        //    los.close();

        //    return outputStream.toByteArray();
        //}

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
            return mimeMultipart[0].GetBuffer();
        }


        public SMimeDigestMethod getDigestMethod()
        {
            return sMimeDigestMethod;
        }

        /**
         * Extracts signature in signature MIME part.
         *
         * @return Signature
         */
        public byte[] getSignature() // throws MessagingException, IOException
        {
            return signature;
        }

        public void Dispose() // throws IOException
        {
            mimeMultipart = null;
            signature = null;
        }
    }

}
