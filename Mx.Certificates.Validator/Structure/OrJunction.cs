using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Certificates.Validator.Structure
{
    using Mx.Certificates.Validator.Api;

    using Org.BouncyCastle.X509;

    /**
     * Allows combining instances of validators using a limited set of logic.
     */
    public class OrJunction : AbstractJunction
    {

        public OrJunction(params ValidatorRule[] validatorRules)
            : base(validatorRules)
        {

        }

        public override void validate(X509Certificate certificate) // throws CertificateValidationException
        {
            List<CertificateValidationException> exceptions = new List<CertificateValidationException>();

            foreach (ValidatorRule validatorRule in this.validatorRules)
            {
                try
                {
                    validatorRule.validate(certificate);
                    return;
                }
                catch (CertificateValidationException e)
                {
                    exceptions.Add(e);
                }
            }

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Or-junction failed with results:");
            foreach (Exception e in exceptions)
            {
                stringBuilder.Append("\n* ").Append(e.Message);
            }

            throw new FailedValidationException(stringBuilder.ToString());
        }
    }

}
