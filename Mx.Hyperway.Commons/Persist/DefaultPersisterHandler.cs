namespace Mx.Hyperway.Commons.Persist
{
    using System.IO;

    using Mx.Hyperway.Api.Inbound;
    using Mx.Hyperway.Api.Model;
    using Mx.Hyperway.Api.Persist;
    using Mx.Peppol.Common.Model;

    public class DefaultPersisterHandler : IPersisterHandler
    {

        private readonly IPayloadPersister payloadPersister;

        private readonly IReceiptPersister receiptPersister;

        public DefaultPersisterHandler(IPayloadPersister payloadPersister, IReceiptPersister receiptPersister)
        {
            this.payloadPersister = payloadPersister;
            this.receiptPersister = receiptPersister;
        }

        public void Persist(IInboundMetadata inboundMetadata, FileInfo payloadPath)
        {
            this.receiptPersister.Persist(inboundMetadata, payloadPath);
        }

        public FileInfo Persist(TransmissionIdentifier transmissionIdentifier, Header header, Stream inputStream)
        {
            return this.payloadPersister.Persist(transmissionIdentifier, header, inputStream);
        }
    }
}
