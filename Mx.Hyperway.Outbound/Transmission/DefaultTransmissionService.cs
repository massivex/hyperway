namespace Mx.Hyperway.Outbound.Transmission
{
    using System.IO;

    using Mx.Hyperway.Api.Outbound;
    using Mx.Hyperway.Commons.Tracing;

    using zipkin4net;

    // TODO: Register as singleton
    public class DefaultTransmissionService : Traceable, ITransmissionService
    {

        private readonly TransmissionRequestFactory transmissionRequestFactory;

        private readonly ITransmitter transmitter;

        public DefaultTransmissionService(
            TransmissionRequestFactory transmissionRequestFactory,
            ITransmitter transmitter,
            Trace tracer)
            : base(tracer)
        {
            this.transmissionRequestFactory = transmissionRequestFactory;
            this.transmitter = transmitter;
        }

        public ITransmissionResponse Send(Stream inputStream)
        {
            var trace = Trace.Create();
            trace.Record(Annotations.ServiceName("TransmissionService"));
            trace.Record(Annotations.ClientSend());
            try
            {
                return this.Send(inputStream, trace);
            }
            finally
            {
                trace.Record(Annotations.ClientRecv());
            }
        }

        public ITransmissionResponse Send(Stream inputStream, Trace trace)
        {
            return this.transmitter.Transmit(this.transmissionRequestFactory.NewInstance(inputStream, trace), trace);
        }
    }

}
