﻿namespace Mx.Hyperway.Outbound
{
    using Autofac;

    using Mx.Hyperway.Api.Evidence;
    using Mx.Hyperway.Api.Lookup;
    using Mx.Hyperway.Api.Outbound;
    using Mx.Hyperway.Outbound.Transmission;

    public class HyperwayOutboundComponent
    {
        private readonly IComponentContext context;

        public HyperwayOutboundComponent(IComponentContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Retrieves instances of TransmissionRequestBuilder, while not exposing Google Guice to the outside
        /// </summary>
        public TransmissionRequestBuilder GetTransmissionRequestBuilder()
        {
            return this.context.Resolve<TransmissionRequestBuilder>();
        }

        public TransmissionRequestFactory GetTransmissionRequestFactory()
        {
            return this.context.Resolve<TransmissionRequestFactory>();
        }

        /// <summary>
        /// Retrieves instance of LookupService, without revealing intern object dependency injection.
        /// </summary>
        public ILookupService GetLookupService()
        {
            return this.context.Resolve<ILookupService>();
        }

        /// <summary>
        /// Retrieves instance of DefaultTransmitter, without revealing intern object dependency injection.
        /// </summary>
        public ITransmitter GetTransmitter()
        {
            return this.context.Resolve<ITransmitter>();
        }

        public IEvidenceFactory GetEvidenceFactory()
        {
            return this.context.Resolve<IEvidenceFactory>();
        }

        public ITransmissionService GetTransmissionService()
        {
            return this.context.Resolve<ITransmissionService>();
        }
    }

}
