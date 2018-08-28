namespace Mx.Peppol.Common.Code
{
    public class DigestMethod
    {

        public static DigestMethod Sha1 = new DigestMethod("SHA-1", "http://www.w3.org/2000/09/xmldsig#sha1");

        public static DigestMethod Sha256 = new DigestMethod("SHA-256", "http://www.w3.org/2001/04/xmlenc#sha256");

        public static DigestMethod Sha512 = new DigestMethod("SHA-512", "http://www.w3.org/2001/04/xmlenc#sha512");

        public DigestMethod(string identifier, string uri)
        {
            this.Identifier = identifier;
            this.Uri = uri;
        }

        public string Identifier { get; }

        public string Uri { get; }

        public static DigestMethod FromUri(string uri)
        {
            var values = new[] { Sha1, Sha256, Sha512 };
            foreach (DigestMethod digestMethod in values)
            {
                if (digestMethod.Uri.Equals(uri))
                {
                    return digestMethod;
                }
            }

            return null;
        }
    }
}
