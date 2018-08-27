using System.Collections.Generic;

namespace Mx.Certificates.Validator.Rules
{
    using System.Linq;

    using Mx.Certificates.Validator.Api;

    using Org.BouncyCastle.Utilities.Collections;
    using Org.BouncyCastle.X509;

    public class CriticalExtensionRequiredRule : IValidatorRule
    {

    private readonly List<string> requiredExtensions;

    public CriticalExtensionRequiredRule(string[] requiredExtensions)
    {
        this.requiredExtensions = requiredExtensions.ToList();
    }

        public void Validate(X509Certificate certificate)
        {
            ISet oids = certificate.GetCriticalExtensionOids();

            if (oids == null)
            {
                throw new FailedValidationException("Certificate doesn't contain critical OIDs.");
            }

            foreach (string oid in this.requiredExtensions)
            {
                if (!oids.Contains(oid))
                {
                    throw new FailedValidationException($"Certificate doesn't contain critical OID '{oid}'.");
                }
            }
        }
    }
}
