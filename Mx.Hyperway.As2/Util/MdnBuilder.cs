using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Hyperway.As2.Util
{
    using System.IO;
    using System.Linq;

    using MimeKit;

    using Mx.Hyperway.As2.Code;
    using Mx.Hyperway.Commons.Util;
    using Mx.Tools;
    using Mx.Tools.Encoding;

    public class MdnBuilder
    {

        private static readonly String ISSUER = String.Format("Hyperway {0}", HyperwayVersion.getVersion());

        // private InternetHeaders headers = new InternetHeaders();

        //  private ByteArrayOutputStream textOutputStream = new ByteArrayOutputStream();

        // private LineOutputStream textLineOutputStream = new LineOutputStream(textOutputStream);
        private HeaderList headers = new HeaderList();

        private IBaseEncoding base64 = new Base64Encoding();

        private StringBuilder sw = new StringBuilder();

        public static MdnBuilder newInstance(MimeMessage mimeMessage)
        {
            MdnBuilder mdnBuilder = new MdnBuilder();
            mdnBuilder.addHeader(MdnHeader.REPORTING_UA, ISSUER);

            // TODO: separate headers
            String recipient = String.Format("rfc822; {0}", mimeMessage.Headers[As2Header.AS2_TO]);
            mdnBuilder.addHeader(MdnHeader.ORIGINAL_RECIPIENT, recipient);
            mdnBuilder.addHeader(MdnHeader.FINAL_RECIPIENT, recipient);

            mdnBuilder.sw.AppendLine("= Received headers");
            mdnBuilder.sw.AppendLine();
            foreach (var header in mimeMessage.Headers)
            {
                mdnBuilder.sw.AppendLine(header.ToString());
                
            }
            mdnBuilder.sw.AppendLine();
            return mdnBuilder;
        }

        private MdnBuilder()
        {
            // No action.
        }

        public void addText(String title, String text)
        {
            this.sw.AppendLine($"= {title}");
            this.sw.AppendLine();
            this.sw.AppendLine(text);
            this.sw.AppendLine();
        }

        public void addHeader(String name, String value)
        {
            this.headers.Add(name, value);
        }

        public void addHeader(String name, DateTime value)
        {
            this.headers.Add(name, As2DateUtil.RFC822.getFormat(value));
        }

        public void addHeader(String name, byte[] value)
        {
            this.headers.Add(name, this.base64.ToString(value));
        }

        public void addHeader(String name, Object value)
        {
            this.headers.Add(name, value.ToString());
        }

        public void addHeader(String name, Disposition disposition)
        {
            this.headers.Add(name, disposition.ToString());
        }

        public Multipart build()
        {
            // Initiate multipart
            MimeMessage mimeMultipart = new MimeMessage();
            var multipart = new Multipart("report; Report-Type=disposition-notification");

            // var textPart = new MimePart("report; Report-Type=disposition-notification");
            // mimeMultipart.setSubType("report; Report-Type=disposition-notification");

            // Insert text part
            //MimeBodyPart textPart = new MimeBodyPart();
            //textLineOutputStream.close();
            var textPart = new MimePart();
            var textContent = Encoding.UTF8.GetBytes(this.sw.ToString());
            textPart.Content = new MimeContent(textContent.ToStream());
            textPart.Headers[HeaderId.ContentType] = "text/plain";
            multipart.Add(textPart);

            // Extract headers
            this.sw = new StringBuilder();
            foreach (var header in this.headers)
            {
                this.sw.AppendLine(header.ToString());
            }
            // ByteArrayOutputStream outputStream = new ByteArrayOutputStream();
            //LineOutputStream lineOutputStream = new LineOutputStream(outputStream);

            // for (String header : Collections.list((Enumeration<String>) headers.getAllHeaderLines()))
            //     lineOutputStream.writeln(header);
            // lineOutputStream.close();

            // Insert header part
            var headerPart = new MimePart();
            headerPart.Headers[HeaderId.ContentType] = "message/disposition-notification";
            var headerContent = Encoding.UTF8.GetBytes(this.sw.ToString());
            headerPart.Content = new MimeContent(headerContent.ToStream());
            multipart.Add(headerPart);

            return multipart;
            //    MimePart mimeBodyPart = new MimePart();

            //mimeBodyPart.setContent(mimeMultipart, mimeMultipart.getContentType());
            //    return mimeBodyPart;
        }
    }

}