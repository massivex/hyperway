namespace Mx.Hyperway.As2.Model
{
    public class DispositionModifier
    {
        internal readonly DispositionModifierPrefix Prefix;

        private readonly string dispositionModifierExtension;

        internal DispositionModifier(DispositionModifierPrefix prefix, string dispositionModifierExtension)
        {
            this.Prefix = prefix;
            this.dispositionModifierExtension = dispositionModifierExtension;
        }

        public DispositionModifierPrefix GetPrefix()
        {
            return this.Prefix;
        }

        public string GetDispositionModifierExtension()
        {
            return this.dispositionModifierExtension;
        }

        public static DispositionModifier AuthenticationFailedError()
        {
            return new DispositionModifier(DispositionModifierPrefix.Error, "authentication-failed");
        }

        public static DispositionModifier DecompressionFailedError()
        {
            return new DispositionModifier(DispositionModifierPrefix.Error, "decompression-failed");
        }

        public static DispositionModifier DecryptionFailedError()
        {
            return new DispositionModifier(DispositionModifierPrefix.Error, "decryption-failed");
        }

        public static DispositionModifier InsufficientMessageSecurityError()
        {
            return new DispositionModifier(DispositionModifierPrefix.Error, "insufficient-message-security");
        }

        public static DispositionModifier IntegrityCheckFailedError()
        {
            return new DispositionModifier(DispositionModifierPrefix.Error, "integrity-check-failed");
        }

        public static DispositionModifier UnexpectedProcessingError()
        {
            return new DispositionModifier(DispositionModifierPrefix.Error, "unexpected-processing-error");
        }

        public static DispositionModifier Warning(string description)
        {
            return new DispositionModifier(DispositionModifierPrefix.Warning, description);
        }

        public static DispositionModifier UnsupportedFormatFailure()
        {
            return new DispositionModifier(DispositionModifierPrefix.Failure, "unsupported format");
        }

        public static DispositionModifier UnsupportedMicAlgorithms()
        {
            return new DispositionModifier(DispositionModifierPrefix.Failure, "unsupported MIC-algorithms");
        }

        public static DispositionModifier Failed(string description)
        {
            return new DispositionModifier(DispositionModifierPrefix.Failure, description);
        }

        public static DispositionModifier Error(string description)
        {
            return new DispositionModifier(DispositionModifierPrefix.Error, description);
        }

        public override string ToString()
        {
            return this.Prefix + ": " + this.dispositionModifierExtension;
        }
    }
}