using System;

namespace Mx.Certificates.Validator.Util
{
    using System.Net;
    using System.Text.RegularExpressions;

    using Mx.Certificates.Validator.Api;

    using Org.BouncyCastle.X509;

    /// <summary>
    /// Simple implementation of CRL fetcher, which caches downloaded CRLs. If a CRL is not cached, or the Next update-
    /// field of a cached CRL indicates there is an updated CRL available, an updated CRL will immediately be downloaded.
    /// </summary>
    public class SimpleCachingCrlFetcher : ICrlFetcher
    {

        private static X509CrlParser certificateFactory;

        private readonly ICrlCache crlCache;

        public SimpleCachingCrlFetcher(ICrlCache crlCache)
        {
            this.crlCache = crlCache;
        }


        public X509Crl Get(string url)
        {
            X509Crl crl = this.crlCache.Get(url);
            if (crl == null)
            {
                // Not in cache
                crl = this.Download(url);
            }
            else if (crl.NextUpdate != null && crl.NextUpdate.Value < DateTime.Now)
            {
                // Outdated
                crl = this.Download(url);
            }
            else if (crl.NextUpdate == null)
            {
                // No action.
            }

            return crl;
        }

        protected X509Crl Download(string url)
        {
            try
            {
                if (Regex.IsMatch(url, "http[s]{0,1}://.*"))
                {
                    if (certificateFactory == null)
                    {
                        certificateFactory = new X509CrlParser();
                    }

                    WebClient wc = new WebClient();
                    var data = wc.DownloadData(url);

                    X509Crl crl = certificateFactory.ReadCrl(data);
                    this.crlCache.Set(url, crl);
                    return crl;
                }
                else if (url.StartsWith("ldap://"))
                {
                    // Currently not supported.
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new CertificateValidationException($"Failed to download CRL '{url}' ({e.Message})", e);
            }

            return null;
        }
    }

}
