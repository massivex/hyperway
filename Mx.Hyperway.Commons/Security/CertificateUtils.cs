namespace Mx.Hyperway.Commons.Security
{
    using System;
    using System.Text.RegularExpressions;

    using Org.BouncyCastle.Asn1.X509;
    using Org.BouncyCastle.X509;

    public class CertificateUtils
    {

        private static readonly Regex PatternCn = new Regex("CN=([^,]+)");

        public static String ExtractCommonName(X509Certificate certificate)
        {
            X509Name principal = certificate.SubjectDN;
            Match m = PatternCn.Match(principal.ToString());

            if (m.Success)
            {
                return m.Groups[1].Value;
            }
            else
            {
                throw new InvalidOperationException("Unable to extract the CN attribute from " + principal);
            }
        }
    }

}
