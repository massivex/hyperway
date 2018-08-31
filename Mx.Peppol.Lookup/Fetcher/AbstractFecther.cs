using System;

namespace Mx.Peppol.Lookup.Fetcher
{
    using Mx.Peppol.Lookup.Api;
    using Mx.Peppol.Mode;

    public abstract class AbstractFetcher : IMetadataFetcher
    {
        protected int Timeout;

        protected AbstractFetcher(Mode mode)
        {
            var timeoutText = mode.GetValue("lookup.fetcher.timeouts");
            this.Timeout = !string.IsNullOrWhiteSpace(timeoutText) ? int.Parse(timeoutText) : 10000;
        }

        public abstract FetcherResponse Fetch(Uri uri);
    }

}
