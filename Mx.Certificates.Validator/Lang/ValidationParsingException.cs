using System;

namespace Mx.Certificates.Validator.Lang
{
    using Mx.Certificates.Validator.Api;

    public class ValidatorParsingException : CertificateValidationException
    {
        public ValidatorParsingException(string reason, Exception cause)
            : base(reason, cause)
        {

        }
    }
}
