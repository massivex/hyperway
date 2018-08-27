using System.Collections.Generic;

namespace Mx.Certificates.Validator.Rules
{
    using System.Linq;

    using Mx.Certificates.Validator.Api;

    using Org.BouncyCastle.Utilities.Collections;
    using Org.BouncyCastle.X509;

    public class CriticalExtensionRecognizedRule : IValidatorRule
    {

        private readonly List<string> recognizedExtensions;

        public CriticalExtensionRecognizedRule(string[] recognizedExtensions)
        {
            this.recognizedExtensions = recognizedExtensions.ToList();
        }

        public void Validate(X509Certificate certificate)
        {
            ISet oids = certificate.GetCriticalExtensionOids();

            if (oids == null)
            {
                return;
            }

            foreach (string oid in oids)
            {
                if (!this.recognizedExtensions.Contains(oid))
                {
                    throw new FailedValidationException(
                        $"X509 certificate {certificate.SerialNumber} specifies a critical extension {oid} which is not recognized");
                }
            }
        }
    }
}
