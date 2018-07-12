using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Oxalis.Api.Lang
{
    public class VerifierException : OxalisTransmissionException
    {

        private readonly Reason reason;

        public static VerifierException becauseOf(Reason reason, String message)
        {
            return new VerifierException(reason, message);
        }

        private VerifierException(Reason reason, String message) : base(message)
        {
            this.reason = reason;
        }

        public Reason getReason()
        {
            return reason;
        }

        public enum Reason
        {
            PARTICIPANT,

            DOCUMENT_TYPE,

            PROCESS
        }
    }

}
