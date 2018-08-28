using System;

namespace Mx.Peppol.Common.Model
{
    using System.Text.RegularExpressions;

    using Mx.Peppol.Common.Lang;

    public class TransportProtocol : AbstractSimpleIdentifier
    {
        private static readonly Regex Pattern = new Regex("[\\p{Upper}\\d]+", RegexOptions.Compiled);

        public static readonly TransportProtocol As2 = new TransportProtocol("AS2");

        public static readonly TransportProtocol As4 = new TransportProtocol("AS4");

        public static readonly TransportProtocol Internal = new TransportProtocol("INTERNAL");

        public static TransportProtocol Of(String value) // throws PeppolException
        {
            if (!Pattern.Match(value).Success)
            {
                throw new PeppolException("Identifier not according to pattern.");
            }

            return new TransportProtocol(value);
        }

        private TransportProtocol(String identifier)
            : base(identifier)
        {
        }


        public override string ToString()
        {
            return "TransportProtocol{" + this.Identifier + '}';
        }
    }
}