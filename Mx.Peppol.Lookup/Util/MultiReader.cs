using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Peppol.Lookup.Util
{
    using System.IO;
    using System.Reflection;

    using Autofac.Features.AttributeFilters;

    using Mx.Peppol.Common.Api;
    using Mx.Peppol.Common.Model;
    using Mx.Peppol.Lookup.Api;
    using Mx.Peppol.Security.Util;
    using Mx.Tools;

    public class MultiReader : MetadataReader
    {
        private readonly IList<MetadataReader> metadataReaders;

        public MultiReader([KeyFilter("reader-protocols")] IList<MetadataReader> metadataReaders)
        {
            this.metadataReaders = metadataReaders;
        }
        // private static readonly List<MetadataReader> METADATA_READERS = Lists.newArrayList(ServiceLoader.load(MetadataReader.class));

        public List<ServiceReference> parseServiceGroup(FetcherResponse fetcherResponse) // throws LookupException
        {
            FetcherResponse response = fetcherResponse;

            if (response.Namespace == null)
            {
                response = detect(response);
            }

            foreach (MetadataReader metadataReader in this.metadataReaders)
            {
                NamespaceAttribute nsAttr = (NamespaceAttribute) metadataReader.GetType().GetCustomAttribute(typeof(NamespaceAttribute), true);
                if (nsAttr.Value.EqualsIgnoreCase(response.Namespace)) {
                    return metadataReader.parseServiceGroup(response);
                }

            }

            throw new LookupException(String.Format("Unknown namespace: {0}", response.Namespace));
        }

        public IPotentiallySigned<ServiceMetadata>
            parseServiceMetadata(FetcherResponse fetcherResponse) // throws LookupException, PeppolSecurityException
        {
            FetcherResponse response = fetcherResponse;

            if (response.Namespace == null)
            {
                response = detect(response);
            }

            foreach (MetadataReader metadataReader in this.metadataReaders) {
                NamespaceAttribute nsAttr = (NamespaceAttribute)metadataReader.GetType().GetCustomAttribute(typeof(NamespaceAttribute), true);
                if (nsAttr.Value.EqualsIgnoreCase(response.Namespace)) {
                    return metadataReader.parseServiceMetadata(response);
                }
            }

            throw new LookupException(String.Format("Unknown namespace: {0}", response.Namespace));
        }

        public FetcherResponse detect(FetcherResponse fetcherResponse) // throws LookupException
        {
            try
            {
                byte[] fileContent = fetcherResponse.InputStream.ToBuffer();

                String ns = XmlUtils.extractRootNamespace(Encoding.UTF8.GetString(fileContent));
                if (ns != null)
                return new FetcherResponse(fileContent.ToStream(),  ns);

                throw new LookupException("Unable to detect namespace.");
            }
            catch (IOException e)
            {
                throw new LookupException(e.Message, e);
            }
        }
    }

}
