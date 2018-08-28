using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Peppol.Sbdh
{
    using System.IO;
    using System.Xml;
    using System.Xml.Serialization;

    using Mx.Peppol.Common.Model;
    using Mx.Peppol.Sbdh.Lang;
    using Mx.Peppol.Xml;
    using Mx.Peppol.Xml.Sbdh;
    using Mx.Tools;

    public class SbdhWriter
    {

        public static void write(Stream outputStream, Header header) // throws SbdhException
        {
            try
            {
                //                XmlTextWriter streamWriter = SbdhHelper.XML_OUTPUT_FACTORY.createXMLStreamWriter(outputStream, "UTF-8");

                XmlTextWriter streamWriter = new XmlTextWriter(outputStream, XmlTools.Utf8NoBom);
                streamWriter.WriteStartDocument();
                write(streamWriter, header);
                streamWriter.WriteEndDocument();
                streamWriter.Dispose();
            }
            catch (Exception e)
            {
                throw new SbdhException("Unable to write SBDH.", e);
            }
        }

        public static void write(XmlTextWriter streamWriter, Header header) // throws SbdhException
        {
            try
            {
                StandardBusinessDocumentHeader sbdh = new StandardBusinessDocumentHeader();
                sbdh.HeaderVersion = "1.0";

                // Sender
                sbdh.Sender = new[] { SbdhHelper.createPartner(header.Sender) };

                // Receiver
                sbdh.Receiver = new[] { SbdhHelper.createPartner(header.Receiver) };

                sbdh.DocumentIdentification = new DocumentIdentification();
                // Standard
                sbdh.DocumentIdentification.Standard = header.InstanceType.Standard;
                // TypeVersion
                sbdh.DocumentIdentification.TypeVersion = header.InstanceType.Version;
                // Identifier
                sbdh.DocumentIdentification.InstanceIdentifier = header.Identifier.Identifier;
                // Type
                sbdh.DocumentIdentification.Type = header.InstanceType.Type;
                // CreationDateAndTime
                var creationTime = header.CreationTimestamp;
                if (creationTime == null)
                {
                    throw new InvalidOperationException("CreationTimestamp cannot be null");
                }
                sbdh.DocumentIdentification.CreationDateAndTime = creationTime.Value;

                sbdh.BusinessScope = new[]
                                         {
                                             // DocumentID
                                             SbdhHelper.createScope(header.DocumentType),
                                             // ProcessID
                                             SbdhHelper.createScope(header.Process)
                                         };

                XmlTools.WriteXmlFragment(streamWriter, sbdh);
            }
            catch (Exception e)
            {
                throw new SbdhException("Unable to write SBDH.", e);
            }
        }
    }
}
