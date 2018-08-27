namespace Mx.Certificates.Validator.Structure
{
    using Mx.Certificates.Validator.Api;
    using Org.BouncyCastle.X509;

    /// <summary>
    /// Allows combining instances of validators using a limited set of logic.
    /// </summary>
    public class AndJunction : AbstractJunction
    {

        public AndJunction(params IValidatorRule[] validatorRules) : base(validatorRules)
        {

        }

        public override void Validate(X509Certificate certificate)
        {
            foreach (IValidatorRule validatorRule in this.ValidatorRules)
            {
                validatorRule.Validate(certificate);
            }
        }
    }

}
