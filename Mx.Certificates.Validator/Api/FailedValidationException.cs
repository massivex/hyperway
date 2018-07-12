using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Certificates.Validator.Api
{

    /**
     * Exception thrown when validation failes.
     */
    public class FailedValidationException : CertificateValidationException
    {
        public FailedValidationException(String reason, Exception cause)
            : base(reason, cause)
        {

        }

        public FailedValidationException(String message)
            : base(message)
        {
        }
    }
}
