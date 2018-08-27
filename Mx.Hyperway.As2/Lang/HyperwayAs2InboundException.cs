namespace Mx.Hyperway.As2.Lang
{
    using System;

    using Mx.Hyperway.As2.Code;

    public class HyperwayAs2InboundException : HyperwayAs2Exception
    {
        public HyperwayAs2InboundException(Disposition disposition, string message, Exception cause)
            : base(message, cause)
        {

            this.Disposition = disposition;
        }

        public Disposition Disposition { get; }
    }
}
