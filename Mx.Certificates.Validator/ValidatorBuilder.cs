using System.Collections.Generic;

namespace Mx.Certificates.Validator
{
    using Mx.Certificates.Validator.Api;
    using Mx.Certificates.Validator.Structure;

    /// <summary>
    /// Builder for creation of validators.
    /// </summary>
    public class ValidatorBuilder
    {
        /// <summary>
        /// Entry point
        /// </summary>
        public static ValidatorBuilder NewInstance()
        {
            return new ValidatorBuilder();
        }

        private readonly List<IValidatorRule> validatorRules = new List<IValidatorRule>();

        private ValidatorBuilder()
        {
            // No action
        }

        /// <summary>
        /// Append validator instance to validator. 
        /// </summary>
        /// <param name="validatorRule">Configured validator.</param>
        /// <returns>Builder instance.</returns>
        public ValidatorBuilder AddRule(IValidatorRule validatorRule)
        {
            this.validatorRules.Add(validatorRule);
            return this;
        }

        /// <summary>
        /// Generates a ValidatorHelper instance containing defined validator(s). 
        /// </summary>
        /// <returns>Validator ready for use.</returns>
        public Validator Build()
        {
            if (this.validatorRules.Count == 1)
            {
                return new Validator(this.validatorRules[0]);
            }

            return new Validator(Junction.And(this.validatorRules.ToArray()));
        }
    }

}
