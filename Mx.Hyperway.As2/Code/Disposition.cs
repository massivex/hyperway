namespace Mx.Hyperway.As2.Code
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    using Mx.Hyperway.Api.Lang;
    using Mx.Tools;

    public class Disposition
    {

        private static readonly Regex Pattern = new Regex(
            "^(.*?); ([a-z]+)/([a-z]+): (.*)|(.*?); ([a-z]+)$",
            RegexOptions.Compiled);

        private static readonly string SentAutomatically = "automatic-action/MDN-sent-automatically";

        public static readonly Disposition Processed = new Disposition(DispositionType.Processed, null, null);

        public static readonly Disposition UnsupportedFormat = new Disposition(
            DispositionType.Failed,
            DispositionModifier.Failure,
            DispositionModifierExtension.UnexpectedProcessingError);

        public static readonly Disposition UnsupportedMicAlgorithms = new Disposition(
            DispositionType.Failed,
            DispositionModifier.Failure,
            DispositionModifierExtension.UnsupportedMicAlgorithms);

        public static readonly Disposition SenderEqualsReceiver = new Disposition(
            DispositionType.Failed,
            DispositionModifier.Failure,
            DispositionModifierExtension.SenderEqualsReceiver);

        public static readonly Disposition DecryptionFailed = new Disposition(
            DispositionType.Failed,
            DispositionModifier.Error,
            DispositionModifierExtension.DecryptionFailed);

        public static readonly Disposition AuthenticationFailed = new Disposition(
            DispositionType.Failed,
            DispositionModifier.Error,
            DispositionModifierExtension.AuthenticationFailed);

        public static readonly Disposition IntegrityCheckFailed = new Disposition(
            DispositionType.Failed,
            DispositionModifier.Error,
            DispositionModifierExtension.IntegrityCheckFailed);

        public static readonly Disposition ParticipantNotAccepted = new Disposition(
            DispositionType.Failed,
            DispositionModifier.Error,
            DispositionModifierExtension.ParticipantNotAccepted);

        public static readonly Disposition DocumentTypeIdNotAccepted = new Disposition(
            DispositionType.Failed,
            DispositionModifier.Error,
            DispositionModifierExtension.DocumentTypeIdNotAccepted);

        public static readonly Disposition ProcessIdNotAccepted = new Disposition(
            DispositionType.Failed,
            DispositionModifier.Error,
            DispositionModifierExtension.ProcessIdNotAccepted);

        public static readonly Disposition UnexpectedProcessingError = new Disposition(
            DispositionType.Failed,
            DispositionModifier.Error,
            DispositionModifierExtension.UnexpectedProcessingError);

        public static readonly Disposition DuplicateDocument = new Disposition(
            DispositionType.Processed,
            DispositionModifier.Warning,
            DispositionModifierExtension.DuplicateDocument);

        private static readonly Dictionary<VerifierException.Reason, Disposition> VerifierMap =
            new Dictionary<VerifierException.Reason, Disposition>()
                {
                    {
                        VerifierException.Reason.DOCUMENT_TYPE,
                        DocumentTypeIdNotAccepted
                    },
                    {
                        VerifierException.Reason.PROCESS,
                        ProcessIdNotAccepted
                    },
                    {
                        VerifierException.Reason.PARTICIPANT,
                        ParticipantNotAccepted
                    }
                };

        public static Disposition Parse(String str)
        {
            // TODO: Verify correct parsing!!!!!
            // Matcher matcher = PATTERN.matcher(str);
            String cleaned = str.ReplaceAll("[ \r\n\t]+", " ");

            var matches = Pattern.Matches(cleaned);

            if (matches.Count > 0)
            {
                if (string.IsNullOrWhiteSpace(matches[1].Value))
                    return new Disposition(DispositionType.Of(matches[6].Value), null, null);
                else
                    return new Disposition(
                        DispositionType.Of(matches[2].Value),
                        DispositionModifier.Of(matches[3].Value),
                        DispositionModifierExtension.Of(matches[4].Value));
            }

            throw new ArgumentException($"Unable to parse disposition '{str}'.");
        }

        public static Disposition FromVerifierException(VerifierException e)
        {
            return VerifierMap[e.GetReason()];
        }

        private Disposition(DispositionType type, DispositionModifier modifier, DispositionModifierExtension extension)
        {
            this.Type = type;
            this.Modifier = modifier;
            this.Extension = extension;
        }

        public DispositionType Type { get; }

        public DispositionModifier Modifier { get; }

        public DispositionModifierExtension Extension { get; }

        public override string ToString()
        {
            if (this.Modifier == null)
            {
                return $"{SentAutomatically}; {this.Type}";
            }

            return $"{SentAutomatically}; {this.Type}/{this.Modifier}: {this.Extension}";
        }
    }
}
