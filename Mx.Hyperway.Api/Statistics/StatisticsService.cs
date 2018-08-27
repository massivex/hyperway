namespace Mx.Hyperway.Api.Statistics
{
    using Mx.Hyperway.Api.Inbound;
    using Mx.Hyperway.Api.Outbound;

    using zipkin4net;

    public interface IStatisticsService
    {

        void Persist(ITransmissionRequest transmissionRequest, ITransmissionResponse transmissionResponse, Trace root);

        void Persist(IInboundMetadata inboundMetadata);
    }

}
