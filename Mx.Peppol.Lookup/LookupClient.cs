using System;

namespace Mx.Peppol.Lookup
{
    using System.IO;

    using Mx.Peppol.Common.Api;
    using Mx.Peppol.Common.Code;
    using Mx.Peppol.Common.Model;
    using Mx.Peppol.Lookup.Api;
    using Mx.Peppol.Security.Api;

    public class LookupClient
    {

        private readonly IMetadataLocator locator;

        private readonly IMetadataProvider provider;

        private readonly IMetadataFetcher fetcher;

        private readonly IMetadataReader reader;

        private readonly ICertificateValidator validator;

        public LookupClient(
            IMetadataLocator locator,
            IMetadataProvider provider,
            IMetadataFetcher fetcher,
            IMetadataReader reader,
            ICertificateValidator validator)
        {
            this.locator = locator;
            this.provider = provider;
            this.fetcher = fetcher;
            this.reader = reader;
            this.validator = validator;
        }

        public ServiceMetadata GetServiceMetadata(
            ParticipantIdentifier participantIdentifier,
            DocumentTypeIdentifier documentTypeIdentifier) // throws LookupException, PeppolSecurityException
        {
            Uri location = this.locator.Lookup(participantIdentifier);
            Uri providerUrl = this.provider.ResolveServiceMetadata(
                location,
                participantIdentifier,
                documentTypeIdentifier);

            FetcherResponse fetcherResponse;
            try
            {
                fetcherResponse = this.fetcher.Fetch(providerUrl);
            }
            catch (FileNotFoundException e)
            {
                throw new LookupException(
                    $"Combination of receiver ({participantIdentifier}) and "
                    + $"document type identifier ({documentTypeIdentifier}) is not supported.",
                    e);
            }

            IPotentiallySigned<ServiceMetadata> serviceMetadata = this.reader.ParseServiceMetadata(fetcherResponse);

            if (serviceMetadata is Signed<ServiceMetadata>)
            {
                this.validator.Validate(Service.Smp, ((Signed<ServiceMetadata>)serviceMetadata).Certificate);
            }

            return serviceMetadata.Content;
        }

        public Endpoint GetEndpoint(
            ServiceMetadata serviceMetadata,
            ProcessIdentifier processIdentifier,
            TransportProfile[] transportProfiles) // throws PeppolSecurityException, EndpointNotFoundException
        {
            Endpoint endpoint = serviceMetadata.GetEndpoint(processIdentifier, transportProfiles);

            this.validator.Validate(Service.Ap, endpoint.Certificate);

            return endpoint;
        }

        public Endpoint GetEndpoint(
            ParticipantIdentifier participantIdentifier,
            DocumentTypeIdentifier documentTypeIdentifier,
            ProcessIdentifier processIdentifier,
            TransportProfile[] transportProfiles) // throws LookupException, PeppolSecurityException, EndpointNotFoundException
        {
            ServiceMetadata serviceMetadata = this.GetServiceMetadata(participantIdentifier, documentTypeIdentifier);
            return this.GetEndpoint(serviceMetadata, processIdentifier, transportProfiles);
        }

        public Endpoint
            GetEndpoint(Header header, TransportProfile[] transportProfiles) // throws LookupException, PeppolSecurityException, EndpointNotFoundException
        {
            return this.GetEndpoint(header.Receiver, header.DocumentType, header.Process, transportProfiles);
        }
    }

}