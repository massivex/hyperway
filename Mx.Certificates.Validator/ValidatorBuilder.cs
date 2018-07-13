using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Certificates.Validator
{
    using Mx.Certificates.Validator.Api;
    using Mx.Certificates.Validator.Structure;

    /**
     * Builder for creation of validators.
     */
    public class ValidatorBuilder
    {

        /**
         * Point of entry.
         *
         * @return Builder instance.
         */
        public static ValidatorBuilder newInstance()
        {
            return new ValidatorBuilder();
        }

        private List<ValidatorRule> validatorRules = new List<ValidatorRule>();

        private ValidatorBuilder()
        {
            // No action
        }

        /**
         * Append validator instance to validator.
         *
         * @param validatorRule Configured validator.
         * @return Builder instance.
         */
        public ValidatorBuilder addRule(ValidatorRule validatorRule)
        {
            validatorRules.Add(validatorRule);
            return this;
        }

        /**
         * Generates a ValidatorHelper instance containing defined validator(s).
         *
         * @return Validator ready for use.
         */
        public Validator build()
        {
            if (validatorRules.Count == 1)
            {
                return new Validator(validatorRules[0]);
            }

            return new Validator(Junction.and(validatorRules.ToArray()));
        }
    }

}
