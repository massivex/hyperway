using System.Collections.Generic;
using System.Linq;

namespace Mx.Certificates.Validator.Rules
{

    using Mx.Certificates.Validator.Api;
    using Mx.Tools;
    using Mx.Xml.tns;

    using Org.BouncyCastle.Asn1;
    using Org.BouncyCastle.Asn1.X509;
    using Org.BouncyCastle.Security.Certificates;
    using Org.BouncyCastle.X509;

    /// <inheritdoc />
    /// <summary>
    /// Validator using defined logic to validate content in principal name of subject or issuer.
    /// </summary>
    public class PrincipalNameRule : IValidatorRule
    {

        protected string Field;

        protected IPrincipalNameProvider<string> Provider;

        protected Enumerations.PrincipalEnum Principal;

        public PrincipalNameRule(IPrincipalNameProvider<string> provider)
            : this(null, provider, Enumerations.PrincipalEnum.SUBJECT)
        {

        }

        public PrincipalNameRule(IPrincipalNameProvider<string> provider, Enumerations.PrincipalEnum principal)
            : this(null, provider, principal)
        {

        }

        public PrincipalNameRule(string field, IPrincipalNameProvider<string> provider)
            : this(field, provider, Enumerations.PrincipalEnum.SUBJECT)
        {

        }

        public PrincipalNameRule(string field, IPrincipalNameProvider<string> provider, Enumerations.PrincipalEnum principal)
        {
            this.Field = field;
            this.Provider = provider;
            this.Principal = principal;
        }

        public void Validate(X509Certificate certificate)
        {
            try
            {
                X509Name current;
                if (this.Principal.Equals(Enumerations.PrincipalEnum.SUBJECT))
                {
                    current = GetSubject(certificate);
                }
                else
                {
                    current = GetIssuer(certificate);
                }

                foreach (string value in Extract(current, this.Field))
                {
                    if (this.Provider.Validate(value))
                    {
                        return;
                    }
                }

                throw new FailedValidationException($"Validation of subject principal({this.Field}) failed.");
            }
            catch (CertificateEncodingException e)
            {
                throw new FailedValidationException("Unable to fetch principal.", e);
            }
        }

        protected static X509Name GetIssuer(X509Certificate certificate) // throws CertificateEncodingException
        {
            return certificate.IssuerDN;
            // return new JcaX509CertificateHolder(certificate).getIssuer();
        }

        protected static X509Name GetSubject(X509Certificate certificate) // throws CertificateEncodingException
        {
            return certificate.SubjectDN;
            // return new JcaX509CertificateHolder(certificate).getSubject();
        }


        protected static IList<string> Extract(X509Name principal, string field)
        {
            if (field == null)
            {
                return new SingletonList<string>(principal.ToString());
                // return Arrays.asList(principal.toString());
            }

            var values = new List<string>();
            var normalizedField = field.Trim().ToLowerInvariant();
            if (!X509Name.DefaultLookup.Contains(normalizedField))
            {
                return values;
            }

            var oidField = (DerObjectIdentifier) X509Name.DefaultLookup[normalizedField];
            values = principal.GetValueList(oidField).OfType<string>().ToList();
            return values;
        }
    }
}
