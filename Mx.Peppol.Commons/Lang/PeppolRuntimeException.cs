using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Peppol.Common.Lang
{
    public class PeppolRuntimeException : Exception
    {

        public PeppolRuntimeException(String message, Exception cause)
            : base(message, cause)
        {

        }
    }
}
