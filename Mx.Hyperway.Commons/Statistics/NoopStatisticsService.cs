namespace Mx.Hyperway.Commons.Statistics
{
    using Mx.Hyperway.Api.Inbound;
    using Mx.Hyperway.Api.Outbound;
    using Mx.Hyperway.Api.Statistics;

    using zipkin4net;

    public class NoopStatisticsService : StatisticsService
    {

    public void persist(ITransmissionRequest transmissionRequest,
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
