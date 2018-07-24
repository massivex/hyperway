namespace Mx.Hyperway.Api.Statistics
{
    using Mx.Hyperway.Api.Inbound;
    using Mx.Hyperway.Api.Outbound;

    using zipkin4net;

    public interface StatisticsService
    {

        void persist(TransmissionRequest transmissionRequest, TransmissionResponse transmissionResponse, Trace root);

        void persist(InboundMetadata inboundMetadata);
    }

}
