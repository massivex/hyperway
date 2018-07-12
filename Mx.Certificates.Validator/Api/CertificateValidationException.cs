using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Certificates.Validator.Api
{
    /**
     * Generic exception for project.
     */
    public class CertificateValidationException : Exception
    {
        public CertificateValidationException(String reason, Exception cause)
            : base(reason, cause)
        {

        }

        public CertificateValidationException(String message)
            : base(message)
        {

        }
    }
}
