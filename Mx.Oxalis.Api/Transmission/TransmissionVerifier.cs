namespace Mx.Hyperway.Api.Transmission
{
    using Mx.Hyperway.Api.Model;
    using Mx.Peppol.Common.Model;

    public interface TransmissionVerifier
    {

        void verify(Header header, Direction direction); // throws VerifierException;
    }
}
