using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Certificates.Validator.Util
{
    using Microsoft.Extensions.Caching.Memory;

    using Mx.Certificates.Validator.Api;

    using Org.BouncyCastle.X509;

    public class CachedValidatorRule : ValidatorRule
    {

        private ValidatorRule validatorRule;

        private readonly long timeout;

        private readonly IMemoryCache memoryCache;

        // private LoadingCache<X509Certificate, Result> cache;
        // private IMemoryCache cache;



        public CachedValidatorRule(ValidatorRule validatorRule, long timeout)
        {
            this.validatorRule = validatorRule;
            this.timeout = timeout;
            this.memoryCache = new MemoryCache(
                new MemoryCacheOptions() { ExpirationScanFrequency = TimeSpan.FromSeconds(5) }
            );

            //cache = CacheBuilder.newBuilder()
            //    .expireAfterWrite(timeout, TimeUnit.SECONDS)
            //    .build(this);
        }

        public void validate(X509Certificate certificate) // throws CertificateValidationException
        {
            Result value;
            if (this.memoryCache.TryGetValue(certificate, out value))
            {
                value.trigger();
            }
            // cache.getUnchecked(certificate).trigger();
        }

        // @Override
        internal Result load(X509Certificate certificate) // throws Exception
        {
            try
            {
                validatorRule.validate(certificate);
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

            public void trigger() // throws CertificateValidationException
            {
                if (exception != null)
                {
                    throw exception;
                }
            }
        }
    }



}
