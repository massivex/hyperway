using System;
using System.Collections.Generic;

namespace Mx.Certificates.Validator.Rules
{
    using System.IO;

    using Mx.Certificates.Validator.Api;
    using Mx.Certificates.Validator.Util;

    using Org.BouncyCastle.Asn1;
    using Org.BouncyCastle.Asn1.X509;
    using Org.BouncyCastle.X509;
    using Org.BouncyCastle.X509.Extension;

    public class CrlRule : IValidatorRule
    {

        private const string CrlExtension = "2.5.29.31";

        private readonly ICrlFetcher crlFetcher;

        public CrlRule(ICrlFetcher crlFetcher)
        {
            this.crlFetcher = crlFetcher;
        }

        public CrlRule(ICrlCache crlCache)
            : this(new SimpleCachingCrlFetcher(crlCache))
        {}

        public CrlRule()
        {
            this.crlFetcher = new SimpleCachingCrlFetcher(new SimpleCrlCache());
        }

        public void Validate(X509Certificate certificate)
        {
            List<string> urls = GetCrlDistributionPoints(certificate);
            foreach (string url in urls) {
                X509Crl crl = this.crlFetcher.Get(url);
                if (crl != null)
                {
                    if (crl.IsRevoked(certificate))
                    {
                        throw new FailedValidationException("Certificate is revoked.");
                    }
                }
            }
        }

        public static List<string> GetCrlDistributionPoints(X509Certificate certificate)
        {
            try
            {
                List<string> urls = new List<string>();

                if (!certificate.GetNonCriticalExtensionOids().Contains(CrlExtension))
                {
                    return urls;
                }

                var oid = new DerObjectIdentifier(CrlExtension);
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
