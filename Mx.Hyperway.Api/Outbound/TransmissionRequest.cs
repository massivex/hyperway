namespace Mx.Hyperway.Api.Outbound
{
    using Mx.Peppol.Common.Model;

    public interface ITransmissionRequest : ITransmissionMessage
    {
        Endpoint GetEndpoint();
    }

}
