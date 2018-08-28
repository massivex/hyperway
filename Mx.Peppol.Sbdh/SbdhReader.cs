using System;

namespace Mx.Peppol.Sbdh
{
    using System.Xml;
    using System.Xml.Serialization;

    using Mx.Peppol.Common.Model;
    using Mx.Peppol.Sbdh.Lang;
    using Mx.Peppol.Xml.Sbdh;

    public class SbdhReader
    {

        private SbdhReader()
        {

        }

        public static Header read(XmlTextReader xmlStreamReader) // throws SbdhException
        {
            try
            {
                XmlSerializer s = new XmlSerializer(typeof(StandardBusinessDocumentHeader));
                var sbdh = (StandardBusinessDocumentHeader)s.Deserialize(xmlStreamReader);
                return read(sbdh);
            }
            catch (Exception e)
            {
                throw new SbdhException(e.Message, e);
            }
        }

        public static Header read(StandardBusinessDocumentHeader sbdh) // throws SbdhException
        {
            Header header = Header.NewInstance();

            // Sender
            PartnerIdentification senderIdentifier = sbdh.Sender[0].Identifier;
            header = header.SetSender(
                ParticipantIdentifier.Of(senderIdentifier.Value, Scheme.Of(senderIdentifier.Authority)));

            // Receiver
            PartnerIdentification receiverIdentifier = sbdh.Receiver[0].Identifier;
            header = header.SetReceiver(
                ParticipantIdentifier.Of(receiverIdentifier.Value, Scheme.Of(receiverIdentifier.Authority)));

            // Identifier
            header = header.SetIdentifier(InstanceIdentifier.Of(sbdh.DocumentIdentification.InstanceIdentifier));

            // InstanceType
            header = header.SetInstanceType(
                InstanceType.Of(
                    sbdh.DocumentIdentification.Standard,
                    sbdh.DocumentIdentification.Type,
                    sbdh.DocumentIdentification.TypeVersion));

            // CreationTimestamp
            if (sbdh.DocumentIdentification.CreationDateAndTime == null)
            {
                throw new SbdhException("Element 'CreationDateAndTime' is not set or contains invalid value.");
            }

            header = header.SetCreationTimestamp(sbdh.DocumentIdentification.CreationDateAndTime);

            // Scope
            foreach (Scope scope in sbdh.BusinessScope)
            {
                if (scope.Type.Equals("DOCUMENTID"))
                {
                    Scheme scheme = scope.Identifier != null
                                        ? Scheme.Of(scope.Identifier)
                                        : DocumentTypeIdentifier.DefaultScheme;
                    header = header.SetDocumentType(DocumentTypeIdentifier.Of(scope.InstanceIdentifier, scheme));
                }
                else if (scope.Type.Equals("PROCESSID"))
                {
                    Scheme scheme = scope.Identifier != null
                                        ? Scheme.Of(scope.Identifier)
                                        : ProcessIdentifier.DefaultScheme;
                    header = header.SetProcess(ProcessIdentifier.Of(scope.InstanceIdentifier, scheme));
                }
            }

            return header;
        }
    }
}
