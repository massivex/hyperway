namespace Mx.Hyperway.Statistics
{
    using System;

    using Mx.Hyperway.Api.Model;
    using Mx.Peppol.Common.Model;

    public interface IRawStatistics
    {

        ParticipantIdentifier Sender { get; }

        ParticipantIdentifier Receiver { get; }

        Direction Direction { get; }

        DateTime? Date { get; }

        AccessPointIdentifier AccessPointIdentifier { get; }

        DocumentTypeIdentifier DocumentTypeIdentifier { get; }

        ChannelId ChannelId { get; }

        ProcessIdentifier ProcessIdentifier { get; }

    }

}
