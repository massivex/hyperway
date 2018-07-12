using System;

namespace Mx.Certificates.Validator
{
    using System.IO;

    using Mx.Certificates.Validator.Api;
    using Mx.Tools;

    using Org.BouncyCastle.Security.Certificates;
    using Org.BouncyCastle.X509;

    /**
     * Encapsulate validator for a more extensive API.
     */
    public class Validator : ValidatorRule
    {

        private static CertificateFactory certFactory;

        public static X509Certificate getCertificate(byte[] cert) // throws CertificateValidationException
        {
            return getCertificate(cert.ToStream());
        }

        public static X509Certificate getCertificate(Stream inputStream) // throws CertificateValidationException
        {
            try
            {
                if (certFactory == null)
                    certFactory = CertificateFactory.getInstance("X.509");

                return (X509Certificate)certFactory.generateCertificate(inputStream);
            }
            catch (CertificateException e)
            {
                throw new CertificateValidationException(e.Message, e);
            }
        }

        private ValidatorRule validatorRule;

        public Validator(ValidatorRule validatorRule)
        {
            this.validatorRule = validatorRule;
        }

        public void validate(X509Certificate certificate) //throws CertificateValidationException
        {
            validatorRule.validate(certificate);
        }

        public X509Certificate validate(Stream inputStream) // throws CertificateValidationException
        {
            X509Certificate certificate = getCertificate(inputStream);
            validate(certificate);
            return certificate;
        }

        public X509Certificate validate(byte[] bytes) //throws CertificateValidationException
        {
            X509Certificate certificate = getCertificate(bytes);
            validate(certificate);
            return certificate;
        }

        public bool isValid(X509Certificate certificate)
        {
            try
            {
                validate(certificate);
                return true;
            }
            catch (CertificateValidationException e)
            {
                return false;
            }
        }

        public bool isValid(Stream inputStream)
        {
            try
            {
                return isValid(getCertificate(inputStream));
            }
            catch (CertificateValidationException e)
            {
                return false;
            }
        }

        public bool isValid(byte[] bytes)
        {
            try
            {
                return isValid(getCertificate(bytes));
            }
            catch (CertificateValidationException e)
            {
                return false;
            }
        }
    }
}