using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Oxalis.Api.Lang
{
    public class OxalisPluginException : OxalisRuntimeException
    {

        public OxalisPluginException(String message)
            : base(message)
        {

        }

        public OxalisPluginException(String message, Exception cause)
            : base(message, cause)
        {

        }
    }
}
