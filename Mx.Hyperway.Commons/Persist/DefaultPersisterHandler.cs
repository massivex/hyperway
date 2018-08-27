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

    public class DefaultPersisterHandler : IPersisterHandler
    {

        private IPayloadPersister payloadPersister;

        private IReceiptPersister receiptPersister;

        public DefaultPersisterHandler(IPayloadPersister payloadPersister, IReceiptPersister receiptPersister)
        {
            this.payloadPersister = payloadPersister;
            this.receiptPersister = receiptPersister;
        }

        public void Persist(IInboundMetadata inboundMetadata, FileInfo payloadPath)
        {
            receiptPersister.Persist(inboundMetadata, payloadPath);
        }

        public FileInfo Persist(TransmissionIdentifier transmissionIdentifier, Header header, Stream inputStream)
        {
            return payloadPersister.Persist(transmissionIdentifier, header, inputStream);
        }
    }
}
