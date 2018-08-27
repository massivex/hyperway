namespace Mx.Hyperway.Api.Transmission
{
    using Mx.Hyperway.Api.Model;
    using Mx.Peppol.Common.Model;

    public interface ITransmissionVerifier
    {

        void Verify(Header header, Direction direction);
    }
}
