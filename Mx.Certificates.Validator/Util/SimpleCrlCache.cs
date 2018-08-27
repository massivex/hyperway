using Mx.Certificates.Validator.Api;

using System.Collections.Generic;

namespace Mx.Certificates.Validator.Util
{
    using Org.BouncyCastle.X509;

    /// <summary>
    /// In-memory implementation of CRL cache. Used as default implementation.
    /// </summary>
    public class SimpleCrlCache : ICrlCache
    {

        private readonly Dictionary<string, X509Crl> storage = new Dictionary<string, X509Crl>();

        public X509Crl Get(string url)
        {
            return this.storage[url];
        }

        public void Set(string url, X509Crl crl)
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
