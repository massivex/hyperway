using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Oxalis.DocumentSniffer.Lang
{
    using Mx.Oxalis.Api.Lang;

    public class InvalidPeppolParticipantException : OxalisRuntimeException
    {

        public InvalidPeppolParticipantException(String s)
            : base(s)
        {
        }

    }
}
