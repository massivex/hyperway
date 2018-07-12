using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Certificates.Validator.Rules
{
    using Mx.Certificates.Validator.Api;

    using Org.BouncyCastle.X509;

    /**
     * Throws an exception on validation if message is set.
     */
    public class DummyRule : ValidatorRule
    {

        public static DummyRule alwaysSuccess()
        {
            return new DummyRule();
        }

        public static DummyRule alwaysFail(String message)
        {
            return new DummyRule(message);
        }

        private String message;

        /**
         * Defines an instance always having successful validations.
         */
        public DummyRule(): this(null)
        { }

        /**
         * Defines as instance always having failing validations, given message is not null.
         * @param message Message used when failing validation.
         */
        public DummyRule(String message)
        {
            this.message = message;
        }

        public void validate(X509Certificate certificate) // throws CertificateValidationException
        {
            if (this.message != null)
            {
                throw new FailedValidationException(this.message);
            }
        }
    }
}