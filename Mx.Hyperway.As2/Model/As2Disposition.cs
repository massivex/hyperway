namespace Mx.Hyperway.As2.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    using Mx.Tools;

    /**
     * Represents an instance of an AS2 Disposition header to be used in an MDN
     * <pre>
     *           automatic-action/MDN-sent-automatically; processed/error: Unknown recipient
     * </pre>
     */
    public class As2Disposition
    {

        public static Regex pattern = new Regex(
            "(?i)(manual-action|automatic-action)\\s*/\\s*(MDN-sent-automatically|MDN-sent-manually)\\s*;\\s*(processed|failed)\\s*(/\\s*(error|warning|failure)\\s*:\\s*(.*)){0,1}",
            RegexOptions.Compiled);

        ActionMode actionMode;

        SendingMode sendingMode;

        DispositionType dispositionType; // processed | failed

        /**
         * Optional. If present, a warning or an error was issued
         */
        DispositionModifier dispositionModifier;

        public ActionMode getActionMode()
        {
            return this.actionMode;
        }

        public SendingMode getSendingMode()
        {
            return this.sendingMode;
        }

        public DispositionType getDispositionType()
        {
            return this.dispositionType;
        }

        public DispositionModifier getDispositionModifier()
        {
            return this.dispositionModifier;
        }

        public As2Disposition(ActionMode actionMode, SendingMode sendingMode, DispositionType dispositionType)
        {
            this.actionMode = actionMode;
            this.sendingMode = sendingMode;
            this.dispositionType = dispositionType;
        }

        public As2Disposition(
            ActionMode actionMode,
            SendingMode sendingMode,
            DispositionType dispositionType,
            DispositionModifier dispositionModifier)
        {
            this.actionMode = actionMode;
            this.sendingMode = sendingMode;
            this.dispositionType = dispositionType;

            // Only processed/error or processed/warning is allowed
            if (dispositionType == DispositionType.PROCESSED
                && (dispositionModifier.prefix == DispositionModifier.Prefix.FAILURE))
            {
                throw new ArgumentException(
                    "DispositionType 'processed' does not allow a prefix of 'failed'. Only 'error' and 'warning' are allowed");
            }

            this.dispositionModifier = dispositionModifier;
        }

        public static As2Disposition processed()
        {
            return new As2Disposition(ActionMode.AUTOMATIC, SendingMode.AUTOMATIC, DispositionType.PROCESSED);
        }

        public static As2Disposition processedWithWarning(String warningMessage)
        {
            return new As2Disposition(
                ActionMode.AUTOMATIC,
                SendingMode.AUTOMATIC,
                DispositionType.PROCESSED,
                DispositionModifier.warning(warningMessage));
        }

        public static As2Disposition processedWithError(String errorMessage)
        {
            return new As2Disposition(
                ActionMode.AUTOMATIC,
                SendingMode.AUTOMATIC,
                DispositionType.PROCESSED,
                DispositionModifier.error(errorMessage));
        }

        public static As2Disposition failed(String message)
        {
            return new As2Disposition(
                ActionMode.AUTOMATIC,
                SendingMode.AUTOMATIC,
                DispositionType.FAILED,
                DispositionModifier.failed(message));
        }



        public String ToString()
        {
            StringBuilder sb = new StringBuilder(
                this.actionMode.getTextValue() + "/" + this.sendingMode.getTextValue() + "; " + this.dispositionType.getTextValue());
            if (this.dispositionModifier != null)
            {
                sb.Append('/');
                sb.Append(this.dispositionModifier.ToString());
            }

            return sb.ToString();
        }


        public static As2Disposition valueOf(String s)
        {
            s = (s ?? string.Empty).Trim();
            Match matcher = pattern.Match(s);
            if (!matcher.Success)
            {
                throw new ArgumentException("'" + s + "'" + " does not match pattern for As2Disposition");
            }

            String actionModeString = matcher.Groups[1].Value;
            ActionMode actionMode = ActionMode.createFromTextValue(actionModeString);

            String sendingModeString = matcher.Groups[2].Value;
            SendingMode sendingMode = SendingMode.createFromTextValue(sendingModeString);

            String dispositionTypeString = matcher.Groups[3].Value;
            DispositionType dispositionType = DispositionType.valueOf(dispositionTypeString.ToUpperInvariant());

            As2Disposition result;
            if (matcher.Groups[4].Success)
            {
                DispositionModifier dispositionModifier;
                String dispositionModifierPrefixString = matcher.Groups[5].Value;
                String dispositionModifierString = matcher.Groups[6].Value;
                dispositionModifier = new DispositionModifier(
                    DispositionModifier.Prefix.valueOf(dispositionModifierPrefixString.ToUpperInvariant()),
                    dispositionModifierString);
                result = new As2Disposition(actionMode, sendingMode, dispositionType, dispositionModifier);
            }
            else
            {
                result = new As2Disposition(actionMode, sendingMode, dispositionType);
            }

            return result;
        }

        public class ActionMode
        {
            public static readonly ActionMode MANUAL = new ActionMode("manual-action");

            public static readonly ActionMode AUTOMATIC = new ActionMode("automatic-action");

            private readonly String textValue;

            ActionMode(String textValue)
            {

                this.textValue = textValue;
            }

            public String getTextValue()
            {
                return this.textValue;
            }

            public static IEnumerable<ActionMode> values()
            {
                return new[] { MANUAL, AUTOMATIC };
            }

            public static ActionMode createFromTextValue(String textValue)
            {
                foreach (ActionMode actionMode in values())
                {
                    if (actionMode.getTextValue().EqualsIgnoreCase(textValue))
                    {
                        return actionMode;
                    }
                }

                throw new ArgumentException(textValue.ToLowerInvariant() + " not recognised as a valid ActionMode");
            }

        }

        public class SendingMode
        {
            public static readonly SendingMode MANUAL = new SendingMode("MDN-sent-manually");

            public static readonly SendingMode AUTOMATIC = new SendingMode("MDN-sent-automatically");

            private readonly String textValue;

            SendingMode(String textValue)
            {
                this.textValue = textValue;
            }

            public String getTextValue()
            {
                return this.textValue;
            }

            public static IEnumerable<SendingMode> values()
            {
                return new[] { MANUAL, AUTOMATIC };
            }

            public static SendingMode createFromTextValue(String textValue)
            {
                foreach (SendingMode sendingMode in values())
                {
                    if (sendingMode.getTextValue().EqualsIgnoreCase(textValue))
                    {
                        return sendingMode;
                    }
                }

                throw new ArgumentException(textValue.ToLowerInvariant() + " not recognised as a valid ActionMode");
            }
        }

        public class DispositionType
        {
            public static readonly DispositionType PROCESSED = new DispositionType("processed");

            public static readonly DispositionType FAILED = new DispositionType("failed");

            private readonly String textValue;

            DispositionType(String textValue)
            {
                this.textValue = textValue;
            }

            public static IEnumerable<DispositionType> values()
            {
                return new[] { PROCESSED, FAILED };
            }
            public static DispositionType valueOf(string value)
            {
                return values().First(x => x.textValue == value);
            }

            public String getTextValue()
            {
                return this.textValue;
            }

        }

        public class DispositionModifier
        {

            public class Prefix
            {
                public static IEnumerable<Prefix> values()
                {
                    return new[] { Prefix.ERROR, Prefix.FAILURE, Prefix.WARNING };
                }

                public static Prefix valueOf(string value)
                {
                    return values().First(x => x.value == value);
                }
                private string value;

                Prefix(string value)
                {
                    this.value = value;
                }

                public static readonly Prefix ERROR = new Prefix("ERROR");
                public static readonly Prefix WARNING = new Prefix("WARNING");
                public static readonly Prefix FAILURE = new Prefix("FAILURE");
            }

            internal readonly Prefix prefix;

            private readonly String dispositionModifierExtension;

            internal DispositionModifier(Prefix prefix, String dispositionModifierExtension)
            {
                this.prefix = prefix;
                this.dispositionModifierExtension = dispositionModifierExtension;
            }

            public Prefix getPrefix()
            {
                return this.prefix;
            }

            public String getDispositionModifierExtension()
            {
                return this.dispositionModifierExtension;
            }

            public static DispositionModifier authenticationFailedError()
            {
                return new DispositionModifier(Prefix.ERROR, "authentication-failed");
            }

            public static DispositionModifier decompressionFailedError()
            {
                return new DispositionModifier(Prefix.ERROR, "decompression-failed");
            }

            public static DispositionModifier decryptionFailedError()
            {
                return new DispositionModifier(Prefix.ERROR, "decryption-failed");
            }

            public static DispositionModifier insufficientMessageSecurityError()
            {
                return new DispositionModifier(Prefix.ERROR, "insufficient-message-security");
            }

            public static DispositionModifier integrityCheckFailedError()
            {
                return new DispositionModifier(Prefix.ERROR, "integrity-check-failed");
            }

            public static DispositionModifier unexpectedProcessingError()
            {
                return new DispositionModifier(Prefix.ERROR, "unexpected-processing-error");
            }

            public static DispositionModifier warning(String description)
            {
                return new DispositionModifier(Prefix.WARNING, description);
            }

            public static DispositionModifier unsupportedFormatFailure()
            {
                return new DispositionModifier(Prefix.FAILURE, "unsupported format");
            }

            public static DispositionModifier unsupportedMicAlgorithms()
            {
                return new DispositionModifier(Prefix.FAILURE, "unsupported MIC-algorithms");
            }

            public static DispositionModifier failed(String description)
            {
                return new DispositionModifier(Prefix.FAILURE, description);
            }

            public static DispositionModifier error(String description)
            {
                return new DispositionModifier(Prefix.ERROR, description);
            }

            public String ToString()
            {
                return this.prefix + ": " + this.dispositionModifierExtension;
            }
        }
    }

}
