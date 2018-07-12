using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Peppol.Security.Lang
{
    using Mx.Peppol.Common.Lang;

    public class PeppolSecurityException : PeppolException
    {

        private static readonly long serialVersionUID = 6928682319726226728L;

        public PeppolSecurityException(String message)
            : base(message)
        {

        }

        public PeppolSecurityException(String message, Exception cause)
            : base(message, cause)
        {

        }
    }
}
