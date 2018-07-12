using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Peppol.Sbdh.Lang
{
    using Mx.Peppol.Common.Lang;

    public class SbdhException : PeppolException
    {

        public SbdhException(String message)
            : base(message)
        {

        }

        public SbdhException(String message, Exception cause)
            : base(message, cause)
        {

        }
    }
}
