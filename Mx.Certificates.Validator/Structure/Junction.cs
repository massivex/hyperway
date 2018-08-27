namespace Mx.Certificates.Validator.Structure
{
    using Mx.Certificates.Validator.Api;

    /// <summary>
    /// Allows combining instances of validators using a limited set of logic. 
    /// </summary>
    public class Junction
    {

        public static IValidatorRule And(params IValidatorRule[] validatorRules)
        {
            if (validatorRules.Length == 1)
            {
                return validatorRules[0];
            }

            return new AndJunction(validatorRules);
        }

        public static IValidatorRule Or(params IValidatorRule[] validatorRules)
        {
            if (validatorRules.Length == 1)
            {
                return validatorRules[0];
            }

            return new OrJunction(validatorRules);
        }

        public static IValidatorRule Xor(params IValidatorRule[] validatorRules)
        {
            if (validatorRules.Length == 1)
            {
                return validatorRules[0];
            }

            return new XorJunction(validatorRules);
        }

        private Junction()
        {
            // No action
        }
    }

}
