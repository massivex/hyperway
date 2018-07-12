using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Oxalis.Outbound.Transmission
{
    using System.IO;

    using Mx.Oxalis.Api.Outbound;
    using Mx.Oxalis.Commons.Tracing;

    using zipkin4net;
    using zipkin4net.Tracers.Zipkin;

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

        public TransmissionResponse
            send(Stream inputStream) // throws IOException, OxalisTransmissionException, OxalisContentException
        {
            var trace = Trace.Create();
            trace.Record(Annotations.ServiceName("TransmissionService"));
            trace.Record(Annotations.ClientSend());
            try
            {
                return send(inputStream, trace);
            }
            finally
            {
                trace.Record(Annotations.ClientRecv());
            }
        }

        public TransmissionResponse
            send(
                Stream inputStream,
                Trace trace) // throws IOException, OxalisTransmissionException, OxalisContentException
        {
            return transmitter.transmit(transmissionRequestFactory.newInstance(inputStream, trace), trace);
        }
    }

}
