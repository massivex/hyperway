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

        private Header header;

        public static SbdReader newInstance(Stream inputStream)
        {
            using (var txt = new XmlTextReader(inputStream))
            {
                return newInstance(txt);
            }
        }

        public static SbdReader newInstance(XmlTextReader xmlStreamReader) // throws SbdhException
        {
            return new SbdReader(xmlStreamReader);
        }

        private SbdReader(XmlTextReader reader) // throws SbdhException
        {
            this.reader = reader;

            try
            {
                // First element, SBD expected.

                while (reader.NodeType != XmlNodeType.Element)
                {
                    reader.Read();
                }

                //if (reader.getEventType() != XMLStreamConstants.START_ELEMENT)
                //    reader.nextTag();


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

                this.header = SbdhReader.read(reader);

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

        public Header getHeader()
        {
            return this.header;
        }

        public Type getType()
        {
            if (this.reader.IsElement(Ns.QNAME_BINARY_CONTENT))
            {
                return Type.BINARY;
            }
            else if (this.reader.IsElement(Ns.QNAME_TEXT_CONTENT))
            {
                return Type.TEXT;
            }

            else
            {
                return Type.XML;
            }
        }

        public XElement xmlReader()
        {
            XElement el = XNode.ReadFrom(this.reader) as XElement;
            return el;
        }

        //public Stream binaryReader() // throws XMLStreamException
        //{
        //    return new Base64InputStream(new XMLTextInputStream(xmlReader()));
        //}

        //public InputStream textReader() // throws XMLStreamException
        //{
        //    return new XMLTextInputStream(xmlReader());
        //}

        public void Dispose()
        {
            this.reader.Close();
        }

        public enum Type
        {
            BINARY,

            TEXT,

            XML
        }
    }

}