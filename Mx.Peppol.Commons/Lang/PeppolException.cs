using System;

namespace Mx.Peppol.Common.Lang
{
    public class PeppolException : Exception
    {
        public PeppolException(String message): base(message)
        {
        }

        public PeppolException(String message, Exception cause)
            : base(message, cause)
        {
        }

        public PeppolException(Exception cause)
            : base("Unhandled exception", cause)
        {
        }
    }

}
