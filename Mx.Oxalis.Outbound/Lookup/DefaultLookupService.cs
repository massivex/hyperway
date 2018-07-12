using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Oxalis.Outbound.Lookup
{
    using Autofac;
    using Autofac.Features.AttributeFilters;

    using Mx.Oxalis.Api.Lang;
    using Mx.Oxalis.Api.Lookup;
    using Mx.Peppol.Common.Lang;
    using Mx.Peppol.Common.Model;
    using Mx.Peppol.Lookup;
    using Mx.Peppol.Lookup.Api;

    public class DefaultLookupService : LookupService
    {

        /**
         * LookupClient provided by VEFA PEPPOL project.
         */
        private readonly LookupClient lookupClient;

        /**
         * Prioritized list of supported transport profiles detected in
         * {@link eu.peppol.outbound.transmission.MessageSenderFactory}.
         */
        private readonly TransportProfile[] transportProfiles;

        public DefaultLookupService(
            LookupClient lookupClient,
            [KeyFilter("prioritized")] List<TransportProfile> transportProfiles)
        {
            this.lookupClient = lookupClient;
            this.transportProfiles = transportProfiles.ToArray();
        }

        public Endpoint lookup(Header header) // throws OxalisTransmissionException
        {
            try
            {
                return lookupClient.getEndpoint(header, transportProfiles);
            }
            catch (Exception e) when (e is LookupException || e is PeppolSecurityException || e is EndpointNotFoundException e) {
                throw new OxalisTransmissionException(e.Message, e);
            }

        }
    }
}
