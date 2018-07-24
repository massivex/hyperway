namespace Mx.Hyperway.Api.Inbound
{
    using System.Security.Cryptography.X509Certificates;

    using Mx.Hyperway.Api.Transmission;

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
