using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Oxalis.Outbound
{
    using Autofac;

    using Mx.Oxalis.Api.Evidence;
    using Mx.Oxalis.Api.Lookup;
    using Mx.Oxalis.Api.Outbound;
    using Mx.Oxalis.Outbound.Transmission;

    public class OxalisOutboundComponent
    {
        private readonly IComponentContext context;

        public OxalisOutboundComponent(IComponentContext context)
        {
            this.context = context;
        }

        // private Injector injector = GuiceModuleLoader.initiate();

        /**
         * Retrieves instances of TransmissionRequestBuilder, while not exposing Google Guice to the outside
         *
         * @return instance of TransmissionRequestBuilder
         */
        public TransmissionRequestBuilder getTransmissionRequestBuilder()
        {
            return this.context.Resolve<TransmissionRequestBuilder>();
        }

        public TransmissionRequestFactory getTransmissionRequestFactory()
        {
            return this.context.Resolve<TransmissionRequestFactory>();
            //.getInstance(TransmissionRequestFactory.class);
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
         *
         * @return instance of Transmitter
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

        /**
         * Provides access to the Google Guice injector in order to reuse the component with other components that also uses
         * Google Guice.
         *
         * @return
         */
        //public Injector getInjector()
        //{
        //    return injector;
        //}

        public TransmissionService getTransmissionService()
        {
            return this.context.Resolve<TransmissionService>();
            //return injector.getInstance(TransmissionService.class);
        }
    }

}
