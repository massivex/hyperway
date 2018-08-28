using System;

namespace Mx.Peppol.Common.Model
{

    public class TransportProfile : AbstractSimpleIdentifier
    {
        // ReSharper disable InconsistentNaming
        public static readonly TransportProfile START = Of("busdox-transport-start");

        public static readonly TransportProfile AS2_1_0 = Of("busdox-transport-as2-ver1p0");

        public static readonly TransportProfile AS4 = Of("bdxr-transport-ebms3-as4-v1p0");
        // ReSharper restore InconsistentNaming

        public static TransportProfile Of(String value)
        {
            return new TransportProfile(value);
        }

        private TransportProfile(String value)
            : base(value)
        {

        }


        public override string ToString()
        {
            return "TransportProfile{" + this.Identifier + '}';
        }
    }
}
