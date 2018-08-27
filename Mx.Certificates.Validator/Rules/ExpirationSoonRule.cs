using System;

namespace Mx.Certificates.Validator.Rules
{
    using Mx.Certificates.Validator.Api;

    using Org.BouncyCastle.X509;

    /// <inheritdoc />
    /// <summary>
    /// Validation making sure certificate doesn't expire in n milliseconds. 
    /// </summary>
    public class ExpirationSoonRule : IValidatorRule
    {

        private readonly long millis;

        public ExpirationSoonRule(long millis)
        {
            this.millis = millis;
        }

        public void Validate(X509Certificate certificate)
        {
            var timeout = DateTime.Now.AddMilliseconds(this.millis);
            if (certificate.NotAfter < timeout)
            {
                throw new FailedValidationException($"Certificate expires in less than {this.millis} milliseconds.");
            }
        }
    }
}
