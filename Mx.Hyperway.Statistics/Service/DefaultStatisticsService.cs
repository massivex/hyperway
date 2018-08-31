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

    public class DefaultStatisticsService : IStatisticsService
    {

        private static readonly ILog Logger = LogManager.GetLogger(typeof(DefaultStatisticsService));

        private readonly IRawStatisticsRepository rawStatisticsRepository;

        private readonly AccessPointIdentifier ourAccessPointIdentifier;

        public DefaultStatisticsService(IRawStatisticsRepository rawStatisticsRepository, X509Certificate certificate)
        {
            this.rawStatisticsRepository = rawStatisticsRepository;
            this.ourAccessPointIdentifier = new AccessPointIdentifier(CertificateUtils.ExtractCommonName(certificate));
        }

        public void Persist(
            ITransmissionRequest transmissionRequest,
            ITransmissionResponse transmissionResponse,
            Trace root)
        {
            Trace span = root.Child();
            span.Record(Annotations.ServiceName("persist statistics"));
            span.Record(Annotations.ClientSend());
            try
            {
                RawStatisticsBuilder builder = new RawStatisticsBuilder()
                    .AccessPointIdentifier(this.ourAccessPointIdentifier).Direction(Direction.OUT)
                    .DocumentType(transmissionResponse.GetHeader().DocumentType)
                    .Sender(transmissionResponse.GetHeader().Sender)
                    .Receiver(transmissionResponse.GetHeader().Receiver)
                    .Profile(transmissionResponse.GetHeader().Process)
                    .Date(transmissionResponse.GetTimestamp()); // Time stamp of reception of the receipt

                // If we know the CN name of the destination AP, supply that
                // as the channel id otherwise use the protocol name
                if (transmissionRequest.GetEndpoint().Certificate!= null)
                {
                    String accessPointIdentifierValue =
                        CertificateUtils.ExtractCommonName(transmissionRequest.GetEndpoint().Certificate);
                    builder.Channel(new ChannelId(accessPointIdentifierValue));
                }
                else
                {
                    String protocolName = transmissionRequest.GetEndpoint().TransportProfile.Identifier;
                    builder.Channel(new ChannelId(protocolName));
                }

                DefaultRawStatistics rawStatistics = builder.Build();
                this.rawStatisticsRepository.Persist(rawStatistics);
            }
            catch (Exception ex)
            {
                span.Record(Annotations.Tag("exception", ex.Message));
                Logger.Error($"Persisting DefaultRawStatistics about oubound transmission failed : {ex.Message}", ex);
            }
            finally
            {
                span.Record(Annotations.ClientRecv());

            }
        }

        public void Persist(IInboundMetadata inboundMetadata)
        {
            // Persists raw statistics when message was received (ignore if stats couldn't be persisted, just warn)
            try
            {
                DefaultRawStatistics rawStatistics = new RawStatisticsBuilder()
                    .AccessPointIdentifier(this.ourAccessPointIdentifier).Direction(Direction.IN)
                    .DocumentType(inboundMetadata.GetHeader().DocumentType)
                    .Sender(inboundMetadata.GetHeader().Sender).Receiver(inboundMetadata.GetHeader().Receiver)
                    .Profile(inboundMetadata.GetHeader().Process).Channel(new ChannelId("AS2")).Build();

                this.rawStatisticsRepository.Persist(rawStatistics);
            }
            catch (Exception e)
            {
                Logger.Error($"Unable to persist statistics for {inboundMetadata}\n {e.Message}", e);
            }
        }
    }

}
