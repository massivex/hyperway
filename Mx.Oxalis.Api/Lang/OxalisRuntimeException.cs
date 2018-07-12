using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Oxalis.Api.Lang
{
    public abstract class OxalisRuntimeException : Exception
    {

        public OxalisRuntimeException(String message)
            : base(message)
        {

        }

        public OxalisRuntimeException(String message, Exception cause)
            : base(message, cause)
        {
        }
    }
}