namespace Mx.Peppol.Lookup.Api
{
    using Mx.Peppol.Common.Api;
    using Mx.Peppol.Common.Model;

    public interface IMetadataReader
    {
        IPotentiallySigned<ServiceMetadata> ParseServiceMetadata(FetcherResponse fetcherResponse);
    }

}
