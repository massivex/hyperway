namespace Mx.Hyperway.Outbound.Transmission
{
    using System.IO;

    using Mx.Hyperway.Api.Outbound;
    using Mx.Hyperway.Commons.Tracing;

    using zipkin4net;

    // TODO: Register as singleton
    public class DefaultTransmissionService : Traceable, TransmissionService
    {

        private readonly TransmissionRequestFactory transmissionRequestFactory;

        private readonly Transmitter transmitter;

        public DefaultTransmissionService(
            TransmissionRequestFactory transmissionRequestFactory,
            Transmitter transmitter,
            Trace tracer)
            : base(tracer)
        {
            this.transmissionRequestFactory = transmissionRequestFactory;
            this.transmitter = transmitter;
        }

        public TransmissionResponse send(Stream inputStream)
        {
            var trace = Trace.Create();
            trace.Record(Annotations.ServiceName("TransmissionService"));
            trace.Record(Annotations.ClientSend());
            try
            {
                return this.send(inputStream, trace);
            }
            finally
            {
                trace.Record(Annotations.ClientRecv());
            }
        }

        public TransmissionResponse send(Stream inputStream, Trace trace)
        {
            return this.transmitter.transmit(this.transmissionRequestFactory.newInstance(inputStream, trace), trace);
        }
    }

}
