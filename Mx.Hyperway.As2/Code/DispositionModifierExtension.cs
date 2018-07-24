namespace Mx.Hyperway.As2.Code
{
    using System;

    public class DispositionModifierExtension
    {
        public static readonly DispositionModifierExtension UNSUPPORTED_FORMAT =
            new DispositionModifierExtension("unsupported format");

        public static readonly DispositionModifierExtension UNSUPPORTED_MIC_ALGORITHMS =
            new DispositionModifierExtension("unsupported MIC-algorithms");

        public static readonly DispositionModifierExtension SENDER_EQUALS_RECEIVER =
            new DispositionModifierExtension("sender-equals-receiver");

        public static readonly DispositionModifierExtension DECRYPTION_FAILED =
            new DispositionModifierExtension("decryption-failed");

        public static readonly DispositionModifierExtension AUTHENTICATION_FAILED =
            new DispositionModifierExtension("authentication-failed");

        public static readonly DispositionModifierExtension INTEGRITY_CHECK_FAILED =
            new DispositionModifierExtension("integrity-check-failed");

        public static readonly DispositionModifierExtension PARTICIPANT_NOT_ACCEPTED =
            new DispositionModifierExtension("participant-not-accepted");

        public static readonly DispositionModifierExtension DOCUMENT_TYPE_ID_NOT_ACCEPTED =
            new DispositionModifierExtension("document-modifier-id-not-accepted");

        public static readonly DispositionModifierExtension PROCESS_ID_NOT_ACCEPTED =
            new DispositionModifierExtension("process-id-not-accepted");

        public static readonly DispositionModifierExtension UNEXPECTED_PROCESSING_ERROR =
            new DispositionModifierExtension("unexpected-processing-error");

        public static readonly DispositionModifierExtension DUPLICATE_DOCUMENT =
            new DispositionModifierExtension("duplicate-document");

        private readonly string value;

        public static DispositionModifierExtension of(String str)
        {
            foreach (DispositionModifierExtension extension in values())
            {
                if (extension.value.Equals(str))
                {
                    return extension;
                }
            }

            throw new ArgumentException(String.Format("Unknown disposition modifier extension: {0}", str));
        }

        DispositionModifierExtension(String extension)
        {
            this.value = extension;
        }

        public static DispositionModifierExtension[] values()
        {
            return new[]
                       {
                           UNSUPPORTED_FORMAT, UNSUPPORTED_MIC_ALGORITHMS, SENDER_EQUALS_RECEIVER, DECRYPTION_FAILED,
                           AUTHENTICATION_FAILED, INTEGRITY_CHECK_FAILED, PARTICIPANT_NOT_ACCEPTED,
                           DOCUMENT_TYPE_ID_NOT_ACCEPTED, PROCESS_ID_NOT_ACCEPTED, UNEXPECTED_PROCESSING_ERROR,
                           UNEXPECTED_PROCESSING_ERROR, DUPLICATE_DOCUMENT
                       };
        }

        public String ToString()
        {
            return this.value;
        }
    }
}