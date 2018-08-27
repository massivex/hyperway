using System;

namespace Mx.Certificates.Validator.Api
{
    /// <summary>
    /// Exception related to actions performed by certificate buckets.
    /// </summary>
    public class CertificateBucketException : CertificateValidationException
    {
        public CertificateBucketException(String reason, Exception cause) : base(reason, cause)
        {

        }
    }

}
