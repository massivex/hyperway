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

    public class SbdhWriter
    {

        public static void write(Stream outputStream, Header header) // throws SbdhException
        {
            try
            {
                //                XmlTextWriter streamWriter = SbdhHelper.XML_OUTPUT_FACTORY.createXMLStreamWriter(outputStream, "UTF-8");

                XmlTextWriter streamWriter = new XmlTextWriter(outputStream, Encoding.UTF8);
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
                sbdh.Sender = new[] { SbdhHelper.createPartner(header.getSender()) };

                // Receiver
                sbdh.Receiver = new[] { SbdhHelper.createPartner(header.getReceiver()) };

                sbdh.DocumentIdentification = new DocumentIdentification();
                // Standard
                sbdh.DocumentIdentification.Standard = header.getInstanceType().getStandard();
                // TypeVersion
                sbdh.DocumentIdentification.TypeVersion = header.getInstanceType().getVersion();
                // Identifier
                sbdh.DocumentIdentification.InstanceIdentifier = header.getIdentifier().getIdentifier();
                // Type
                sbdh.DocumentIdentification.Type = header.getInstanceType().getType();
                // CreationDateAndTime
                var creationTime = header.getCreationTimestamp();
                if (creationTime == null)
                {
                    throw new InvalidOperationException("CreationTimestamp cannot be null");
                }
                sbdh.DocumentIdentification.CreationDateAndTime = creationTime.Value;

                sbdh.BusinessScope = new[]
                                         {
                                             // DocumentID
                                             SbdhHelper.createScope(header.getDocumentType()),
                                             // ProcessID
                                             SbdhHelper.createScope(header.getProcess())
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
