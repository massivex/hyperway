namespace Mx.Hyperway.Api.Persist
{
    using System.IO;

    using Mx.Hyperway.Api.Model;
    using Mx.Peppol.Common.Model;

    public interface IPayloadPersister
    {

        FileInfo Persist(TransmissionIdentifier transmissionIdentifier, Header header, Stream inputStream);
    }
}
