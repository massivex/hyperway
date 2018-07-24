namespace Mx.Hyperway.Standalone
{
    using System.IO;

    using Mx.Hyperway.Outbound;
    using Mx.Peppol.Common.Model;

    public class TransmissionParameters
    {
        public TransmissionParameters(HyperwayOutboundComponent hyperwayOutboundComponent)
        {
            this.HyperwayOutboundComponent = hyperwayOutboundComponent;
        }

        public ParticipantIdentifier Receiver { get; set; }

        public ParticipantIdentifier Sender { get; set; }

        public DocumentTypeIdentifier DocType { get; set; }

        public ProcessIdentifier ProcessIdentifier { get; set; }

        public DirectoryInfo EvidencePath { get; set; }


        public HyperwayOutboundComponent HyperwayOutboundComponent { get; }

        public bool UseFactory { get; set; }

        public Endpoint Endpoint { get; set; }
    }

}
