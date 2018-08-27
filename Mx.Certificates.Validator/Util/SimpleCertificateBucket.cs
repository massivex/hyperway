using System.Collections.Generic;

namespace Mx.Certificates.Validator.Util
{
    using System.Collections;

    using Mx.Certificates.Validator.Api;

    using Org.BouncyCastle.Asn1.X509;
    using Org.BouncyCastle.X509;

    /// <summary>
    /// Lightweight implementation using ArrayList to keep certificates in memory.
    /// </summary>
    public class SimpleCertificateBucket : ICertificateBucket
    {

        private readonly List<X509Certificate> certificates = new List<X509Certificate>();

        public SimpleCertificateBucket(params X509Certificate[] certificates)
        {
            this.Add(certificates);
        }

        /// <summary>
        /// Append certificate(s) to bucket. 
        /// </summary>
        /// <param name="certificatesToAdd">Certificate(s) to be added.</param>
        public void Add(params X509Certificate[] certificatesToAdd)
        {
            this.certificates.AddRange(certificatesToAdd);
        }

        public X509Certificate FindBySubject(X509Name principal)
        {
            foreach (X509Certificate certificate in this.certificates)
            {
                if (certificate.SubjectDN.Equals(principal))
                {
                    return certificate;

                }
            }

            return null;
        }

        public IEnumerator<X509Certificate> GetEnumerator()
        {
            return this.certificates.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }

}
