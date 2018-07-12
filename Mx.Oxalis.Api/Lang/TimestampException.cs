using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Oxalis.Api.Lang
{
    public class TimestampException : OxalisException
    {

        public TimestampException(String message)
            : base(message)
        {

        }

        public TimestampException(String message, Exception cause)
            : base(message, cause)
        {

        }
    }
}
