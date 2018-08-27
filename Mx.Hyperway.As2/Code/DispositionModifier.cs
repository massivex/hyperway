namespace Mx.Hyperway.As2.Code
{
    using System;
    using System.Collections.Generic;

    public class DispositionModifier
    {

        public static readonly DispositionModifier Failure = new DispositionModifier("failure");

        public static readonly DispositionModifier Error = new DispositionModifier("error");

        public static readonly DispositionModifier Warning = new DispositionModifier("warning");

        private String code;

        public static DispositionModifier Of(String str)
        {
            foreach (DispositionModifier modifier in Values())
            {
                if (modifier.code.Equals(str))
                {
                    return modifier;
                }
            }

            throw new ArgumentException($"Unknown disposition modifier: {str}");
        }

        private DispositionModifier(String code)
        {
            this.code = code;
        }

        public static IEnumerable<DispositionModifier> Values()
        {
            return new[] { Failure, Error, Warning };
        }

        public override string ToString()
        {
            return this.code;
        }
    }

}
