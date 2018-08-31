using System;

namespace Mx.Peppol.Sbdh
{
    using System.IO;
    using System.Xml;

    using Mx.Peppol.Common.Model;
    using Mx.Peppol.Sbdh.Lang;
    using Mx.Peppol.Xml.Sbdh;
    using Mx.Tools;

    public class SbdhWriter
    {

        public static void Write(Stream outputStream, Header header)
        {
            try
            {
                XmlTextWriter streamWriter = new XmlTextWriter(outputStream, XmlTools.Utf8NoBom);
                streamWriter.WriteStartDocument();
                Write(streamWriter, header);
                streamWriter.WriteEndDocument();
                streamWriter.Dispose();
            }
            catch (Exception e)
            {
                throw new SbdhException("Unable to write SBDH.", e);
            }
        }

        public static void Write(XmlTextWriter streamWriter, Header header)
        {
            try
            {
                StandardBusinessDocumentHeader sbdh = new StandardBusinessDocumentHeader();
                sbdh.HeaderVersion = "1.0";

                // Sender
                sbdh.Sender = new[] { SbdhHelper.CreatePartner(header.Sender) };

                // Receiver
                sbdh.Receiver = new[] { SbdhHelper.CreatePartner(header.Receiver) };

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
                                             SbdhHelper.CreateScope(header.DocumentType),
                                             // ProcessID
                                             SbdhHelper.CreateScope(header.Process)
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
