using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Certificates.Validator.Rules
{
    using Mx.Certificates.Validator.Api;

    using Org.BouncyCastle.X509;

    /**
     * Validation making sure certificate doesn't expire in n milliseconds.
     */
    public class ExpirationSoonRule : ValidatorRule
    {

        private long millis;

        public ExpirationSoonRule(long millis)
        {
            this.millis = millis;
        }

        public void validate(X509Certificate certificate) // throws CertificateValidationException
        {
            var timeout = DateTime.Now.AddMilliseconds(this.millis);
            if (certificate.NotAfter < timeout)
            {
                throw new FailedValidationException($"Certificate expires in less than {this.millis} milliseconds.");
            }
        }
    }
}
