using System;
using System.Text;

namespace Mx.Hyperway.As2.Util
{
    using MimeKit;

    using Mx.Hyperway.As2.Code;
    using Mx.Hyperway.Commons.Util;
    using Mx.Tools.Encoding;

    public class MdnBuilder
    {

        private static readonly string Issuer = string.Format("Hyperway {0}", HyperwayVersion.getVersion());

        private HeaderList headers = new HeaderList();

        private IBaseEncoding base64 = new Base64Encoding();

        private StringBuilder sw = new StringBuilder();

        public static MdnBuilder NewInstance(MimeMessage mimeMessage)
        {
            MdnBuilder mdnBuilder = new MdnBuilder();
            mdnBuilder.AddHeader(MdnHeader.ReportingUa, Issuer);

            string recipient = string.Format("rfc822; {0}", mimeMessage.Headers[As2Header.As2To]);
            mdnBuilder.AddHeader(MdnHeader.OriginalRecipient, recipient);
            mdnBuilder.AddHeader(MdnHeader.FinalRecipient, recipient);

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

        public void AddText(string title, string text)
        {
            this.sw.AppendLine($"= {title}");
            this.sw.AppendLine();
            this.sw.AppendLine(text);
            this.sw.AppendLine();
        }

        public void AddHeader(string name, string value)
        {
            this.headers.Add(name, value);
        }

        public void AddHeader(string name, DateTime value)
        {
            this.headers.Add(name, As2DateUtil.Rfc822.GetFormat(value));
        }

        public void AddHeader(string name, byte[] value)
        {
            this.headers.Add(name, this.base64.ToString(value));
        }

        public void AddHeader(string name, object value)
        {
            this.headers.Add(name, value.ToString());
        }

        public void AddHeader(string name, Disposition disposition)
        {
            this.headers.Add(name, disposition.ToString());
        }

        public MultipartReport Build()
        {
            // Initiate multipart
            
            var multipart = new MultipartReport("disposition-notification");
            
            // Insert text part
            var textPart = new TextPart();
            textPart.ContentTransferEncoding = ContentEncoding.SevenBit;
            textPart.SetText(Encoding.ASCII, this.sw.ToString());
            textPart.ContentTransferEncoding = ContentEncoding.SevenBit;
            multipart.Add(textPart);

            // Insert header part
            var headerPart = new MessageDispositionNotification();
            headerPart.ContentTransferEncoding = ContentEncoding.Default;
            foreach (var header in this.headers)
            {
                headerPart.Fields.Add(header);
            }
            headerPart.ContentTransferEncoding = ContentEncoding.SevenBit;
            multipart.Add(headerPart);
            return multipart;
        }
    }

}