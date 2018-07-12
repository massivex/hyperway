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
    public class XorJunction : AbstractJunction
    {

        public XorJunction(params ValidatorRule[] validatorRules)
            : base(validatorRules)
        {

        }

        public override void validate(X509Certificate certificate) // throws CertificateValidationException
        {
            List<CertificateValidationException> exceptions = new List<CertificateValidationException>();

            foreach (ValidatorRule validatorRule in validatorRules)
            {
                try
                {
                    validatorRule.validate(certificate);
                }
                catch (CertificateValidationException e)
                {
                    exceptions.Add(e);
                }
            }

            if (exceptions.Count != validatorRules.Count - 1)
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(
                    String.Format(
                        "Xor-junction failed with results ({0} of {1}):",
                        exceptions.Count,
                        validatorRules.Count));
                foreach (Exception e in exceptions)
                {
                    stringBuilder.Append("\n* ").Append(e.Message);
                }


                throw new FailedValidationException(stringBuilder.ToString());
            }
        }
    }
}
