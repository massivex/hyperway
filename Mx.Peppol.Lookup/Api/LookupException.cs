using System;

namespace Mx.Peppol.Lookup.Api
{
    using Mx.Peppol.Common.Lang;

    public class LookupException : PeppolException
    {
        public LookupException(String message)
            : base(message)
        {
        }

        public LookupException(String message, Exception cause)
            : base(message, cause)
        {
        }
    }
}
