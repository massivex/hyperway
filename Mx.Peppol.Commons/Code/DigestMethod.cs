using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Peppol.Common.Code
{
    public class DigestMethod
    {

        public static DigestMethod SHA1 = new DigestMethod("SHA-1", "http://www.w3.org/2000/09/xmldsig#sha1");

        public static DigestMethod SHA256 = new DigestMethod("SHA-256", "http://www.w3.org/2001/04/xmlenc#sha256");

        public static DigestMethod SHA512 = new DigestMethod("SHA-512", "http://www.w3.org/2001/04/xmlenc#sha512");

        private readonly String identifier;

        private readonly String uri;

        public DigestMethod(String identifier, String uri)
        {
            this.identifier = identifier;
            this.uri = uri;
        }

        public String getIdentifier()
        {
            return identifier;
        }

        public String getUri()
        {
            return uri;
        }

        public static DigestMethod fromUri(String uri)
        {
            var values = new[] { SHA1, SHA256, SHA512 };
            foreach (DigestMethod digestMethod in values)
            {
                if (digestMethod.uri.Equals(uri))
                {
                    return digestMethod;
                }
            }

            return null;
        }
    }
}
