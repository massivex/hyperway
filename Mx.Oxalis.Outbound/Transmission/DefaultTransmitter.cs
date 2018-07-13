using System;
using System.Collections.Generic;
using System.Text;

using zipkin4net.Tracers.Zipkin;

namespace Mx.Oxalis.Outbound.Transmission
{
    using Mx.Oxalis.Api.Lang;
    using Mx.Oxalis.Api.Lookup;
    using Mx.Oxalis.Api.Model;
    using Mx.Oxalis.Api.Outbound;
    using Mx.Oxalis.Api.Statistics;
    using Mx.Oxalis.Api.Transmission;
    using Mx.Oxalis.Commons.Tracing;
    using Mx.Peppol.Common.Model;

    using zipkin4net;

    /**
     * Executes transmission requests by sending the payload to the requested destination.
     * Updates statistics for the transmission using the configured RawStatisticsRepository.
     * <p>
     * Will log an error if the recording of statistics fails for some reason.
     *
     * @author steinar
     * @author thore
     * @author erlend
     */
    public class DefaultTransmitter : Transmitter
    {

        /**
         * Factory used to fetch implementation of required transport profile implementation.
         */
        private readonly MessageSenderFactory messageSenderFactory;

        /**
         * Service to report statistics when transmission is successfully transmitted.
         */
        private readonly StatisticsService statisticsService;

        private readonly TransmissionVerifier transmissionVerifier;

        private readonly LookupService lookupService;

        public DefaultTransmitter(
            MessageSenderFactory messageSenderFactory,
            StatisticsService statisticsService,
            TransmissionVerifier transmissionVerifier,
            LookupService lookupService)
        {
            this.messageSenderFactory = messageSenderFactory;
            this.statisticsService = statisticsService;
            this.transmissionVerifier = transmissionVerifier;
            this.lookupService = lookupService;
        }

        public TransmissionResponse
            transmit(TransmissionMessage transmissionMessage, Trace root) //  throws OxalisTransmissionException
        {
            Trace span = root.Child();
            span.Record(Annotations.ServiceName("transmit"));
            span.Record(Annotations.ClientSend());
            // Span span = tracer.newChild(root.context()).name("transmit").start();
            try
            {
                return perform(transmissionMessage, span);
            }
            finally
            {
                span.Record(Annotations.ClientRecv());
                // span.finish();
            }
        }

        public TransmissionResponse
            transmit(TransmissionMessage transmissionMessage) //throws OxalisTransmissionException
        {
            // Span root = tracer.newTrace().name("transmit").start();
            Trace root = Trace.Create();
            root.Record(Annotations.ServiceName("transmit"));
            root.Record(Annotations.ClientSend());
            try
            {
                return perform(transmissionMessage, root);
            }
            finally
            {
                root.Record(Annotations.ClientRecv());
                // root.finish();
            }
        }

        private TransmissionResponse perform(TransmissionMessage transmissionMessage, Trace root)
            // throws OxalisTransmissionException
        {

            transmissionVerifier.verify(transmissionMessage.getHeader(), Direction.OUT);

            TransmissionRequest transmissionRequest;
            if (transmissionMessage is TransmissionRequest)
            {
                transmissionRequest = (TransmissionRequest)transmissionMessage;
            }
            else
            {
                // Perform lookup using header.

                // Span span = tracer.newChild(root.context()).name("Fetch endpoint information").start();
                Trace lookupSpan = root.Child();
                lookupSpan.Record(Annotations.ServiceName("Fetch endpoint information"));
                lookupSpan.Record(Annotations.ClientSend());
                Endpoint endpoint;
                try
                {
                    endpoint = lookupService.lookup(transmissionMessage.getHeader(), lookupSpan);
                    lookupSpan.Record(
                        Annotations.Tag("transport profile", endpoint.getTransportProfile().getIdentifier()));
                    transmissionRequest = new DefaultTransmissionRequest(transmissionMessage, endpoint);
                }
                catch (OxalisTransmissionException e)
                {
                    lookupSpan.Record(Annotations.Tag("exception", e.Message));
                    throw e;
                }
                finally
                {
                    lookupSpan.Record(Annotations.ClientRecv());
                    // span.finish();
                }
            }

            Trace span = root.Child();
            span.Record(Annotations.ServiceName("send message"));
            span.Record(Annotations.ClientSend());

            // Span span = tracer.newChild(root.context()).name("send message").start();
            TransmissionResponse transmissionResponse;
            try
            {
                TransportProfile transportProfile = transmissionRequest.getEndpoint().getTransportProfile();
                MessageSender messageSender = messageSenderFactory.getMessageSender(transportProfile);
                transmissionResponse = messageSender.send(transmissionRequest, span);
            }
            catch (OxalisTransmissionException e)
            {
                span.Record(Annotations.Tag("exception", e.Message));
                // span.tag("exception", e.getMessage());
                throw e;
            }
            finally
            {
                span.Record(Annotations.ClientRecv());
                // span.finish();
            }

            statisticsService.persist(transmissionRequest, transmissionResponse, root);

            return transmissionResponse;
        }
    }

}
