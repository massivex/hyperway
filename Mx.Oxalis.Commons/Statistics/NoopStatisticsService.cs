using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Oxalis.Commons.Statistics
{
    using Mx.Oxalis.Api.Inbound;
    using Mx.Oxalis.Api.Outbound;
    using Mx.Oxalis.Api.Statistics;

    using zipkin4net;

    public class NoopStatisticsService : StatisticsService
    {

    public void persist(TransmissionRequest transmissionRequest,
                        TransmissionResponse transmissionResponse, Trace root)
    {
        // No action.
    }

    public void persist(InboundMetadata inboundMetadata)
    {
        // No action.
    }
    }

}
