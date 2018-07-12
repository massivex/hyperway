using System.Collections.Generic;

using Mx.Tools;

namespace Mx.Certificates.Validator.Rules
{
    using System.Linq;

    using Mx.Certificates.Validator.Api;

    using Org.BouncyCastle.Asn1.X509;
    using Org.BouncyCastle.X509;

    public class KeyUsageRule : ValidatorRule
    {

        private KeyUsage[] expectedKeyUsages;

        private bool[] expected = new bool[9];

        public KeyUsageRule(KeyUsage[] keyUsages)
        {
            this.expectedKeyUsages = keyUsages;

            foreach (KeyUsage keyUsage in keyUsages)
            {
                
                this.expected[keyUsage.IntValue] = true;
            }
        }

        public void validate(X509Certificate certificate) // throws CertificateValidationException
        {
            bool[] found = certificate.GetKeyUsage();

            var keyUsageText = this.expectedKeyUsages.ToStringValues();
            var keyValueText = prettyprint(found).ToStringValues();
            var msg = $"Expected {keyUsageText}, found {keyValueText}.";
                
            if (!this.expected.SequenceEqual(found))
            {
                throw new FailedValidationException(msg);
            }
        }

        private KeyUsage[] prettyprint(bool[] ku)
        {
            List<KeyUsage> keyUsages = new List<KeyUsage>();

            for (int i = 0; i < ku.Length; i++)
            {
                if (ku[i])
                {
                    keyUsages.Add(new KeyUsage(i));
                }

            }

            return keyUsages.ToArray();
        }
    }

}
