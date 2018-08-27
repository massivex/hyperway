namespace Mx.Certificates.Validator.Rules
{
    using Mx.Certificates.Validator.Api;

    using Org.BouncyCastle.X509;

    /// <inheritdoc />
    /// <summary>
    /// Throws an exception on validation if message is set.
    /// </summary>
    public class DummyRule : IValidatorRule
    {

        public static DummyRule AlwaysSuccess()
        {
            return new DummyRule();
        }

        public static DummyRule AlwaysFail(string message)
        {
            return new DummyRule(message);
        }

        private readonly string message;

        /// <summary>
        /// Defines an instance always having successful validations.</summary>
        public DummyRule(): this(null)
        { }

        /// <summary>
        /// Defines as instance always having failing validations, given message is not null.
        /// </summary>
        /// <param name="message">Message used when failing validation.</param>
        public DummyRule(string message)
        {
            this.message = message;
        }

        public void Validate(X509Certificate certificate)
        {
            if (this.message != null)
            {
                throw new FailedValidationException(this.message);
            }
        }
    }
}