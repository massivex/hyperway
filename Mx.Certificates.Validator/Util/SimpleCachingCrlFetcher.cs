using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Certificates.Validator.Util
{
    using System.Net;
    using System.Security.Cryptography;
    using System.Security.Cryptography.X509Certificates;
    using System.Text.RegularExpressions;

    using Mx.Certificates.Validator.Api;

    using Org.BouncyCastle.Asn1.X509;
    using Org.BouncyCastle.X509;

    using X509Certificate = Org.BouncyCastle.X509.X509Certificate;

    /**
     * Simple implementation of CRL fetcher, which caches downloaded CRLs. If a CRL is not cached, or the Next update-
     * field of a cached CRL indicates there is an updated CRL available, an updated CRL will immediately be downloaded.
     */
    public class SimpleCachingCrlFetcher : CrlFetcher
    {

        private static X509CrlParser certificateFactory;

        private CrlCache crlCache;

        public SimpleCachingCrlFetcher(CrlCache crlCache)
        {
            this.crlCache = crlCache;
        }


        public X509Certificate2 get(String url) // throws CertificateValidationException
        {
            X509Crl crl = crlCache.get(url);
            if (crl == null)
            {
                // Not in cache
                crl = download(url);
            }
            else if (crl.NextUpdate != null
                     && crl.NextUpdate.getTime() < System.DateTime.Now.currentTimeMillis())
            {
                // Outdated
                crl = download(url);
            }
            else if (crl.getNextUpdate() == null)
            {
                // No action.
            }

            return crl;
        }

        protected X509Crl download(String url) // throws CertificateValidationException
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

                    X509Crl crl = (X509Crl)certificateFactory.ReadCrl(data);
                    crlCache.set(url, crl);
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
                throw new CertificateValidationException(
                    String.Format("Failed to download CRL '{0}' ({1})", url, e.Message),
                    e);
            }

            return null;
        }
    }

}
