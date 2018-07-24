namespace Mx.Hyperway.Statistics.Model
{
    using System;

    using Mx.Hyperway.Api.Model;
    using Mx.Peppol.Common.Model;

    public abstract class AbstractBuilder<T, TBuild> : IAbstractBuilder
    {
        private AccessPointIdentifier accessPointIdentifier;

        private DateTime date = DateTime.Now;

        private Direction direction;

        private DocumentTypeIdentifier peppolDocumentTypeId;

        private ProcessIdentifier peppolProcessTypeId;

        private ChannelId channelId;

        public T Date(DateTime dt)
        {
            this.date = dt;
            return this.GetThis();
        }

        public T AccessPointIdentifier(AccessPointIdentifier value)
        {
            this.accessPointIdentifier = value;
            return this.GetThis();
        }

        public T Direction(Direction value)
        {
            this.direction = value;
            return this.GetThis();
        }

        public T Outbound()
        {
            this.direction = Hyperway.Api.Model.Direction.OUT;
            return this.GetThis();
        }

        public T Inbound()
        {
            this.direction = Hyperway.Api.Model.Direction.IN;
            return this.GetThis();
        }

        public T DocumentType(DocumentTypeIdentifier value)
        {
            this.peppolDocumentTypeId = value;
            return this.GetThis();
        }

        public T Profile(ProcessIdentifier processIdentifier)
        {
            this.peppolProcessTypeId = processIdentifier;
            return this.GetThis();
        }

        public T Channel(ChannelId value)
        {
            this.channelId = value;
            return this.GetThis();
        }

        protected void CheckRequiredFields()
        {

            if (this.direction == Hyperway.Api.Model.Direction.None)
            {
                throw new InvalidOperationException("Must specify the direction of the message");
            }

            if (this.accessPointIdentifier == null)
            {
                throw new InvalidOperationException("Identity of access point required");
            }

            if (this.peppolDocumentTypeId == null)
            {
                throw new InvalidOperationException("Document type required");
            }

            if (this.peppolProcessTypeId == null)
            {
                throw new InvalidOperationException("Process id/profile id required");
            }

            if (this.date == null)
            {
                throw new InvalidOperationException("Date (period) required");
            }
        }

        public abstract TBuild Build();

        protected abstract T GetThis();

        public AccessPointIdentifier GetAccessPointIdentifier()
        {
            return this.accessPointIdentifier;
        }

        public DateTime GetDate()
        {
            return this.date;
        }

        public Direction GetDirection()
        {
            return this.direction;
        }

        public DocumentTypeIdentifier GetPeppolDocumentTypeId()
        {
            return this.peppolDocumentTypeId;
        }

        public ProcessIdentifier GetPeppolProcessTypeId()
        {
            return this.peppolProcessTypeId;
        }

        public ChannelId GetChannelId()
        {
            return this.channelId;
        }
    }
}