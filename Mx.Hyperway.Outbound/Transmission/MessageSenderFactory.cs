﻿namespace Mx.Hyperway.Outbound.Transmission
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Autofac;

    using Mx.Hyperway.Api.Lang;
    using Mx.Hyperway.Api.Outbound;
    using Mx.Peppol.Common.Model;
    using Mx.Peppol.Mode;

    /**
     * Factory orchestrating available implementations of transport profiles.
     *
     * @author erlend
     */
    public class MessageSenderFactory
    {

        private static readonly log4net.ILog LOGGER = log4net.LogManager.GetLogger(typeof(MessageSenderFactory));
        // private static final Logger LOGGER = LoggerFactory.getLogger(MessageSenderFactory.class);

        /**
         * Guice injector used to load MessageSender implementation when needed, allows use of non-singleton
         * implementations. It is not considered best practice to inject injector like this, however in this case is this
         * suitable based on our requirements.
         */
        private readonly IComponentContext injector;

        /**
         * Map of configurations for supported transport profiles.
         */
        private readonly Dictionary<TransportProfile, TransportConfig> configMap;

        /**
         * Prioritized list of supported transport profiles.
         */
        private readonly List<TransportProfile> prioritizedTransportProfiles;

        // @Inject
        public MessageSenderFactory(IComponentContext injector, Mode config)
        {
            this.injector = injector;

            this.configMap = config.Defaults.Transports.Where(x => x.Enabled).OrderBy(x => x.Weight)
                .ToDictionary(t => TransportProfile.of(t.Profile), x => x);

            this.prioritizedTransportProfiles = config.Defaults.Transports.Where(x => x.Enabled).OrderBy(x => x.Weight)
                .Select(x => TransportProfile.of(x.Profile)).ToList();

            //// Construct map of configuration for detected transport profiles.
            //configMap = config.getObject("transport").keySet().stream()
            //        .map(key->config.getConfig(String.format("transport.%s", key)))
            //        .collect(Collectors.toMap(c->TransportProfile.of(c.getString("profile")), Function.identity()));

            //// Create prioritized list of transport profiles.
            //prioritizedTransportProfiles = Collections.unmodifiableList(configMap.values().stream()
            //        .filter(o-> !o.hasPath("enabled") || o.getBoolean("enabled"))
            //        .sorted((o1, o2)->Integer.compare(o2.getInt("weight"), o1.getInt("weight")))
            //        .map(o->o.getString("profile"))
            //        .map(TransportProfile::of)
            //        .collect(Collectors.toList()));

            // Logging list of prioritized transport profiles supported.
            LOGGER.Info("Prioritized list of transport profiles:");
            this.prioritizedTransportProfiles.ForEach(tp => LOGGER.InfoFormat("=> {0}", tp.getIdentifier()));
        }

        /**
         * Fetch list of supported transport profiles ordered by priority.
         *
         * @return List of supported transport profiles.
         */
        public List<TransportProfile> getPrioritizedTransportProfiles()
        {
            return this.prioritizedTransportProfiles;
        }

        /**
         * Fetch identifier used in named annotation for the implementation of requested transport profile.
         */
        public String getSender(TransportProfile transportProfile)
        {
            if (!this.prioritizedTransportProfiles.Any(x => x.Equals(transportProfile)))
            {
                throw new HyperwayTransmissionException(
                    $"Transport protocol '{transportProfile.getIdentifier()}' not supported.");
            }

            return this.configMap.Where(x => x.Key.Equals(transportProfile)).Select(x => x.Value.Sender).First();
        }

        /**
         * Fetch MessageSender implementing from provided transport profile.
         */
        public MessageSender getMessageSender(TransportProfile transportProfile)
        {
            return this.injector.ResolveKeyed<MessageSender>(this.getSender(transportProfile));
        }
    }

}