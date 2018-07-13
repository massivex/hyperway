using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Mx.Certificates.Validator.Rules
{

    using Mx.Certificates.Validator.Api;
    using Mx.Certificates.Validator.Xml;
    using Mx.Tools;

    using Org.BouncyCastle.Asn1;
    using Org.BouncyCastle.Asn1.Crmf;
    using Org.BouncyCastle.Asn1.X509;
    using Org.BouncyCastle.Security.Certificates;
    using Org.BouncyCastle.Utilities;
    using Org.BouncyCastle.X509;

    /**
     * Validator using defined logic to validate content in principal name of subject or issuer.
     */
    public class PrincipalNameRule : ValidatorRule
    {

        protected String field;

        protected PrincipalNameProvider<String> provider;

        protected PrincipalEnum principal;

        public PrincipalNameRule(PrincipalNameProvider<String> provider)
            : this(null, provider, PrincipalEnum.SUBJECT)
        {

        }

        public PrincipalNameRule(PrincipalNameProvider<String> provider, PrincipalEnum principal)
            : this(null, provider, principal)
        {

        }

        public PrincipalNameRule(String field, PrincipalNameProvider<String> provider)
            : this(field, provider, PrincipalEnum.SUBJECT)
        {

        }

        public PrincipalNameRule(String field, PrincipalNameProvider<String> provider, PrincipalEnum principal)
        {
            this.field = field;
            this.provider = provider;
            this.principal = principal;
        }

        public void validate(X509Certificate certificate) // CertificateValidationException
        {
            try
            {
                X509Name current;
                if (principal.Equals(PrincipalEnum.SUBJECT))
                {
                    current = getSubject(certificate);
                }
                else
                {
                    current = getIssuer(certificate);
                }

                foreach (string value in extract(current, field))
                {
                    if (provider.validate(value))
                    {
                        return;
                    }
                }

                throw new FailedValidationException($"Validation of subject principal({this.field}) failed.");
            }
            catch (CertificateEncodingException e)
            {
                throw new FailedValidationException("Unable to fetch principal.", e);
            }
        }

        protected static X509Name getIssuer(X509Certificate certificate) // throws CertificateEncodingException
        {
            return certificate.IssuerDN;
            // return new JcaX509CertificateHolder(certificate).getIssuer();
        }

        protected static X509Name getSubject(X509Certificate certificate) // throws CertificateEncodingException
        {
            return certificate.SubjectDN;
            // return new JcaX509CertificateHolder(certificate).getSubject();
        }


        protected static IList<String> extract(X509Name principal, String field)
        {
            if (field == null)
            {
                return new SingletonList<string>(principal.ToString());
                // return Arrays.asList(principal.toString());
            }

            var values = new List<string>();
            var normalizedField = (field ?? string.Empty).Trim().ToLowerInvariant();
            if (!X509Name.DefaultLookup.Contains(normalizedField))
            {
                return values;
            }

            var oidField = (DerObjectIdentifier) X509Name.DefaultLookup[normalizedField];
            values = principal.GetValueList(oidField).OfType<string>().ToList();
            return values;
        }

        //public enum Principal
        //{
        //    SUBJECT,

        //    ISSUER
        //}
    }

}
