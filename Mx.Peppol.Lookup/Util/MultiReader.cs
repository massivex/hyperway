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

    public class MultiReader : IMetadataReader
    {
        private readonly IList<IMetadataReader> metadataReaders;

        public MultiReader([KeyFilter("reader-protocols")] IList<IMetadataReader> metadataReaders)
        {
            this.metadataReaders = metadataReaders;
        }

        public IPotentiallySigned<ServiceMetadata> ParseServiceMetadata(FetcherResponse fetcherResponse)
        {
            FetcherResponse response = fetcherResponse;

            if (response.Namespace == null)
            {
                response = this.Detect(response);
            }

            foreach (IMetadataReader metadataReader in this.metadataReaders) {
                NamespaceAttribute nsAttr = (NamespaceAttribute)metadataReader.GetType().GetCustomAttribute(typeof(NamespaceAttribute), true);
                if (nsAttr.Value.EqualsIgnoreCase(response.Namespace)) {
                    return metadataReader.ParseServiceMetadata(response);
                }
            }

            throw new LookupException(String.Format("Unknown namespace: {0}", response.Namespace));
        }

        public FetcherResponse Detect(FetcherResponse fetcherResponse) // throws LookupException
        {
            try
            {
                byte[] fileContent = fetcherResponse.InputStream.ToBuffer();

                String ns = XmlUtils.ExtractRootNamespace(Encoding.UTF8.GetString(fileContent));
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
