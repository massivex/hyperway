using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Peppol.Common.Lang
{

    public class PeppolLoadingException : PeppolException
    {
        public PeppolLoadingException(String message)
            : base(message)
        {

        }

        public PeppolLoadingException(String message, Exception cause)
            : base(message, cause)
        {
        }
    }
}
