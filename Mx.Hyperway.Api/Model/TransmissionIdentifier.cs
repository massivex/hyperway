namespace Mx.Hyperway.Api.Model
{
    using System;
    using System.Text.RegularExpressions;

    using Mx.Peppol.Common.Model;

    public class TransmissionIdentifier : AbstractSimpleIdentifier
    {

        private static readonly Regex Rfc2822 = new Regex("^<(.+?)>$", RegexOptions.Compiled);

        public static TransmissionIdentifier GenerateUuid()
        {
            return Of(Guid.NewGuid().ToString());
        }

        public static TransmissionIdentifier Of(String value)
        {
            return new TransmissionIdentifier(value);
        }

        public static TransmissionIdentifier FromHeader(String value)
        {
            var matches = Rfc2822.Matches(value);
            if (matches.Count > 0)
            {
                return Of(matches[0].Groups[1].Value);
            }

            return Of(value);
        }

        private TransmissionIdentifier(String value)
            : base(value)
        {
        }
    }
}
