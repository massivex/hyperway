namespace Mx.Hyperway.Api.Outbound
{
    using System.IO;

    using Mx.Peppol.Common.Model;

    public interface TransmissionMessage
    {

        Header getHeader();

        Stream getPayload();
    }
}
