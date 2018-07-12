using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Peppol.Lookup.Api
{
    public interface MetadataFetcher
    {
        FetcherResponse fetch(Uri uri); //throws LookupException, FileNotFoundException;
    }
}
