namespace Mx.Hyperway.Statistics.Model
{
    using System;

    using Mx.Peppol.Common.Model;

    public class RawStatisticsBuilder : AbstractBuilder<RawStatisticsBuilder, DefaultRawStatistics>
    {
        ParticipantIdentifier sender;

        ParticipantIdentifier receiver;

        public ParticipantIdentifier GetSender()
        {
            return this.sender;
        }

        public ParticipantIdentifier GetReceiver()
        {
            return this.receiver;
        }

        public RawStatisticsBuilder Sender(ParticipantIdentifier value)
        {
            this.sender = value;
            return this.GetThis();
        }

        public RawStatisticsBuilder Receiver(ParticipantIdentifier value)
        {
            this.receiver = value;
            return this.GetThis();
        }

        public override DefaultRawStatistics Build()
        {
            this.CheckRequiredFields();

            if (this.sender == null)
            {
                throw new ArgumentException("Must specify identity of sender");
            }

            if (this.receiver == null)
            {
                throw new ArgumentException("Identity of receiver required");
            }

            return new DefaultRawStatistics(this);
        }

        protected override RawStatisticsBuilder GetThis()
        {
            return this;
        }
    }
}
