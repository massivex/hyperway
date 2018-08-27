using System.Collections.Generic;

namespace Mx.Certificates.Validator.Structure
{
    using Mx.Certificates.Validator.Api;

    using Org.BouncyCastle.X509;

    public abstract class AbstractJunction : IValidatorRule
    {

        protected List<IValidatorRule> ValidatorRules = new List<IValidatorRule>();

        public AbstractJunction(IValidatorRule[] validatorRules)
        {
            this.AddRule(validatorRules);
        }

        public AbstractJunction AddRule(IValidatorRule[] validatorRules)
        {
            this.ValidatorRules.AddRange(validatorRules);
            return this;
        }

        public abstract void Validate(X509Certificate certificate);
    }
}
