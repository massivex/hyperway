using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Oxalis.Api.Lang
{
    /**
     * Security exceptions are always thrown to indicate a certain action would involve stepping outside
     * current security domain, and forcing such action must be seen as a no-go.
     */
    public class OxalisSecurityException : OxalisException
    {

        public OxalisSecurityException(String message)
            : base(message)
        {

        }

        public OxalisSecurityException(String message, Exception cause)
            : base(message, cause)
        {

        }
    }
}
