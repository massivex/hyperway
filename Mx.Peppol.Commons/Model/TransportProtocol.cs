using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Peppol.Common.Model
{
    using System.Text.RegularExpressions;

    using Mx.Peppol.Common.Lang;

    public class TransportProtocol : AbstractSimpleIdentifier
    {

        private static readonly long serialVersionUID = -5938766453542971103L;

        private static Regex pattern = new Regex("[\\p{Upper}\\d]+", RegexOptions.Compiled);

        public static readonly TransportProtocol AS2 = new TransportProtocol("AS2");

        public static readonly TransportProtocol AS4 = new TransportProtocol("AS4");

        public static readonly TransportProtocol INTERNAL = new TransportProtocol("INTERNAL");

        public static TransportProtocol of(String value) // throws PeppolException
        {
            if (!pattern.Match(value).Success)
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
            return "TransportProtocol{" + this.value + '}';
        }
    }
}