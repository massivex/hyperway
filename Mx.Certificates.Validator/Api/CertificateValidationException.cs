using System;

namespace Mx.Certificates.Validator.Api
{
    /// <summary>
    /// Generic exception for project.
    /// </summary>
    public class CertificateValidationException : Exception
    {
        public CertificateValidationException(string reason, Exception cause)
            : base(reason, cause)
        {

        }

        public CertificateValidationException(string message)
            : base(message)
        {

        }
    }
}
