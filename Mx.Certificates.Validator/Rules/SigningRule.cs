using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Certificates.Validator.Rules
{
    using Mx.Certificates.Validator.Api;

    using Org.BouncyCastle.Crypto;
    using Org.BouncyCastle.Security;
    using Org.BouncyCastle.X509;

    public class SigningRule : ValidatorRule
    {

        public static SigningRule PublicSignedOnly()
        {
            return new SigningRule(Kind.PUBLIC_SIGNED_ONLY);
        }

        public static SigningRule SelfSignedOnly()
        {
            return new SigningRule(Kind.SELF_SIGNED_ONLY);
        }

        private Kind kind;

        public SigningRule()
            : this(Kind.PUBLIC_SIGNED_ONLY)
        {

        }

        public SigningRule(Kind kind)
        {
            this.kind = kind;
        }

        public void validate(X509Certificate certificate) // throws CertificateValidationException
        {
            try
            {
                if (isSelfSigned(certificate))
                {
                    // Self signed
                    if (kind.Equals(Kind.PUBLIC_SIGNED_ONLY))
                    {
                        throw new FailedValidationException("Certificate should be publicly signed.");
                    }
                }
                else
                {
                    // Publicly signed
                    if (kind.Equals(Kind.SELF_SIGNED_ONLY))
                    {
                        throw new FailedValidationException("Certificate should be self-signed.");
                    }
                }
            }
            catch (FailedValidationException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new CertificateValidationException(e.Message, e);
            }
        }

        /**
         * Source: http://www.nakov.com/blog/2009/12/01/x509-certificate-validation-in-java-build-and-verify-chain-and-verify-clr-with-bouncy-castle/
         */
        public static bool
            isSelfSigned(
                X509Certificate cert) // throws CertificateException, NoSuchAlgorithmException, NoSuchProviderException {
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
            PUBLIC_SIGNED_ONLY,

            SELF_SIGNED_ONLY
        }
    }
}
