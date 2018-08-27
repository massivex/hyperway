namespace Mx.Hyperway.Api.Persist
{
    using System.IO;

    using Mx.Hyperway.Api.Inbound;

    public interface IReceiptPersister
    {
        void Persist(IInboundMetadata inboundMetadata, FileInfo payloadPath);
    }
}
