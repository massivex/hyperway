namespace Mx.Hyperway.As2.Lang
{
    using System;

    using Mx.Hyperway.As2.Code;

    public class HyperwayAs2InboundException : HyperwayAs2Exception
    {

        private Disposition disposition;

        public HyperwayAs2InboundException(Disposition disposition, String message, Exception cause)
            : base(message, cause)
        {

            this.disposition = disposition;
        }

        public Disposition getDisposition()
        {
            return this.disposition;
        }
    }
}
