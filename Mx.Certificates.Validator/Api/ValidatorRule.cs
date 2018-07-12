using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Certificates.Validator.Api
{
    using Org.BouncyCastle.X509;

    /**
     * Defines a validator rule. Made as simple as possible by purpose.
     */
    public interface ValidatorRule
    {

        /**
         * Validate certificate.
         * @param certificate Certificate subject to validation.
         * @throws CertificateValidationException
         */
        void validate(X509Certificate certificate); // throws CertificateValidationException;
    }

}
