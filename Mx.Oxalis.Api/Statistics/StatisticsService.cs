using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Oxalis.Api.Statistics
{
    using Mx.Oxalis.Api.Inbound;
    using Mx.Oxalis.Api.Outbound;

    using zipkin4net;
    using zipkin4net.Tracers.Zipkin;

    public interface StatisticsService
    {

        void persist(TransmissionRequest transmissionRequest, TransmissionResponse transmissionResponse, Trace root);

        void persist(InboundMetadata inboundMetadata);
    }

}
