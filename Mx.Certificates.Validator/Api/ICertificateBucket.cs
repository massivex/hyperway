using System.Collections.Generic;

namespace Mx.Certificates.Validator.Api
{

    using Org.BouncyCastle.Asn1.X509;
    using Org.BouncyCastle.X509;

    /// <summary>
    /// Defines bucket for certificate allowing customized storage of certificates.
    /// </summary>
    public interface ICertificateBucket : IEnumerable<X509Certificate>
    {
        /// <summary>
        /// Find certificate by subject.
        /// </summary>
        /// <param name="principal">Principal representing certificate to be found</param>
        /// <returns>Certificate or null if not found</returns>
        X509Certificate FindBySubject(X509Name principal);
    }
}
