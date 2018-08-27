namespace Mx.Hyperway.Commons.Transmission
{
    using Mx.Hyperway.Api.Model;
    using Mx.Hyperway.Api.Transmission;
    using Mx.Peppol.Common.Model;

    public class DefaultTransmissionVerifier : ITransmissionVerifier
    {

        public void Verify(Header header, Direction direction)
        {
            // No action.
        }
    }

}
