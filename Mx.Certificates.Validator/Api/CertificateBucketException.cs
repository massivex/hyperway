using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Certificates.Validator.Api
{
    /**
     * Exception related to actions performed by certificate buckets.
     */
    public class CertificateBucketException : CertificateValidationException
    {
        public CertificateBucketException(String reason, Exception cause) : base(reason, cause)
        {

        }
    }

}
