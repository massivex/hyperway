namespace Mx.Oxalis.Standalone
{
    using System.IO;

    using Mx.Oxalis.Outbound;
    using Mx.Peppol.Common.Model;

    public class TransmissionParameters
    {
        public TransmissionParameters(OxalisOutboundComponent oxalisOutboundComponent)
        {
            this.OxalisOutboundComponent = oxalisOutboundComponent;
        }

        public ParticipantIdentifier Receiver { get; set; }

        public ParticipantIdentifier Sender { get; set; }

        public DocumentTypeIdentifier DocType { get; set; }

        public ProcessIdentifier ProcessIdentifier { get; set; }

        public DirectoryInfo EvidencePath { get; set; }


        public OxalisOutboundComponent OxalisOutboundComponent { get; }

        public bool UseFactory { get; set; }

        public Endpoint Endpoint { get; set; }
    }

}
