using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Certificates.Validator.Api
{
    using System.Security.Cryptography.X509Certificates;

    using X509Certificate = Org.BouncyCastle.X509.X509Certificate;

    /**
     * Defines bucket for certificate allowing customized storage of certificates.
     */
    public interface CertificateBucket : IEnumerable<X509Certificate>
    {

        /**
         * Find certificate by subject.
         *
         * @param principal Principal representing certificate to be found.
         * @return Certificate if found, otherwise null.
         * @throws CertificateBucketException
         */
        X509Certificate findBySubject(X500DistinguishedName principal); // throws CertificateBucketException;
    }
}
