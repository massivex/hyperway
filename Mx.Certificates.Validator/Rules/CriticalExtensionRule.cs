namespace Mx.Certificates.Validator.Rules
{
    public class CriticalExtensionRule
    {
        private CriticalExtensionRule()
        {
            // No action.
        }

        public static CriticalExtensionRecognizedRule Recognizes(string[] recognizedExtensions)
        {
            return new CriticalExtensionRecognizedRule(recognizedExtensions);
        }

        public static CriticalExtensionRequiredRule Requires(string[] requiredExtensions)
        {
            return new CriticalExtensionRequiredRule(requiredExtensions);
        }
    }
}