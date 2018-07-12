using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Oxalis.Api.Lang
{
    /**
     * Base exception of the Oxalis exception hierarchy. Thrown exceptions must use a subclass of this to indicate type
     * of exception for better handling.
     */
    public abstract class OxalisException : Exception
    {

        public OxalisException(String message)
            : base(message)
        {

        }

        public OxalisException(String message, Exception cause)
            : base(message, cause)
        {

        }
    }
}
