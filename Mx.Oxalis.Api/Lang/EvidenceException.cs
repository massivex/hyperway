using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Oxalis.Api.Lang
{
    public class EvidenceException : OxalisException
    {

        public EvidenceException(String message, Exception cause)
            : base(message, cause)
        {
        }
    }
}
