using System;

namespace Mx.Peppol.Security.Lang
{
    using Mx.Peppol.Common.Lang;

    public class PeppolSecurityException : PeppolException
    {
        public PeppolSecurityException(string message)
            : base(message)
        {

        }

        public PeppolSecurityException(string message, Exception cause)
            : base(message, cause)
        {

        }
    }
}
