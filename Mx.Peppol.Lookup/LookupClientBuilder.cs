namespace Mx.Peppol.Lookup
{
    using Mx.Peppol.Lookup.Api;
    using Mx.Peppol.Mode;
    using Mx.Peppol.Security.Api;
    using Mx.Peppol.Security.Util;

    public class LookupClientBuilder
    {

        private readonly Mode mode;

        private IMetadataFetcher metadataFetcher;

        private IMetadataLocator metadataLocator;

        private ICertificateValidator validator = EmptyCertificateValidator.Instance;

        private IMetadataProvider metadataProvider;

        private IMetadataReader metadataReader;

        public static LookupClientBuilder NewInstance(Mode mode)
        {
            return new LookupClientBuilder(mode);
        }

        public static LookupClientBuilder ForMode(Mode mode)
        {
            return NewInstance(mode).CertificateValidator();
        }

        private LookupClientBuilder(Mode mode)
        {
            this.mode = mode;
        }

        public LookupClientBuilder Fetcher(IMetadataFetcher value)
        {
            this.metadataFetcher = value;
            return this;
        }

        public LookupClientBuilder Fetcher()
        {
            return this.Fetcher(this.mode.Resolve<IMetadataFetcher>());
        }

        public LookupClientBuilder Locator(IMetadataLocator value)
        {
            this.metadataLocator = value;
            return this;
        }

        public LookupClientBuilder Locator()
        {
            return this.Locator(this.mode.Resolve<IMetadataLocator>());
        }

        public LookupClientBuilder Provider(IMetadataProvider value)
        {
            this.metadataProvider = value;
            return this;
        }

        public LookupClientBuilder Provider()
        {
            return this.Provider(this.mode.Resolve<IMetadataProvider>());
        }

        public LookupClientBuilder Reader(IMetadataReader value)
        {
            this.metadataReader = value;
            return this;
        }

        public LookupClientBuilder Reader()
        {
            return this.Reader(this.mode.Resolve<IMetadataReader>());
        }

        public LookupClientBuilder CertificateValidator(ICertificateValidator certificateValidator)
        {
            this.validator = certificateValidator;
            return this;
        }

        public LookupClientBuilder CertificateValidator()
        {
            return this.CertificateValidator(this.mode.Resolve<ICertificateValidator>());
        }

        public LookupClient Build()
        {
            if (this.metadataLocator == null)
            {
                this.Locator();
            }

            if (this.metadataProvider == null)
            {
                this.Provider();
    }

            if (this.metadataFetcher == null)
            {
                this.Fetcher();
            }

            if (this.metadataReader == null)
            {
                this.Reader();
            }

            return new LookupClient(
                this.metadataLocator,
                this.metadataProvider,
                this.metadataFetcher,
                this.metadataReader,
                this.validator);
        }
    }

}
