using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Oxalis.Api.Inbound
{
    using System.Security.Cryptography.X509Certificates;

    using Mx.Oxalis.Api.Transmission;

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
