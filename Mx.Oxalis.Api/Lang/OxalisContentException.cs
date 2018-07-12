using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Oxalis.Api.Lang
{
    public class OxalisContentException : OxalisException
    {

        public OxalisContentException(String message) 
            : base(message)
        {
        }

        public OxalisContentException(String message, Exception cause)
            : base(message, cause)
        {

        }
    }
}
