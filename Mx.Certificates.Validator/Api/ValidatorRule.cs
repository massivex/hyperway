namespace Mx.Certificates.Validator.Api
{
    using Org.BouncyCastle.X509;

    /// <summary>
    /// Defines a validator rule. Made as simple as possible by purpose.
    /// </summary>
    public interface IValidatorRule
    {
        /// <summary>
        /// Validate certificate
        /// </summary>
        /// <param name="certificate">Certificate subject to validation</param>
        void Validate(X509Certificate certificate);
    }
}
