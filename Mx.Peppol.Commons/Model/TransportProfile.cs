using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Peppol.Common.Model
{

    public class TransportProfile : AbstractSimpleIdentifier
    {

        private static readonly long serialVersionUID = -8215053834194901976L;

        public static readonly TransportProfile START = TransportProfile.of("busdox-transport-start");

        public static readonly TransportProfile AS2_1_0 = TransportProfile.of("busdox-transport-as2-ver1p0");

        public static readonly TransportProfile AS4 = TransportProfile.of("bdxr-transport-ebms3-as4-v1p0");

        public static TransportProfile of(String value)
        {
            return new TransportProfile(value);
        }

        private TransportProfile(String value)
            : base(value)
        {

        }


        public override string ToString()
        {
            return "TransportProfile{" + this.value + '}';
        }
    }
}
