namespace Mx.Hyperway.As2.Model
{
    using System;
    using System.Collections.Generic;

    using Mx.Tools;

    public class SendingMode
    {
        public static readonly SendingMode Manual = new SendingMode("MDN-sent-manually");

        public static readonly SendingMode Automatic = new SendingMode("MDN-sent-automatically");

        private readonly String textValue;

        SendingMode(String textValue)
        {
            this.textValue = textValue;
        }

        public String GetTextValue()
        {
            return this.textValue;
        }

        public static IEnumerable<SendingMode> Values()
        {
            return new[] { Manual, Automatic };
        }

        public static SendingMode CreateFromTextValue(String textValue)
        {
            foreach (SendingMode sendingMode in Values())
            {
                if (sendingMode.GetTextValue().EqualsIgnoreCase(textValue))
                {
                    return sendingMode;
                }
            }

            throw new ArgumentException(textValue.ToLowerInvariant() + " not recognised as a valid ActionMode");
        }
    }
}