using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Oxalis.As2.Lang
{
    using Mx.Oxalis.As2.Code;

    public class OxalisAs2InboundException : OxalisAs2Exception
    {

        private Disposition disposition;

        public OxalisAs2InboundException(Disposition disposition, String message, Exception cause)
            : base(message, cause)
        {

            this.disposition = disposition;
        }

        public Disposition getDisposition()
        {
            return disposition;
        }
    }
}
