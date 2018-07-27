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

    public class DefaultPersisterHandler : PersisterHandler
    {

        private PayloadPersister payloadPersister;

        private ReceiptPersister receiptPersister;

        public DefaultPersisterHandler(PayloadPersister payloadPersister, ReceiptPersister receiptPersister)
        {
            this.payloadPersister = payloadPersister;
            this.receiptPersister = receiptPersister;
        }

        public void persist(InboundMetadata inboundMetadata, FileInfo payloadPath)
        {
            receiptPersister.persist(inboundMetadata, payloadPath);
        }

        public FileInfo persist(TransmissionIdentifier transmissionIdentifier, Header header, Stream inputStream)
        {
            return payloadPersister.persist(transmissionIdentifier, header, inputStream);
        }
    }
}
