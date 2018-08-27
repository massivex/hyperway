namespace Mx.Hyperway.As2.Code
{
    using System;

    public class DispositionModifierExtension
    {
        public static readonly DispositionModifierExtension UnsupportedFormat =
            new DispositionModifierExtension("unsupported format");

        public static readonly DispositionModifierExtension UnsupportedMicAlgorithms =
            new DispositionModifierExtension("unsupported MIC-algorithms");

        public static readonly DispositionModifierExtension SenderEqualsReceiver =
            new DispositionModifierExtension("sender-equals-receiver");

        public static readonly DispositionModifierExtension DecryptionFailed =
            new DispositionModifierExtension("decryption-failed");

        public static readonly DispositionModifierExtension AuthenticationFailed =
            new DispositionModifierExtension("authentication-failed");

        public static readonly DispositionModifierExtension IntegrityCheckFailed =
            new DispositionModifierExtension("integrity-check-failed");

        public static readonly DispositionModifierExtension ParticipantNotAccepted =
            new DispositionModifierExtension("participant-not-accepted");

        public static readonly DispositionModifierExtension DocumentTypeIdNotAccepted =
            new DispositionModifierExtension("document-modifier-id-not-accepted");

        public static readonly DispositionModifierExtension ProcessIdNotAccepted =
            new DispositionModifierExtension("process-id-not-accepted");

        public static readonly DispositionModifierExtension UnexpectedProcessingError =
            new DispositionModifierExtension("unexpected-processing-error");

        public static readonly DispositionModifierExtension DuplicateDocument =
            new DispositionModifierExtension("duplicate-document");

        private readonly string value;

        public static DispositionModifierExtension Of(String str)
        {
            foreach (DispositionModifierExtension extension in Values())
            {
                if (extension.value.Equals(str))
                {
                    return extension;
                }
            }

            throw new ArgumentException($"Unknown disposition modifier extension: {str}");
        }

        private DispositionModifierExtension(String extension)
        {
            this.value = extension;
        }

        public static DispositionModifierExtension[] Values()
        {
            return new[]
                       {
                           UnsupportedFormat, UnsupportedMicAlgorithms, SenderEqualsReceiver, DecryptionFailed,
                           AuthenticationFailed, IntegrityCheckFailed, ParticipantNotAccepted,
                           DocumentTypeIdNotAccepted, ProcessIdNotAccepted, UnexpectedProcessingError,
                           UnexpectedProcessingError, DuplicateDocument
                       };
        }

        public override string ToString()
        {
            return this.value;
        }
    }
}