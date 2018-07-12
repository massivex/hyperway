using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Certificates.Validator.Rules
{
    using System.IO;

    using Mx.Certificates.Validator.Api;
    using Mx.Certificates.Validator.Util;

    using Org.BouncyCastle.Asn1;
    using Org.BouncyCastle.Asn1.X509;
    using Org.BouncyCastle.X509;
    using Org.BouncyCastle.X509.Extension;

    public class CrlRule : ValidatorRule
    {

        private const string CRL_EXTENSION = "2.5.29.31";

        private CrlFetcher crlFetcher;

        public CrlRule(CrlFetcher crlFetcher)
        {
            this.crlFetcher = crlFetcher;
        }

        public CrlRule(CrlCache crlCache)
            : this(new SimpleCachingCrlFetcher(crlCache))
        {}

        public CrlRule()
        {
            this.crlFetcher = new SimpleCachingCrlFetcher(new SimpleCrlCache());
        }

        public void validate(X509Certificate certificate) // throws CertificateValidationException
        {
            List<String> urls = getCrlDistributionPoints(certificate);
            foreach (String url in urls) {
                X509Crl crl = this.crlFetcher.get(url);
                if (crl != null)
                {
                    if (crl.IsRevoked(certificate))
                    {
                        throw new FailedValidationException("Certificate is revoked.");
                    }
                }
            }
        }

        public static List<String> getCrlDistributionPoints(X509Certificate certificate) // throws CertificateValidationException
        {
            try
            {
                List<String> urls = new List<String>();

                if (!certificate.GetNonCriticalExtensionOids().Contains(CRL_EXTENSION))
                {
                    return urls;
                }

                var oid = new DerObjectIdentifier(CRL_EXTENSION);
                CrlDistPoint distPoint = CrlDistPoint.GetInstance(X509ExtensionUtilities.FromExtensionValue(certificate.GetExtensionValue(oid)));
                foreach (DistributionPoint dp in distPoint.GetDistributionPoints())
                {
                    GeneralNames gn = (GeneralNames)dp.DistributionPointName.Name;
                    foreach (GeneralName name in gn.GetNames())
                    {
                        if (name.TagNo == GeneralName.UniformResourceIdentifier)
                        {
                            urls.Add(((DerIA5String)name.Name).GetString());
                        }
                    }
                }

                return urls;
            }
            catch (Exception e) when (e is IOException || e is NullReferenceException) {
                throw new CertificateValidationException(e.Message, e);
            }
            }
    }

}
