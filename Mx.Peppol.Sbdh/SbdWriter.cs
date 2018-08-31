using System;

namespace Mx.Peppol.Sbdh
{
    using System.IO;
    using System.Xml;

    using Mx.Peppol.Common.Model;
    using Mx.Tools;

    public class SbdWriter : IDisposable
    {

        private readonly XmlTextWriter writer;

        public static SbdWriter NewInstance(Stream outputStream, Header header)
        {
            return new SbdWriter(outputStream, header);
        }

        private SbdWriter(Stream outputStream, Header header)
        {
            this.writer = new XmlTextWriter(outputStream, XmlTools.Utf8NoBom);
            this.InitiateDocument(header);
        }

        private void InitiateDocument(Header header)
        {
            this.writer.WriteStartDocument();
            this.writer.WriteStartElement("", Ns.QNAME_SBD.LocalPart, Ns.QNAME_SBD.NamespaceUri);
            SbdhWriter.Write(this.writer, header);
        }

        public void AddFragment(Stream s)
        {
            XmlTools.AddXmlFragment(s, this.writer);
        }

        private void FinalizeDocument()
        {
            this.writer.WriteRaw("</StandardBusinessDocument>");
            this.writer.Flush();
        }


        public void Dispose()
        {
            this.FinalizeDocument();
            // you MUST keep input stream open for further tasks
        }
    }
}
