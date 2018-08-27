namespace Mx.Hyperway.As2.Model
{
    using System;
    using System.Text;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Represents an instance of an AS2 Disposition header to be used in an MDN
    /// <code>automatic-action/MDN-sent-automatically; processed/error: Unknown recipient</code>
    /// </summary>
    public class As2Disposition
    {

        public static Regex pattern = new Regex(
            "(?i)(manual-action|automatic-action)\\s*/\\s*(MDN-sent-automatically|MDN-sent-manually)\\s*;\\s*(processed|failed)\\s*(/\\s*(error|warning|failure)\\s*:\\s*(.*)){0,1}",
            RegexOptions.Compiled);

        ActionMode actionMode;

        SendingMode sendingMode;

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

        public DispositionType DispositionType { get; }

        public DispositionModifier getDispositionModifier()
        {
            return this.dispositionModifier;
        }

        public As2Disposition(ActionMode actionMode, SendingMode sendingMode, DispositionType dispositionType)
        {
            this.actionMode = actionMode;
            this.sendingMode = sendingMode;
            this.DispositionType = dispositionType;
        }

        public As2Disposition(
            ActionMode actionMode,
            SendingMode sendingMode,
            DispositionType dispositionType,
            DispositionModifier dispositionModifier)
        {
            this.actionMode = actionMode;
            this.sendingMode = sendingMode;
            this.DispositionType = dispositionType;

            // Only processed/error or processed/warning is allowed
            if (dispositionType == DispositionType.Processed
                && (dispositionModifier.Prefix == DispositionModifierPrefix.Failure))
            {
                throw new ArgumentException(
                    "DispositionType 'processed' does not allow a prefix of 'failed'. Only 'error' and 'warning' are allowed");
            }

            this.dispositionModifier = dispositionModifier;
        }

        public static As2Disposition processed()
        {
            return new As2Disposition(ActionMode.AUTOMATIC, SendingMode.Automatic, DispositionType.Processed);
        }

        public static As2Disposition processedWithWarning(String warningMessage)
        {
            return new As2Disposition(
                ActionMode.AUTOMATIC,
                SendingMode.Automatic,
                DispositionType.Processed,
                DispositionModifier.Warning(warningMessage));
        }

        public static As2Disposition processedWithError(String errorMessage)
        {
            return new As2Disposition(
                ActionMode.AUTOMATIC,
                SendingMode.Automatic,
                DispositionType.Processed,
                DispositionModifier.Error(errorMessage));
        }

        public static As2Disposition failed(String message)
        {
            return new As2Disposition(
                ActionMode.AUTOMATIC,
                SendingMode.Automatic,
                DispositionType.Failed,
                DispositionModifier.Failed(message));
        }



        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(
                this.actionMode.TextValue + "/" + this.sendingMode.GetTextValue() + "; " + this.DispositionType.GetTextValue());
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
            ActionMode actionMode = ActionMode.CreateFromTextValue(actionModeString);

            String sendingModeString = matcher.Groups[2].Value;
            SendingMode sendingMode = SendingMode.CreateFromTextValue(sendingModeString);

            String dispositionTypeString = matcher.Groups[3].Value;
            DispositionType dispositionType = DispositionType.ValueOf(dispositionTypeString.ToLowerInvariant());

            As2Disposition result;
            if (matcher.Groups[4].Success)
            {
                DispositionModifier dispositionModifier;
                String dispositionModifierPrefixString = matcher.Groups[5].Value;
                String dispositionModifierString = matcher.Groups[6].Value;
                dispositionModifier = new DispositionModifier(
                    DispositionModifierPrefix.ValueOf(dispositionModifierPrefixString.ToUpperInvariant()),
                    dispositionModifierString);
                result = new As2Disposition(actionMode, sendingMode, dispositionType, dispositionModifier);
            }
            else
            {
                result = new As2Disposition(actionMode, sendingMode, dispositionType);
            }

            return result;
        }
    }
}
