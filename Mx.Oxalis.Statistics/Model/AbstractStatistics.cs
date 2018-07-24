namespace Mx.Hyperway.Statistics.Model
{
    using System;

    using Mx.Hyperway.Api.Model;
    using Mx.Peppol.Common.Model;

    public abstract class AbstractStatistics : IRawStatistics
    {
        internal AbstractStatistics(IAbstractBuilder abstractBuilder)
        {
            this.ProcessIdentifier = abstractBuilder.GetPeppolProcessTypeId();
            this.DocumentTypeIdentifier = abstractBuilder.GetPeppolDocumentTypeId();
            this.AccessPointIdentifier = abstractBuilder.GetAccessPointIdentifier();
            this.Date = abstractBuilder.GetDate();
            this.Direction = abstractBuilder.GetDirection();
            this.ChannelId = abstractBuilder.GetChannelId();
        }

        public ParticipantIdentifier Sender { get; protected set; }

        public ParticipantIdentifier Receiver { get; protected set; }

        public Direction Direction { get; }

        public DateTime? Date { get; }

        public AccessPointIdentifier AccessPointIdentifier { get; }

        public DocumentTypeIdentifier DocumentTypeIdentifier { get; }

        public ChannelId ChannelId { get; }

        public ProcessIdentifier ProcessIdentifier { get; }
    }
}