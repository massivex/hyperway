using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Oxalis.Statistics
{
    using Mx.Oxalis.Api.Model;
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
