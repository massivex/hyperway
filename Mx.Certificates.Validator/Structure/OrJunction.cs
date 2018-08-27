using System.Collections.Generic;
using System.Text;

namespace Mx.Certificates.Validator.Structure
{
    using Mx.Certificates.Validator.Api;

    using Org.BouncyCastle.X509;

    /// <summary>
    /// Allows combining instances of validators using a limited set of logic. 
    /// </summary>
    public class OrJunction : AbstractJunction
    {

        public OrJunction(params IValidatorRule[] validatorRules)
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
                    return;
                }
                catch (CertificateValidationException e)
                {
                    exceptions.Add(e);
                }
            }

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Or-junction failed with results:");
            foreach (CertificateValidationException e in exceptions)
            {
                stringBuilder.Append("\n* ").Append(e.Message);
            }

            throw new FailedValidationException(stringBuilder.ToString());
        }
    }

}
