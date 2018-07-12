using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Certificates.Validator.Structure
{
    using Mx.Certificates.Validator.Api;

    using Org.BouncyCastle.X509;

    public abstract class AbstractJunction : ValidatorRule
    {

        protected List<ValidatorRule> validatorRules = new List<ValidatorRule>();

        public AbstractJunction(ValidatorRule[] validatorRules)
        {
            addRule(validatorRules);
        }

        public AbstractJunction addRule(ValidatorRule[] validatorRules)
        {
            this.validatorRules.AddRange(validatorRules);
            return this;
        }

        public abstract void validate(X509Certificate certificate);
    }
}
