using System;

namespace Mx.Peppol.Common.Lang
{
    public class PeppolException : Exception
    {
        public PeppolException(string message): base(message)
        {
        }

        public PeppolException(string message, Exception cause)
            : base(message, cause)
        {
        }

        public PeppolException(Exception cause)
            : base("Unhandled exception", cause)
        {
        }
    }

}
