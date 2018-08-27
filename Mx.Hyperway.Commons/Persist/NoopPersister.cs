using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Hyperway.Commons.Persist
{
    using System.IO;

    using Mx.Hyperway.Api.Inbound;
    using Mx.Hyperway.Api.Model;
    using Mx.Hyperway.Api.Persist;
    using Mx.Peppol.Common.Model;

    public class NoopPersister : IPersisterHandler
    {
        public FileInfo Persist(TransmissionIdentifier transmissionIdentifier, Header header, Stream inputStream)
        {
            // Iterate all stream
            inputStream.Seek(inputStream.Length, SeekOrigin.Begin);
            return null;
        }

        public void Persist(IInboundMetadata inboundMetadata, FileInfo payloadPath)
        {
            // No operation (intended)
        }
    }

}
