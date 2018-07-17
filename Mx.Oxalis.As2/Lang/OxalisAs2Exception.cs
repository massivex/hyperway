using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Oxalis.As2.Lang
{
    using Mx.Oxalis.Api.Lang;

    public class OxalisAs2Exception : OxalisException
    {

        public OxalisAs2Exception(String message)
            : base(message)
        {
        }

        public OxalisAs2Exception(String message, Exception cause)
            : base(message, cause)
        {

        }
    }
}
