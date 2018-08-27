using System;

namespace Mx.Certificates.Validator.Util
{
    using Microsoft.Extensions.Caching.Memory;

    using Mx.Certificates.Validator.Api;

    using Org.BouncyCastle.X509;

    public class CachedValidatorRule : IValidatorRule
    {

        private readonly IValidatorRule validatorRule;

        private readonly IMemoryCache memoryCache;


        public CachedValidatorRule(IValidatorRule validatorRule)
        {
            this.validatorRule = validatorRule;
            this.memoryCache = new MemoryCache(
                new MemoryCacheOptions() { ExpirationScanFrequency = TimeSpan.FromSeconds(5) }
            );
        }

        public void Validate(X509Certificate certificate)
        {
            Result value;
            if (this.memoryCache.TryGetValue(certificate, out value))
            {
                value.Trigger();
            }
        }

        internal Result Load(X509Certificate certificate)
        {
            try
            {
                this.validatorRule.Validate(certificate);
                return new Result();
            }
            catch (CertificateValidationException e)
            {
                return new Result(e);
            }
        }


        internal class Result
        {

            private CertificateValidationException exception;

            public Result()
            {
                // No action.
            }

            public Result(CertificateValidationException e)
            {
                this.exception = e;
            }

            public void Trigger() // throws CertificateValidationException
            {
                if (this.exception != null)
                {
                    throw this.exception;
                }
            }
        }
    }
}
