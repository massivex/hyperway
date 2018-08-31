using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Peppol.Lookup.Api
{
    public interface IMetadataFetcher
    {
        FetcherResponse Fetch(Uri uri);
    }
}
