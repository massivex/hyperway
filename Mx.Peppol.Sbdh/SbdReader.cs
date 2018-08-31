using System;

namespace Mx.Peppol.Sbdh
{
    using System.IO;
    using System.Xml;
    using System.Xml.Linq;

    using Mx.Peppol.Common.Model;
    using Mx.Peppol.Sbdh.Lang;
    using Mx.Tools;

    public class SbdReader : IDisposable
    {

        private XmlTextReader reader;

        public static SbdReader NewInstance(Stream inputStream)
        {
            using (var txt = new XmlTextReader(inputStream))
            {
                return NewInstance(txt);
            }
        }

        public static SbdReader NewInstance(XmlTextReader xmlStreamReader)
        {
            return new SbdReader(xmlStreamReader);
        }

        private SbdReader(XmlTextReader reader)
        {
            this.reader = reader;

            try
            {
                // First element, SBD expected.
                while (reader.NodeType != XmlNodeType.Element)
                {
                    reader.Read();
                }

                if (!reader.IsElement(Ns.QNAME_SBD))
                {
                    throw new SbdhException("Element 'StandardBusinessDocument' not found as first element.");
                }

                // Read header
                reader.ReadToNextElement();
                if (!reader.IsElement(Ns.QNAME_SBDH))
                {
                    throw new SbdhException(
                        "Element 'StandardBusinessDocumentHeader' not found as first element in 'StandardBusinessDocument'.");
                }

                this.Header = SbdhReader.Read(reader);

                // Go to payload
                var found = reader.ReadToNextElement();
                if (!found)
                {
                    throw new SbdhException("Payload not found.");
                }
            }
            catch (Exception e)
            {
                throw new SbdhException(e.Message, e);
            }
        }

        public Header Header { get; }

        public SbdReaderType Type
        {
            get
            {
                if (this.reader.IsElement(Ns.QNAME_BINARY_CONTENT))
                {
                    return SbdReaderType.Binary;
                }
                else if (this.reader.IsElement(Ns.QNAME_TEXT_CONTENT))
                {
                    return SbdReaderType.Text;
                }

                else
                {
                    return SbdReaderType.Xml;
                }
            }
        }

        public XElement XmlReader()
        {
            XElement el = XNode.ReadFrom(this.reader) as XElement;
            return el;
        }

        public void Dispose()
        {
            this.reader.Close();
        }
    }
}