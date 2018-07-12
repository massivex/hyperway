using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Certificates.Validator.Util
{
    using System.Collections;

    using Mx.Certificates.Validator.Api;

    using Org.BouncyCastle.Asn1.X509;
    using Org.BouncyCastle.Utilities;
    using Org.BouncyCastle.X509;

    /**
     * Lightweight implementation using ArrayList to keep certificates in memory.
     */
    public class SimpleCertificateBucket : CertificateBucket
    {

        private List<X509Certificate> certificates = new List<X509Certificate>();

        public SimpleCertificateBucket(params X509Certificate[] certificates)
        {
            add(certificates);
        }

        /**
         * Append certificate(s) to bucket.
         *
         * @param certificates Certificate(s) to be added.
         */
        public void add(params X509Certificate[] certificates)
        {
            this.certificates.AddRange(certificates);
        }

        public X509Certificate findBySubject(X509Name principal)
        {
            foreach (X509Certificate certificate in certificates)
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
            return certificates.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }

}
