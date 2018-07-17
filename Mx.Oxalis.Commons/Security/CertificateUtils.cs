using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Oxalis.Commons.Security
{
    using System.Text.RegularExpressions;

    using Org.BouncyCastle.Asn1.X509;
    using Org.BouncyCastle.X509;

    public class CertificateUtils
    {

        private static readonly Regex PATTERN_CN = new Regex("CN=([^,]+)");

        public static String extractCommonName(X509Certificate certificate)
        {
            X509Name principal = certificate.SubjectDN; // .getSubjectX500Principal();
            // TODO: check if ToString return common name
            Match m = PATTERN_CN.Match(principal.ToString());

            if (m.Success)
            {
                return m.Groups[1].Value;
            }
            else
            {
                throw new InvalidOperationException("Unable to extract the CN attribute from " + principal.ToString());
            }
        }
    }

}
