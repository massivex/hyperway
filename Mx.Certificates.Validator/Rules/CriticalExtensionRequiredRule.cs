using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Certificates.Validator.Rules
{
    using System.Linq;

    using Mx.Certificates.Validator.Api;

    using Org.BouncyCastle.Utilities;
    using Org.BouncyCastle.Utilities.Collections;
    using Org.BouncyCastle.X509;

    public class CriticalExtensionRequiredRule : ValidatorRule
    {

    private List<String> requiredExtensions;

    public CriticalExtensionRequiredRule(String[] requiredExtensions)
    {
        this.requiredExtensions = requiredExtensions.ToList();
    }

        public void validate(X509Certificate certificate) // throws CertificateValidationException
        {
            ISet oids = certificate.GetCriticalExtensionOids();

            if (oids == null)
            {
                throw new FailedValidationException("Certificate doesn't contain critical OIDs.");
            }

            foreach (String oid in requiredExtensions)
            {
                if (!oids.Contains(oid))
                {
                    throw new FailedValidationException(
                        String.Format("Certificate doesn't contain critical OID '{0}'.", oid));
                }
            }
        }
    }
}
