namespace Mx.Hyperway.Outbound.Transmission
{
    using System.Collections.Generic;
    using System.Linq;

    using Autofac;

    using Mx.Hyperway.Api.Lang;
    using Mx.Hyperway.Api.Outbound;
    using Mx.Peppol.Common.Model;
    using Mx.Peppol.Mode;

    /// <summary>Factory orchestrating available implementations of transport profiles.</summary>
    public class MessageSenderFactory
    {

        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(typeof(MessageSenderFactory));

        private readonly IComponentContext injector;

        /// <summary>Map of configurations for supported transport profiles.</summary>
        private readonly Dictionary<TransportProfile, TransportConfig> configMap;

        /// <summary>Prioritized list of supported transport profiles.</summary>
        private readonly List<TransportProfile> prioritizedTransportProfiles;

        public MessageSenderFactory(IComponentContext injector, Mode config)
        {
            this.injector = injector;

            this.configMap = config.Defaults.Transports.Where(x => x.Enabled).OrderBy(x => x.Weight)
                .ToDictionary(t => TransportProfile.of(t.Profile), x => x);

            this.prioritizedTransportProfiles = config.Defaults.Transports.Where(x => x.Enabled).OrderBy(x => x.Weight)
                .Select(x => TransportProfile.of(x.Profile)).ToList();

            // Logging list of prioritized transport profiles supported.
            Logger.Info("Prioritized list of transport profiles:");
            this.prioritizedTransportProfiles.ForEach(tp => Logger.InfoFormat("=> {0}", tp.getIdentifier()));
        }


        /// <summary>
        /// Fetch identifier used in named annotation for the implementation of requested transport profile.
        /// </summary>
        public string GetSender(TransportProfile transportProfile)
        {
            if (!this.prioritizedTransportProfiles.Any(x => x.Equals(transportProfile)))
            {
                throw new HyperwayTransmissionException(
                    $"Transport protocol '{transportProfile.getIdentifier()}' not supported.");
            }

            return this.configMap.Where(x => x.Key.Equals(transportProfile)).Select(x => x.Value.Sender).First();
        }
        
        
        /// <summary>
        /// Fetch MessageSender implementing from provided transport profile.
        /// </summary>
        public MessageSender GetMessageSender(TransportProfile transportProfile)
        {
            return this.injector.ResolveKeyed<MessageSender>(this.GetSender(transportProfile));
        }
    }

}
