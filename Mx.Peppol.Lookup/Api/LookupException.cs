using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Peppol.Lookup.Api
{
    using Mx.Peppol.Common.Lang;

    public class LookupException : PeppolException
    {

        private static readonly long serialVersionUID = -8630614964594045904L;

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
