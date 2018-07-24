namespace Mx.Hyperway.As2.Code
{
    using System;
    using System.Collections.Generic;

    public class DispositionModifier
    {

        public static readonly DispositionModifier FAILURE = new DispositionModifier("failure");

        public static readonly DispositionModifier ERROR = new DispositionModifier("error");

        public static readonly DispositionModifier WARNING = new DispositionModifier("warning");

        private String code;

        public static DispositionModifier of(String str)
        {
            foreach (DispositionModifier modifier in values())
            {
                if (modifier.code.Equals(str))
                {
                    return modifier;
                }
            }

            throw new ArgumentException(String.Format("Unknown disposition modifier: {0}", str));
        }

        DispositionModifier(String code)
        {
            this.code = code;
        }

        public static IEnumerable<DispositionModifier> values()
        {
            return new[] { FAILURE, ERROR, WARNING };
        }

        public String ToString()
        {
            return this.code;
        }
    }

}
