using Mx.Certificates.Validator.Api;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Certificates.Validator.Util
{
    using Org.BouncyCastle.X509;

    /**
     * In-memory implementation of CRL cache. Used as default implementation.
     */
    public class SimpleCrlCache : CrlCache
    {

        private readonly Dictionary<String, X509Crl> storage = new Dictionary<string, X509Crl>();

        public X509Crl get(String url)
        {
            return this.storage[url];
        }

        public void set(String url, X509Crl crl)
        {
            if (crl == null)
            {
                this.storage.Remove(url);
            }
            else
            {
                this.storage.Add(url, crl);
            }
        }
    }
}
