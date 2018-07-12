using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Peppol.Common.Lang
{
    public class PeppolParsingException : PeppolException
    {

        public PeppolParsingException(String message)
            : base(message)
        {
        }

        public PeppolParsingException(String message, Exception cause)
            : base(message, cause)
        {
        }
    }
}
