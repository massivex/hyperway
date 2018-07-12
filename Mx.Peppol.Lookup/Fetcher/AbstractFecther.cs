using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Peppol.Lookup.Fetcher
{
    using Mx.Peppol.Lookup.Api;
    using Mx.Peppol.Mode;

    public abstract class AbstractFetcher : MetadataFetcher
    {

        protected int timeout = 10000;

        public AbstractFetcher(Mode mode)
        {
            timeout = int.Parse(mode.getString("s"));
        }

        public virtual FetcherResponse fetch(Uri uri);
    }

}
