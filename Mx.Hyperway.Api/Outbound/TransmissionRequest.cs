namespace Mx.Hyperway.Api.Outbound
{
    using Mx.Peppol.Common.Model;

    public interface TransmissionRequest : TransmissionMessage
    {
        Endpoint getEndpoint();
    }

}
