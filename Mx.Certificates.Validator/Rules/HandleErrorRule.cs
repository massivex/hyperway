using System.Collections.Generic;

namespace Mx.Certificates.Validator.Rules
{
    using System.Linq;

    using Mx.Certificates.Validator.Api;

    using Org.BouncyCastle.X509;

    /// <summary>
    /// Allows encapsulation of other validations rule, allowing errors to occur but not failed validation. May be useful
    /// for encapsulation of CRLRule and other rules where use of external resources may cause validation to fail due to
    /// unavailability of services.
    /// </summary>
    public class HandleErrorRule : IValidatorRule
    {

        private readonly List<IValidatorRule> validatorRules;

        public HandleErrorRule(IValidatorRule[] validatorRules)
            : this(validatorRules.ToList())
        {

        }

        public HandleErrorRule(List<IValidatorRule> validatorRules)
        {
            this.validatorRules = validatorRules;
        }

        public void Validate(X509Certificate certificate) // throws CertificateValidationException
        {
            foreach (IValidatorRule validatorRule in this.validatorRules)
            {
                try
                {
                    validatorRule.Validate(certificate);
                }
                catch (FailedValidationException)
                {
                    throw;
                }
                catch (CertificateValidationException)
                {
                    // No action.
                }
            }
        }
    }
}