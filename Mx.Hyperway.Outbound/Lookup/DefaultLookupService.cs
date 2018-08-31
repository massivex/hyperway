namespace Mx.Hyperway.Outbound.Lookup
{
    using System;
    using System.Collections.Generic;

    using Autofac.Features.AttributeFilters;

    using Mx.Hyperway.Api.Lang;
    using Mx.Hyperway.Api.Lookup;
    using Mx.Peppol.Common.Lang;
    using Mx.Peppol.Common.Model;
    using Mx.Peppol.Lookup;
    using Mx.Peppol.Lookup.Api;
    using Mx.Peppol.Security.Lang;

    using zipkin4net;

    public class DefaultLookupService : ILookupService
    {
        /// <summary>
        /// LookupClient provided by VEFA PEPPOL project. 
        /// </summary>
        private readonly LookupClient lookupClient;

        /// <summary>
        /// Prioritized list of supported transport profiles detected in
        /// <see cref="Transmission.MessageSenderFactory" />
        /// </summary>
        private readonly TransportProfile[] transportProfiles;

        public DefaultLookupService(
            LookupClient lookupClient,
            [KeyFilter("prioritized")] List<TransportProfile> transportProfiles)
        {
            this.lookupClient = lookupClient;
            this.transportProfiles = transportProfiles.ToArray();
        }

        public Endpoint Lookup(Header header)
        {
            try
            {
                return this.lookupClient.GetEndpoint(header, this.transportProfiles);
            }
            catch (Exception e) when (e is LookupException || e is PeppolSecurityException || e is EndpointNotFoundException) {
                throw new HyperwayTransmissionException(e.Message, e);
            }

        }

        public Endpoint Lookup(Header header, Trace root)
        {
            return this.Lookup(header);
        }
    }
}
