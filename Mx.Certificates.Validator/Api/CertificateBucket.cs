using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Certificates.Validator.Api
{

    using Org.BouncyCastle.Asn1.X509;
    using Org.BouncyCastle.X509;

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
        X509Certificate findBySubject(X509Name principal); // throws CertificateBucketException;
    }
}
