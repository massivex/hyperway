namespace Mx.Hyperway.Api.Outbound
{
    using System.IO;

    using Mx.Peppol.Common.Model;

    public interface ITransmissionMessage
    {

        Header GetHeader();

        Stream GetPayload();
    }
}
