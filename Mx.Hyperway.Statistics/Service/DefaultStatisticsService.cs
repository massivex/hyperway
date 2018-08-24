namespace Mx.Hyperway.Statistics.Service
{
    using System;

    using log4net;

    using Mx.Hyperway.Api.Inbound;
    using Mx.Hyperway.Api.Model;
    using Mx.Hyperway.Api.Outbound;
    using Mx.Hyperway.Api.Statistics;
    using Mx.Hyperway.Commons.Security;
    using Mx.Hyperway.Statistics.Model;

    using Org.BouncyCastle.X509;

    using zipkin4net;

    public class DefaultStatisticsService : StatisticsService
    {

        private static readonly ILog logger = LogManager.GetLogger(typeof(DefaultStatisticsService));

        private readonly RawStatisticsRepository rawStatisticsRepository;

        private readonly AccessPointIdentifier ourAccessPointIdentifier;

        public DefaultStatisticsService(RawStatisticsRepository rawStatisticsRepository, X509Certificate certificate)
        {
            this.rawStatisticsRepository = rawStatisticsRepository;
            this.ourAccessPointIdentifier = new AccessPointIdentifier(CertificateUtils.extractCommonName(certificate));
        }

        public void persist(
            ITransmissionRequest transmissionRequest,
            TransmissionResponse transmissionResponse,
            Trace root)
        {
            Trace span = root.Child();
            span.Record(Annotations.ServiceName("persist statistics"));
            span.Record(Annotations.ClientSend());
            try
            {
                RawStatisticsBuilder builder = new RawStatisticsBuilder()
                    .AccessPointIdentifier(this.ourAccessPointIdentifier).Direction(Direction.OUT)
                    .DocumentType(transmissionResponse.getHeader().getDocumentType())
                    .Sender(transmissionResponse.getHeader().getSender())
                    .Receiver(transmissionResponse.getHeader().getReceiver())
                    .Profile(transmissionResponse.getHeader().getProcess())
                    .Date(transmissionResponse.getTimestamp()); // Time stamp of reception of the receipt

                // If we know the CN name of the destination AP, supply that
                // as the channel id otherwise use the protocol name
                if (transmissionRequest.GetEndpoint().getCertificate() != null)
                {
                    String accessPointIdentifierValue =
                        CertificateUtils.extractCommonName(transmissionRequest.GetEndpoint().getCertificate());
                    builder.Channel(new ChannelId(accessPointIdentifierValue));
                }
                else
                {
                    String protocolName = transmissionRequest.GetEndpoint().getTransportProfile().getIdentifier();
                    builder.Channel(new ChannelId(protocolName));
                }

                DefaultRawStatistics rawStatistics = builder.Build();
                this.rawStatisticsRepository.persist(rawStatistics);
            }
            catch (Exception ex)
            {
                span.Record(Annotations.Tag("exception", ex.Message));
                logger.Error($"Persisting DefaultRawStatistics about oubound transmission failed : {ex.Message}", ex);
            }
            finally
            {
                span.Record(Annotations.ClientRecv());

            }
        }

        public void persist(InboundMetadata inboundMetadata)
        {
            // Persists raw statistics when message was received (ignore if stats couldn't be persisted, just warn)
            try
            {
                DefaultRawStatistics rawStatistics = new RawStatisticsBuilder()
                    .AccessPointIdentifier(this.ourAccessPointIdentifier).Direction(Direction.IN)
                    .DocumentType(inboundMetadata.getHeader().getDocumentType())
                    .Sender(inboundMetadata.getHeader().getSender()).Receiver(inboundMetadata.getHeader().getReceiver())
                    .Profile(inboundMetadata.getHeader().getProcess()).Channel(new ChannelId("AS2")).Build();

                this.rawStatisticsRepository.persist(rawStatistics);
            }
            catch (Exception e)
            {
                logger.Error($"Unable to persist statistics for {inboundMetadata}\n {e.Message}", e);
            }
        }
    }

}
