using System;

namespace Mx.Peppol.Sbdh.Lang
{
    using Mx.Peppol.Common.Lang;

    public class SbdhException : PeppolException
    {

        public SbdhException(string message)
            : base(message)
        {

        }

        public SbdhException(string message, Exception cause)
            : base(message, cause)
        {

        }
    }
}
