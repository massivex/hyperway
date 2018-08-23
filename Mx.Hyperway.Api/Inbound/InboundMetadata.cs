namespace Mx.Hyperway.Api.Inbound
{
    using Mx.Hyperway.Api.Transmission;

    using Org.BouncyCastle.X509;

    public interface InboundMetadata : TransmissionResult
    {

        /**
         * Fetch sender's certificate.
         *
         * @return Certificate.
         */
        X509Certificate getCertificate();

    }
}
