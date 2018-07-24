namespace Mx.Hyperway.Api.Lang
{
    using System;

    public class VerifierException : HyperwayTransmissionException
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
            return this.reason;
        }

        public enum Reason
        {
            PARTICIPANT,

            DOCUMENT_TYPE,

            PROCESS
        }
    }

}
