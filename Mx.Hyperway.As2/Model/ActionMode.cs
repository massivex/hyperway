namespace Mx.Hyperway.As2.Model
{
    using System;
    using System.Collections.Generic;

    using Mx.Tools;

    public class ActionMode
    {
        public static readonly ActionMode MANUAL = new ActionMode("manual-action");

        public static readonly ActionMode AUTOMATIC = new ActionMode("automatic-action");

        private ActionMode(String textValue)
        {

            this.TextValue = textValue;
        }

        public string TextValue { get; }

        public static IEnumerable<ActionMode> Values()
        {
            return new[] { MANUAL, AUTOMATIC };
        }

        public static ActionMode CreateFromTextValue(String textValue)
        {
            foreach (var actionMode in Values())
            {
                if (actionMode.TextValue.EqualsIgnoreCase(textValue))
                {
                    return actionMode;
                }
            }

            throw new ArgumentException(textValue.ToLowerInvariant() + " not recognised as a valid ActionMode");
        }

    }
}