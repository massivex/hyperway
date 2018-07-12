using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Oxalis.Api.Lang
{
    public class OxalisLoadingException : OxalisRuntimeException
    {

        public OxalisLoadingException(String message)
            : base(message)
        {

        }

        public OxalisLoadingException(String message, Exception cause)
            : base(message)
        {

        }
    }
}