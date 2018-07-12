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
    public class AndJunction : AbstractJunction
    {

        public AndJunction(params ValidatorRule[] validatorRules) : base(validatorRules)
        {

        }

        public override void validate(X509Certificate certificate) // throws CertificateValidationException
        {
            foreach (ValidatorRule validatorRule in validatorRules)
            {
                validatorRule.validate(certificate);
            }
        }
    }

}
