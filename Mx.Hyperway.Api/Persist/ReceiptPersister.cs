using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Hyperway.Api.Persist
{
    using System.IO;

    using Mx.Hyperway.Api.Inbound;

    public interface ReceiptPersister
    {
        void persist(InboundMetadata inboundMetadata, FileInfo payloadPath);
    }
}
