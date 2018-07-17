using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Oxalis.As2.Code
{
    using System.Text.RegularExpressions;

    using Mx.Oxalis.Api.Lang;
    using Mx.Tools;

    public class Disposition
    {

        private static readonly Regex PATTERN = new Regex("^(.*?); ([a-z]+)/([a-z]+): (.*)|(.*?); ([a-z]+)$", RegexOptions.Compiled);

    private static readonly string SENT_AUTOMATICALLY = "automatic-action/MDN-sent-automatically";

    public static readonly Disposition PROCESSED = new Disposition(DispositionType.PROCESSED, null, null);

        public static readonly Disposition UNSUPPORTED_FORMAT = new Disposition(
                DispositionType.FAILED, DispositionModifier.FAILURE,
                DispositionModifierExtension.UNEXPECTED_PROCESSING_ERROR);

        public static readonly Disposition UNSUPPORTED_MIC_ALGORITHMS = new Disposition(
                DispositionType.FAILED, DispositionModifier.FAILURE,
                DispositionModifierExtension.UNSUPPORTED_MIC_ALGORITHMS);

        public static readonly Disposition SENDER_EQUALS_RECEIVER = new Disposition(
                DispositionType.FAILED, DispositionModifier.FAILURE,
                DispositionModifierExtension.SENDER_EQUALS_RECEIVER);

        public static readonly Disposition DECRYPTION_FAILED = new Disposition(
                DispositionType.FAILED, DispositionModifier.ERROR,
                DispositionModifierExtension.DECRYPTION_FAILED);

        public static readonly Disposition AUTHENTICATION_FAILED = new Disposition(
                DispositionType.FAILED, DispositionModifier.ERROR,
                DispositionModifierExtension.AUTHENTICATION_FAILED);

        public static readonly Disposition INTEGRITY_CHECK_FAILED = new Disposition(
                DispositionType.FAILED, DispositionModifier.ERROR,
                DispositionModifierExtension.INTEGRITY_CHECK_FAILED);

        public static readonly Disposition PARTICIPANT_NOT_ACCEPTED = new Disposition(
                DispositionType.FAILED, DispositionModifier.ERROR,
                DispositionModifierExtension.PARTICIPANT_NOT_ACCEPTED);

        public static readonly Disposition DOCUMENT_TYPE_ID_NOT_ACCEPTED = new Disposition(
                DispositionType.FAILED, DispositionModifier.ERROR,
                DispositionModifierExtension.DOCUMENT_TYPE_ID_NOT_ACCEPTED);

        public static readonly Disposition PROCESS_ID_NOT_ACCEPTED = new Disposition(
                DispositionType.FAILED, DispositionModifier.ERROR,
                DispositionModifierExtension.PROCESS_ID_NOT_ACCEPTED);

        public static readonly Disposition UNEXPECTED_PROCESSING_ERROR = new Disposition(
                DispositionType.FAILED, DispositionModifier.ERROR,
                DispositionModifierExtension.UNEXPECTED_PROCESSING_ERROR);

        public static readonly Disposition DUPLICATE_DOCUMENT = new Disposition(
                DispositionType.PROCESSED, DispositionModifier.WARNING,
                DispositionModifierExtension.DUPLICATE_DOCUMENT);

        private static Dictionary<VerifierException.Reason, Disposition> verifierMap =
            new Dictionary<VerifierException.Reason, Disposition>()
                {
                    {
                        VerifierException.Reason.DOCUMENT_TYPE,
                        DOCUMENT_TYPE_ID_NOT_ACCEPTED
                    },
                    {
                        VerifierException.Reason.PROCESS,
                        PROCESS_ID_NOT_ACCEPTED
                    },
                    {
                        VerifierException.Reason.PARTICIPANT,
                        PARTICIPANT_NOT_ACCEPTED
                    }
                };

        private DispositionType type;

        private DispositionModifier modifier;

        private DispositionModifierExtension extension;

        public static Disposition parse(String str)
        {
            // TODO: Verify correct parsing!!!!!
            // Matcher matcher = PATTERN.matcher(str);
            String cleaned = str.ReplaceAll("[ \r\n\t]+", " ");

            var matches = PATTERN.Matches(cleaned);

            if (matches.Count > 0)
            {
                if (string.IsNullOrWhiteSpace(matches[1].Value))
                    return new Disposition(
                            DispositionType.of(matches[6].Value),
                            null, null
                    );
                else
                    return new Disposition(
                            DispositionType.of(matches[2].Value),
                            DispositionModifier.of(matches[3].Value),
                            DispositionModifierExtension.of(matches[4].Value)
                    );
            }

            throw new ArgumentException(String.Format("Unable to parse disposition '{0}'.", str));
        }

        public static Disposition fromVerifierException(VerifierException e)
        {
            return verifierMap[e.getReason()];
        }

        private Disposition(DispositionType type, DispositionModifier modifier, DispositionModifierExtension extension)
        {
            this.type = type;
            this.modifier = modifier;
            this.extension = extension;
        }

        public DispositionType getType()
        {
            return type;
        }

        public DispositionModifier getModifier()
        {
            return modifier;
        }

        public DispositionModifierExtension getExtension()
        {
            return extension;
        }

        public override string ToString()
        {
            if (modifier == null)
            {
                return String.Format("{0}; {1}", SENT_AUTOMATICALLY, type);
            }

            return String.Format("{0}; {1}/{2}: {3}", SENT_AUTOMATICALLY, type, modifier, extension);
        }
    }

}
