namespace Mx.Hyperway.Outbound
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

        /**
         * Retrieves instances of TransmissionRequestBuilder, while not exposing Google Guice to the outside
         */
        public TransmissionRequestBuilder getTransmissionRequestBuilder()
        {
            return this.context.Resolve<TransmissionRequestBuilder>();
        }

        public TransmissionRequestFactory getTransmissionRequestFactory()
        {
            return this.context.Resolve<TransmissionRequestFactory>();
        }

        /**
         * Retrieves instance of LookupService, without revealing intern object dependency injection.
         */
        public LookupService getLookupService()
        {
            // return injector.getInstance(LookupService.class);
            return this.context.Resolve<LookupService>();
        }

        /**
         * Retrieves instance of DefaultTransmitter, without revealing intern object dependency injection.
         */
        public Transmitter getTransmitter()
        {
            return this.context.Resolve<Transmitter>();
            // return context.getInstance(Transmitter.class);
        }

        public EvidenceFactory getEvidenceFactory()
        {
            return this.context.Resolve<EvidenceFactory>();
            // return injector.getInstance(EvidenceFactory.class);
        }

        public TransmissionService getTransmissionService()
        {
            return this.context.Resolve<TransmissionService>();
        }
    }

}
