using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Peppol.Lookup
{
    using Autofac;

    using Mx.Peppol.Lookup.Api;
    using Mx.Peppol.Mode;
    using Mx.Peppol.Security.Api;
    using Mx.Peppol.Security.Util;

    public class LookupClientBuilder
    {

        private Mode mode;

        private MetadataFetcher metadataFetcher;

        private MetadataLocator metadataLocator;

        private CertificateValidator _certificateValidator = EmptyCertificateValidator.INSTANCE;

        private MetadataProvider metadataProvider;

        private MetadataReader metadataReader;

        public static LookupClientBuilder newInstance(Mode mode)
        {
            return new LookupClientBuilder(mode);
        }

        public static LookupClientBuilder forMode(Mode mode) // throws PeppolLoadingException
        {
            //return newInstance(mode)
            //    .certificateValidator(mode.initiate("security.validator.class", CertificateValidator));
            return newInstance(mode).certificateValidator();
        }

        public static LookupClientBuilder forMode(String modeIdentifier, IContainer container) // throws PeppolLoadingException
        {
            return forMode(Mode.of(modeIdentifier, container));
        }

        public static LookupClientBuilder forProduction(IContainer container) // throws PeppolLoadingException
        {
            return forMode(Mode.PRODUCTION, container);
        }

        public static LookupClientBuilder forTest(IContainer container) // throws PeppolLoadingException
        {
            return forMode(Mode.TEST, container);
        }

        LookupClientBuilder(Mode mode)
        {
            this.mode = mode;
        }

        public LookupClientBuilder fetcher(MetadataFetcher metadataFetcher)
        {
            this.metadataFetcher = metadataFetcher;
            return this;
        }

        public LookupClientBuilder fetcher() // throws PeppolLoadingException
        {
            return this.fetcher(this.mode.initiate<MetadataFetcher>());
        }

        public LookupClientBuilder locator(MetadataLocator metadataLocator)
        {
            this.metadataLocator = metadataLocator;
            return this;
        }

        public LookupClientBuilder locator() // throws PeppolLoadingException
        {
            return this.locator(this.mode.initiate<MetadataLocator>());
        }

        public LookupClientBuilder provider(MetadataProvider metadataProvider)
        {
            this.metadataProvider = metadataProvider;
            return this;
        }

        public LookupClientBuilder provider() // throws PeppolLoadingException
        {
            return this.provider(this.mode.initiate<MetadataProvider>());
        }

        public LookupClientBuilder reader(MetadataReader metadataReader)
        {
            this.metadataReader = metadataReader;
            return this;
        }

        public LookupClientBuilder reader() // throws PeppolLoadingException
        {
            return this.reader(this.mode.initiate<MetadataReader>());
        }

        public LookupClientBuilder certificateValidator(CertificateValidator certificateValidator)
        {
            this._certificateValidator = certificateValidator;
            return this;
        }

        public LookupClientBuilder certificateValidator()
        {
            return this.certificateValidator(this.mode.initiate<CertificateValidator>());
        }

        public LookupClient build() // throws PeppolLoadingException
        {
            if (this.metadataLocator == null)
            {
                // locator(mode.initiate("lookup.locator.class", MetadataLocator.class));
                this.locator();
            }

            if (this.metadataProvider == null)
            {
                // provider(mode.initiate("lookup.provider.class", MetadataProvider.class));
                this.provider();
    }

            if (this.metadataFetcher == null)
            {
                // fetcher(mode.initiate("lookup.fetcher.class", MetadataFetcher.class));
                this.fetcher();
            }

            if (this.metadataReader == null)
            {
                // reader(mode.initiate("lookup.reader.class", MetadataReader.class));
                this.reader();
            }

            return new LookupClient(
                this.metadataLocator,
                this.metadataProvider,
                this.metadataFetcher,
                this.metadataReader,
                this._certificateValidator);
        }
    }

}
