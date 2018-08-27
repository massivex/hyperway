namespace Mx.Hyperway.Api.Lang
{
    using System;

    public class VerifierException : HyperwayTransmissionException
    {

        private readonly Reason reason;

        public static VerifierException BecauseOf(Reason reason, String message)
        {
            return new VerifierException(reason, message);
        }

        private VerifierException(Reason reason, String message) : base(message)
        {
            this.reason = reason;
        }

        public Reason GetReason()
        {
            return this.reason;
        }

        public enum Reason
        {
            // ReSharper disable InconsistentNaming
            PARTICIPANT,

            DOCUMENT_TYPE,

            PROCESS
            // ReSharper restore InconsistentNaming
        }
    }

}
