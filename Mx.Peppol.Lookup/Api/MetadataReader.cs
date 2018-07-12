using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Peppol.Lookup.Api
{
    using Mx.Peppol.Common.Api;
    using Mx.Peppol.Common.Model;

    public interface MetadataReader
    {

        List<ServiceReference> parseServiceGroup(FetcherResponse fetcherResponse); // throws LookupException;

        PotentiallySigned<ServiceMetadata>
            parseServiceMetadata(FetcherResponse fetcherResponse); // throws LookupException, PeppolSecurityException;
    }

}
