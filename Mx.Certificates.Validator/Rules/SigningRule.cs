using System;

namespace Mx.Certificates.Validator.Rules
{
    using Mx.Certificates.Validator.Api;

    using Org.BouncyCastle.Crypto;
    using Org.BouncyCastle.Security;
    using Org.BouncyCastle.X509;

    public class SigningRule : IValidatorRule
    {

        public static SigningRule PublicSignedOnly()
        {
            return new SigningRule(Kind.PublicSignedOnly);
        }

        public static SigningRule SelfSignedOnly()
        {
            return new SigningRule(Kind.SelfSignedOnly);
        }

        private Kind kind;

        public SigningRule()
            : this(Kind.PublicSignedOnly)
        {

        }

        public SigningRule(Kind kind)
        {
            this.kind = kind;
        }

        public void Validate(X509Certificate certificate) // throws CertificateValidationException
        {
            try
            {
                if (IsSelfSigned(certificate))
                {
                    // Self signed
                    if (this.kind.Equals(Kind.PublicSignedOnly))
                    {
                        throw new FailedValidationException("Certificate should be publicly signed.");
                    }
                }
                else
                {
                    // Publicly signed
                    if (this.kind.Equals(Kind.SelfSignedOnly))
                    {
                        throw new FailedValidationException("Certificate should be self-signed.");
                    }
                }
            }
            catch (FailedValidationException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new CertificateValidationException(e.Message, e);
            }
        }

        public static bool IsSelfSigned(X509Certificate cert)
        {
            try
            {
                // Try to verify certificate signature with its own public key
                AsymmetricKeyParameter key = cert.GetPublicKey();
                cert.Verify(key);
                return true;
            }
            catch (Exception ex) when (ex is SignatureException | ex is InvalidKeyException)
            {
                // Invalid signature --> not self-signed
                // Invalid key --> not self-signed
                return false;
            }
        }

        public enum Kind
        {
            PublicSignedOnly,

            SelfSignedOnly
        }
    }
}
