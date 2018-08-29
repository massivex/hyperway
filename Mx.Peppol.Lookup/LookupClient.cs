using System;
using System.Collections.Generic;
using System.Text;

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

        private MetadataLocator locator;

        private MetadataProvider provider;

        private MetadataFetcher fetcher;

        private MetadataReader reader;

        private CertificateValidator validator;

        public LookupClient(
            MetadataLocator locator,
            MetadataProvider provider,
            MetadataFetcher fetcher,
            MetadataReader reader,
            CertificateValidator validator)
        {
            this.locator = locator;
            this.provider = provider;
            this.fetcher = fetcher;
            this.reader = reader;
            this.validator = validator;
        }

        public ServiceMetadata getServiceMetadata(
            ParticipantIdentifier participantIdentifier,
            DocumentTypeIdentifier documentTypeIdentifier) // throws LookupException, PeppolSecurityException
        {
            Uri location = this.locator.lookup(participantIdentifier);
            Uri provider = this.provider.resolveServiceMetadata(
                location,
                participantIdentifier,
                documentTypeIdentifier);

            FetcherResponse fetcherResponse;
            try
            {
                fetcherResponse = this.fetcher.fetch(provider);
            }
            catch (FileNotFoundException e)
            {
                throw new LookupException(
                    $"Combination of receiver ({participantIdentifier}) and "
                    + $"document type identifier ({documentTypeIdentifier}) is not supported.",
                    e);
            }

            IPotentiallySigned<ServiceMetadata> serviceMetadata = this.reader.parseServiceMetadata(fetcherResponse);

            if (serviceMetadata is Signed<ServiceMetadata>)
            {
                this.validator.validate(Service.Smp, ((Signed<ServiceMetadata>)serviceMetadata).Certificate);
            }

            return serviceMetadata.Content;
        }

        public Endpoint getEndpoint(
            ServiceMetadata serviceMetadata,
            ProcessIdentifier processIdentifier,
            TransportProfile[] transportProfiles) // throws PeppolSecurityException, EndpointNotFoundException
        {
            Endpoint endpoint = serviceMetadata.GetEndpoint(processIdentifier, transportProfiles);

            this.validator.validate(Service.Ap, endpoint.Certificate);

            return endpoint;
        }

        public Endpoint getEndpoint(
            ParticipantIdentifier participantIdentifier,
            DocumentTypeIdentifier documentTypeIdentifier,
            ProcessIdentifier processIdentifier,
            TransportProfile[] transportProfiles) // throws LookupException, PeppolSecurityException, EndpointNotFoundException
        {
            ServiceMetadata serviceMetadata = this.getServiceMetadata(participantIdentifier, documentTypeIdentifier);
            return getEndpoint(serviceMetadata, processIdentifier, transportProfiles);
        }

        public Endpoint
            getEndpoint(Header header, TransportProfile[] transportProfiles) // throws LookupException, PeppolSecurityException, EndpointNotFoundException
        {
            return this.getEndpoint(header.Receiver, header.DocumentType, header.Process, transportProfiles);
        }
    }

}