using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Certificates.Validator.Api
{
    using System.Security.Cryptography.X509Certificates;

    using Org.BouncyCastle.X509;

    public interface CrlFetcher
    {
        X509Crl get(String url); // throws CertificateValidationException;
    }
}
