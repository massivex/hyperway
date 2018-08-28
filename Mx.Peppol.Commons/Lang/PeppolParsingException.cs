using System;

namespace Mx.Peppol.Common.Lang
{
    public class PeppolParsingException : PeppolException
    {

        public PeppolParsingException(string message)
            : base(message)
        {
        }

        public PeppolParsingException(string message, Exception cause)
            : base(message, cause)
        {
        }
    }
}
