namespace Mx.Oxalis.Api.Model
{
    using System;
    using System.Text.RegularExpressions;

    using Mx.Peppol.Common.Model;

    public class TransmissionIdentifier : AbstractSimpleIdentifier
    {

        private static readonly Regex RFC2822 = new Regex("^<(.+?)>$", RegexOptions.Compiled);

        public static TransmissionIdentifier generateUUID()
        {
            return of(Guid.NewGuid().ToString());
        }

        public static TransmissionIdentifier of(String value)
        {
            return new TransmissionIdentifier(value);
        }

        public static TransmissionIdentifier fromHeader(String value)
        {
            var matches = RFC2822.Matches(value);
            if (matches.Count > 0)
            {
                return of(matches[0].Groups[1].Value);
            }

            return of(value);
        }

        private TransmissionIdentifier(String value)
            : base(value)
        {
        }
    }
}
