using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Certificates.Validator.Structure
{
    using Mx.Certificates.Validator.Api;

    /**
     * Allows combining instances of validators using a limited set of logic.
     */
    public class Junction
    {

        public static ValidatorRule and(params ValidatorRule[] validatorRules)
        {
            if (validatorRules.Length == 1)
            {
                return validatorRules[0];
            }

            return new AndJunction(validatorRules);
        }

        public static ValidatorRule or(params ValidatorRule[] validatorRules)
        {
            if (validatorRules.Length == 1)
            {
                return validatorRules[0];
            }

            return new OrJunction(validatorRules);
        }

        public static ValidatorRule xor(params ValidatorRule[] validatorRules)
        {
            if (validatorRules.Length == 1)
            {
                return validatorRules[0];
            }

            return new XorJunction(validatorRules);
        }

        Junction()
        {
            // No action
        }
    }

}
