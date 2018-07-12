using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Certificates.Validator.Rules
{
    using Mx.Certificates.Validator.Api;

    using Org.BouncyCastle.Security.Certificates;
    using Org.BouncyCastle.X509;

    /**
     * Validate validity of certificate.
     */
    public class ExpirationRule : ValidatorRule
    {

        public void validate(X509Certificate certificate) // throws FailedValidationException
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