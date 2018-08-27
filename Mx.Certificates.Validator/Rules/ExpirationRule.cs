using System;

namespace Mx.Certificates.Validator.Rules
{
    using Mx.Certificates.Validator.Api;

    using Org.BouncyCastle.Security.Certificates;
    using Org.BouncyCastle.X509;

    /// <inheritdoc />
    /// <summary>
    /// Validate validity of certificate.
    /// </summary>
    public class ExpirationRule : IValidatorRule
    {

        public void Validate(X509Certificate certificate)
        {
            try
            {
                certificate.CheckValidity(DateTime.Now);
            }
            catch (Exception e) when (e is CertificateNotYetValidException || e is CertificateExpiredException) {
                throw new FailedValidationException("Certificate does not have a valid expiration date.");
            }
        }
    }
}