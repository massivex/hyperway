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

    public class CriticalExtensionRecognizedRule : ValidatorRule
    {

        private readonly List<String> recognizedExtensions;

        public CriticalExtensionRecognizedRule(String[] recognizedExtensions)
        {
            this.recognizedExtensions = recognizedExtensions.ToList();
        }

        public void validate(X509Certificate certificate) // throws CertificateValidationException
        {
            ISet oids = certificate.GetCriticalExtensionOids();

            if (oids == null)
            {
                return;
            }

            foreach (String oid in oids)
            {
                if (!recognizedExtensions.Contains(oid))
                {
                    throw new FailedValidationException(
                        String.Format(
                            "X509 certificate {0} specifies a critical extension {1} which is not recognized",
                            certificate.SerialNumber,
                            oid));
                }
            }
        }
    }
}
