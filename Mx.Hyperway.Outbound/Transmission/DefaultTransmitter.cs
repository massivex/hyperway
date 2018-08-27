namespace Mx.Hyperway.Outbound.Transmission
{
    using Mx.Hyperway.Api.Lang;
    using Mx.Hyperway.Api.Lookup;
    using Mx.Hyperway.Api.Model;
    using Mx.Hyperway.Api.Outbound;
    using Mx.Hyperway.Api.Statistics;
    using Mx.Hyperway.Api.Transmission;
    using Mx.Peppol.Common.Model;

    using zipkin4net;

    /// <summary>
    /// Executes transmission requests by sending the payload to the requested destination.
    /// Updates statistics for the transmission using the configured RawStatisticsRepository.
    /// </summary>
    public class DefaultTransmitter : ITransmitter
    {
        /// <summary>Factory used to fetch implementation of required transport profile implementation.</summary>
        private readonly MessageSenderFactory messageSenderFactory;

        /// <summary>Service to report statistics when transmission is successfully transmitted.</summary>
        private readonly IStatisticsService statisticsService;

        private readonly ITransmissionVerifier transmissionVerifier;

        private readonly ILookupService lookupService;

        public DefaultTransmitter(
            MessageSenderFactory messageSenderFactory,
            IStatisticsService statisticsService,
            ITransmissionVerifier transmissionVerifier,
            ILookupService lookupService)
        {
            this.messageSenderFactory = messageSenderFactory;
            this.statisticsService = statisticsService;
            this.transmissionVerifier = transmissionVerifier;
            this.lookupService = lookupService;
        }

        /// <inheritdoc />
        public ITransmissionResponse Transmit(ITransmissionMessage transmissionMessage, Trace root)
        {
            Trace span = root.Child();
            span.Record(Annotations.ServiceName("transmit"));
            span.Record(Annotations.ClientSend());
            try
            {
                return this.Perform(transmissionMessage, span);
            }
            finally
            {
                span.Record(Annotations.ClientRecv());
            }
        }

        /// <inheritdoc />
        public ITransmissionResponse Transmit(ITransmissionMessage transmissionMessage)
        {
            Trace root = Trace.Create();
            root.Record(Annotations.ServiceName("transmit"));
            root.Record(Annotations.ClientSend());
            try
            {
                return this.Perform(transmissionMessage, root);
            }
            finally
            {
                root.Record(Annotations.ClientRecv());
            }
        }

        private ITransmissionResponse Perform(ITransmissionMessage transmissionMessage, Trace root)
        {

            this.transmissionVerifier.Verify(transmissionMessage.GetHeader(), Direction.OUT);

            ITransmissionRequest transmissionRequest;
            if (transmissionMessage is ITransmissionRequest)
            {
                transmissionRequest = (ITransmissionRequest)transmissionMessage;
            }
            else
            {
                // Perform lookup using header.
                Trace lookupSpan = root.Child();
                lookupSpan.Record(Annotations.ServiceName("Fetch endpoint information"));
                lookupSpan.Record(Annotations.ClientSend());
                try
                {
                    var endpoint = this.lookupService.Lookup(transmissionMessage.GetHeader(), lookupSpan);
                    lookupSpan.Record(
                        Annotations.Tag("transport profile", endpoint.getTransportProfile().getIdentifier()));
                    transmissionRequest = new DefaultTransmissionRequest(transmissionMessage, endpoint);
                }
                catch (HyperwayTransmissionException e)
                {
                    lookupSpan.Record(Annotations.Tag("exception", e.Message));
                    throw;
                }
                finally
                {
                    lookupSpan.Record(Annotations.ClientRecv());
                }
            }

            Trace span = root.Child();
            span.Record(Annotations.ServiceName("send message"));
            span.Record(Annotations.ClientSend());

            // Span span = tracer.newChild(root.context()).name("send message").start();
            ITransmissionResponse transmissionResponse;
            try
            {
                TransportProfile transportProfile = transmissionRequest.GetEndpoint().getTransportProfile();
                IMessageSender messageSender = this.messageSenderFactory.GetMessageSender(transportProfile);
                transmissionResponse = messageSender.Send(transmissionRequest, span);
            }
            catch (HyperwayTransmissionException e)
            {
                span.Record(Annotations.Tag("exception", e.Message));
                throw;
            }
            finally
            {
                span.Record(Annotations.ClientRecv());
            }

            this.statisticsService.Persist(transmissionRequest, transmissionResponse, root);

            return transmissionResponse;
        }
    }

}
