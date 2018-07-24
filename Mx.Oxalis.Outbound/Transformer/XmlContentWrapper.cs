namespace Mx.Hyperway.Outbound.Transformer
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml;

    using Mx.Hyperway.Api.Lang;
    using Mx.Hyperway.Api.Transformer;
    using Mx.Peppol.Common.Model;
    using Mx.Peppol.Sbdh;
    using Mx.Tools;

    public class XmlContentWrapper : ContentWrapper
    {
        public Stream wrap(Stream inputStream, Header header)
        {
            var m = new MemoryStream();
            try
            {
                // ByteArrayOutputStream outputStream = new ByteArrayOutputStream();
                var xml = new XmlTextWriter(m, Encoding.UTF8);
                xml.Formatting = Formatting.None;
                using (var sbdWriter = SbdWriter.newInstance(m, header))
                {
                    XmlTools.AddXmlFragment(inputStream, xml);
                    // XMLStreamUtils.copy(inputStream, sbdWriter.xmlWriter());
                }


            }
            catch (Exception e)
            {
                throw new HyperwayContentException("Unable to wrap content into SBDH.", e);
            }

            m.Seek(0, SeekOrigin.Begin);
            return m;
        }
    }
}