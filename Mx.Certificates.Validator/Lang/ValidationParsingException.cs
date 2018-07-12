using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Certificates.Validator.Lang
{
    using Mx.Certificates.Validator.Api;

    public class ValidatorParsingException : CertificateValidationException
    {
        public ValidatorParsingException(String reason, Exception cause)
            : base(reason, cause)
        {

        }
    }
}
