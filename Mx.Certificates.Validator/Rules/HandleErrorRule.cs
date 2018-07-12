using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Certificates.Validator.Rules
{
    using System.Linq;

    using Mx.Certificates.Validator.Api;

    using Org.BouncyCastle.Utilities;
    using Org.BouncyCastle.X509;

    /**
     * Allows encapsulation of other validations rule, allowing errors to occur but not failed validation. May be useful
     * for encapsulation of CRLRule and other rules where use of external resources may cause validation to fail due to
     * unavailability of services.
     */
    public class HandleErrorRule : ValidatorRule
    {

        private readonly List<ValidatorRule> validatorRules;

        public HandleErrorRule(ValidatorRule[] validatorRules)
            : this(validatorRules.ToList())
        {

        }

        public HandleErrorRule(List<ValidatorRule> validatorRules)
        {
            this.validatorRules = validatorRules;
        }

        public void validate(X509Certificate certificate) // throws CertificateValidationException
        {
            foreach (ValidatorRule validatorRule in validatorRules)
            {
                try
                {
                    validatorRule.validate(certificate);
                }
                catch (FailedValidationException e)
                {
                    throw e;
                }
                catch (CertificateValidationException e)
                {
                    // No action.
                }
            }
        }
    }
}