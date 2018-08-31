namespace Mx.Peppol.Sbdh
{
    using Mx.Peppol.Common.Model;
    using Mx.Peppol.Xml.Sbdh;

    class SbdhHelper
    {

        public static Partner CreatePartner(ParticipantIdentifier participant)
        {
            PartnerIdentification partnerIdentification = new PartnerIdentification();
            partnerIdentification.Authority = participant.Scheme.Identifier;
            partnerIdentification.Value = participant.Identifier;

            Partner partner = new Partner();
            partner.Identifier = partnerIdentification;
            return partner;
        }

        public static Scope CreateScope(ProcessIdentifier processIdentifier)
        {
            Scope scope = new Scope();
            scope.Type = "PROCESSID";
            scope.InstanceIdentifier = processIdentifier.Identifier;
            if (!processIdentifier.Scheme.Equals(ProcessIdentifier.DefaultScheme))
            {
                scope.Identifier = processIdentifier.Scheme.Identifier;
            }

            return scope;
        }

        public static Scope CreateScope(DocumentTypeIdentifier documentTypeIdentifier)
        {
            Scope scope = new Scope();
            scope.Type = "DOCUMENTID";
            scope.InstanceIdentifier = documentTypeIdentifier.Identifier;
            if (!documentTypeIdentifier.Scheme.Equals(DocumentTypeIdentifier.DefaultScheme))
            {
                scope.Identifier = documentTypeIdentifier.Scheme.Identifier;
            }

            return scope;
        }
    }
}
