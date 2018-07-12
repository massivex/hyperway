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
            Header header = Header.newInstance();

            // Sender
            PartnerIdentification senderIdentifier = sbdh.Sender[0].Identifier;
            header = header.sender(
                ParticipantIdentifier.of(senderIdentifier.Value, Scheme.of(senderIdentifier.Authority)));

            // Receiver
            PartnerIdentification receiverIdentifier = sbdh.Receiver[0].Identifier;
            header = header.receiver(
                ParticipantIdentifier.of(receiverIdentifier.Value, Scheme.of(receiverIdentifier.Authority)));

            // Identifier
            header = header.identifier(InstanceIdentifier.of(sbdh.DocumentIdentification.InstanceIdentifier));

            // InstanceType
            header = header.instanceType(
                InstanceType.of(
                    sbdh.DocumentIdentification.Standard,
                    sbdh.DocumentIdentification.Type,
                    sbdh.DocumentIdentification.TypeVersion));

            // CreationTimestamp
            if (sbdh.DocumentIdentification.CreationDateAndTime == null)
            {
                throw new SbdhException("Element 'CreationDateAndTime' is not set or contains invalid value.");
            }

            header = header.creationTimestamp(sbdh.DocumentIdentification.CreationDateAndTime);

            // Scope
            foreach (Scope scope in sbdh.BusinessScope)
            {
                if (scope.Type.Equals("DOCUMENTID"))
                {
                    Scheme scheme = scope.Identifier != null
                                        ? Scheme.of(scope.Identifier)
                                        : DocumentTypeIdentifier.DEFAULT_SCHEME;
                    header = header.documentType(DocumentTypeIdentifier.of(scope.InstanceIdentifier, scheme));
                }
                else if (scope.Type.Equals("PROCESSID"))
                {
                    Scheme scheme = scope.Identifier != null
                                        ? Scheme.of(scope.Identifier)
                                        : ProcessIdentifier.DEFAULT_SCHEME;
                    header = header.process(ProcessIdentifier.of(scope.InstanceIdentifier, scheme));
                }
            }

            return header;
        }
    }
}
