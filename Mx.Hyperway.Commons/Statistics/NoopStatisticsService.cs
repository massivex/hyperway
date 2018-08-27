namespace Mx.Hyperway.Commons.Statistics
{
    using Mx.Hyperway.Api.Inbound;
    using Mx.Hyperway.Api.Outbound;
    using Mx.Hyperway.Api.Statistics;

    using zipkin4net;

    public class NoopStatisticsService : IStatisticsService
    {

    public void Persist(ITransmissionRequest transmissionRequest,
                        ITransmissionResponse transmissionResponse, Trace root)
    {
        // No action.
    }

    public void Persist(IInboundMetadata inboundMetadata)
    {
        // No action.
    }
    }

}
