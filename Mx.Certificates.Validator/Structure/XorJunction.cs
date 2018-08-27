using System.Collections.Generic;
using System.Text;

namespace Mx.Certificates.Validator.Structure
{
    using Mx.Certificates.Validator.Api;

    using Org.BouncyCastle.X509;
    /// <summary>
    /// Allows combining instances of validators using a limited set of logic.
    /// </summary>
    public class XorJunction : AbstractJunction
    {

        public XorJunction(params IValidatorRule[] validatorRules)
            : base(validatorRules)
        {

        }

        public override void Validate(X509Certificate certificate)
        {
            List<CertificateValidationException> exceptions = new List<CertificateValidationException>();

            foreach (IValidatorRule validatorRule in this.ValidatorRules)
            {
                try
                {
                    validatorRule.Validate(certificate);
                }
                catch (CertificateValidationException e)
                {
                    exceptions.Add(e);
                }
            }

            if (exceptions.Count != this.ValidatorRules.Count - 1)
            {
                StringBuilder stringBuilder = new StringBuilder();
                int totExceptions = exceptions.Count;
                int totRules = this.ValidatorRules.Count;
                stringBuilder.Append($"Xor-junction failed with results ({totExceptions} of {totRules}):");
                foreach (var e in exceptions)
                {
                    stringBuilder.Append("\n* ").Append(e.Message);
                }


                throw new FailedValidationException(stringBuilder.ToString());
            }
        }
    }
}
