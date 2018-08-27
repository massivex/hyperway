namespace Mx.Certificates.Validator
{
    using System.IO;

    using Mx.Certificates.Validator.Api;
    using Mx.Tools;

    using Org.BouncyCastle.Security.Certificates;
    using Org.BouncyCastle.X509;

    /// <summary>
    /// Encapsulate validator for a more extensive API. 
    /// </summary>
    public class Validator : IValidatorRule
    {

        private static X509CertificateParser certFactory;

        public static X509Certificate GetCertificate(byte[] cert)
        {
            return GetCertificate(cert.ToStream());
        }

        public static X509Certificate GetCertificate(Stream inputStream)
        {
            try
            {
                if (certFactory == null)
                {
                    certFactory = new X509CertificateParser();
                }

                return certFactory.ReadCertificate(inputStream);
            }
            catch (CertificateException e)
            {
                throw new CertificateValidationException(e.Message, e);
            }
        }

        private IValidatorRule validatorRule;

        public Validator(IValidatorRule validatorRule)
        {
            this.validatorRule = validatorRule;
        }

        public virtual void Validate(X509Certificate certificate)
        {
            this.validatorRule.Validate(certificate);
        }

        public X509Certificate Validate(Stream inputStream)
        {
            X509Certificate certificate = GetCertificate(inputStream);
            this.Validate(certificate);
            return certificate;
        }

        public X509Certificate Validate(byte[] bytes)
        {
            X509Certificate certificate = GetCertificate(bytes);
            this.Validate(certificate);
            return certificate;
        }

        public bool IsValid(X509Certificate certificate)
        {
            try
            {
                this.Validate(certificate);
                return true;
            }
            catch (CertificateValidationException)
            {
                return false;
            }
        }

        public bool IsValid(Stream inputStream)
        {
            try
            {
                return this.IsValid(GetCertificate(inputStream));
            }
            catch (CertificateValidationException)
            {
                return false;
            }
        }

        public bool IsValid(byte[] bytes)
        {
            try
            {
                return this.IsValid(GetCertificate(bytes));
            }
            catch (CertificateValidationException)
            {
                return false;
            }
        }
    }
}