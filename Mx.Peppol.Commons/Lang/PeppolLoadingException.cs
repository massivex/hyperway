using System;

namespace Mx.Peppol.Common.Lang
{

    public class PeppolLoadingException : PeppolException
    {
        public PeppolLoadingException(string message)
            : base(message)
        {

        }

        public PeppolLoadingException(string message, Exception cause)
            : base(message, cause)
        {
        }
    }
}
