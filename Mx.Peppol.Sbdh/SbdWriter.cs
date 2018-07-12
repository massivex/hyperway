using System;
using System.Text;

namespace Mx.Peppol.Sbdh
{
    using System.IO;
    using System.Xml;

    using Mx.Peppol.Common.Model;
    using Mx.Tools;

    public class SbdWriter : IDisposable
    {

        private XmlTextWriter writer;

        public static SbdWriter newInstance(Stream outputStream, Header header) // throws SbdhException
        {
            return new SbdWriter(outputStream, header);
        }

        private SbdWriter(Stream outputStream, Header header) // throws SbdhException
        {
            this.writer = new XmlTextWriter(outputStream, Encoding.UTF8);
            this.initiateDocument(header);
        }

        private void initiateDocument(Header header) // throws SbdhException
        {
            this.writer.WriteStartDocument();
            // writer.WriteStartDocument("UTF-8", "1.0");
            this.writer.WriteStartElement("", Ns.QNAME_SBD.LocalPart, Ns.SBDH);
            this.writer.WriteQualifiedName(
                Ns.QNAME_SBDH.LocalPart,
                Ns.QNAME_SBDH.NamespaceUri); // .WriteDefaultNamespace(Ns.SBDH);
            SbdhWriter.write(this.writer, header);
        }

        public void AddFragment(Stream s)
        {
            XmlTools.AddXmlFragment(s, this.writer);
        }
        //public Stream binaryWriter(String mimeType) // throws XMLStreamException
        //{
        //    return this.binaryWriter(mimeType, null);
        //}

        //public Stream binaryWriter(String mimeType, String encoding) // throws XMLStreamException
        //{
        //    return new XMLBinaryOutputStream(xmlWriter(), mimeType, encoding);
        //}

        //public Stream textWriter(String mimeType) // throws XMLStreamException
        //{
        //    return new XMLTextOutputStream(xmlWriter(), mimeType);
        //}

        private void finalizeDocument() // throws SbdhException
        {
            this.writer.WriteEndElement();
            this.writer.WriteEndDocument();

            //    ExceptionUtil.perform(SbdhException.class, new PerformAction()
            //{
            //    @Override
            //            public void action() throws Exception {
            //        writer.writeEndElement();
            //        writer.writeEndDocument();
            //    }
            //});
        }


        public void Dispose() // throws IOException
        {
            this.finalizeDocument();
            this.writer.Dispose();

            //    ExceptionUtil.perform(IOException.class, new PerformAction()
            //{
            //    @Override
            //            public void action() throws Exception {
            //        finalizeDocument();
            //        writer.close();
            //    }
            //});
        }
    }
}
