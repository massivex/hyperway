﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Peppol.Lookup
{
    using System.IO;

    using Mx.Peppol.Common.Api;
    using Mx.Peppol.Common.Code;
    using Mx.Peppol.Common.Model;
    using Mx.Peppol.Lookup.Api;

    public class LookupClient
    {

        private MetadataLocator locator;

        private MetadataProvider provider;

        private MetadataFetcher fetcher;

        private MetadataReader reader;

        private CertificateValidator validator;

        LookupClient(
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

        [Obsolete]
        public List<DocumentTypeIdentifier>
            getDocumentIdentifiers(ParticipantIdentifier participantIdentifier) // throws LookupException
        {
            List<DocumentTypeIdentifier> documentTypeIdentifiers = new List<DocumentTypeIdentifier>();

            foreach (ServiceReference serviceReference in this.getServiceReferences(participantIdentifier))
            {
                documentTypeIdentifiers.add(serviceReference.getDocumentTypeIdentifier());
            }

            return documentTypeIdentifiers;
        }

        public List<ServiceReference>
            getServiceReferences(ParticipantIdentifier participantIdentifier) // throws LookupException
        {
            Uri location = this.locator.lookup(participantIdentifier);
            Uri provider = this.provider.resolveDocumentIdentifiers(location, participantIdentifier);

            FetcherResponse fetcherResponse;
            try
            {
                fetcherResponse = this.fetcher.fetch(provider);
            }
            catch (FileNotFoundException e)
            {
                throw new LookupException(
                    String.format("Receiver (%s) not found.", participantIdentifier.toString()),
                    e);
            }

            return this.reader.parseServiceGroup(fetcherResponse);
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

            PotentiallySigned<ServiceMetadata> serviceMetadata = this.reader.parseServiceMetadata(fetcherResponse);

            if (serviceMetadata is Signed<ServiceMetadata>)
            {
                this.validator.validate(Service.SMP, ((Signed<ServiceMetadata>)serviceMetadata).Certificate);
            }

            return serviceMetadata.Content;
        }

        public Endpoint getEndpoint(
            ServiceMetadata serviceMetadata,
            ProcessIdentifier processIdentifier,
            TransportProfile[] transportProfiles) // throws PeppolSecurityException, EndpointNotFoundException
        {
            Endpoint endpoint = serviceMetadata.getEndpoint(processIdentifier, transportProfiles);

            this.validator.validate(Service.AP, endpoint.getCertificate());

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
            return this.getEndpoint(header.getReceiver(), header.getDocumentType(), header.getProcess(), transportProfiles);
        }
    }

}