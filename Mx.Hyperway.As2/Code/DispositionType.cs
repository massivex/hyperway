namespace Mx.Hyperway.As2.Code
{
    using System;
    using System.Collections.Generic;

    public class DispositionType
    {
        public static readonly DispositionType Processed = new DispositionType("processed");

        public static readonly DispositionType Failed = new DispositionType("failed");

        private string code;

        public static DispositionType Of(String str)
        {
            foreach (DispositionType modifier in Values())
            {
                if (modifier.code.Equals(str))
                {
                    return modifier;
                }

            }

            throw new ArgumentException(String.Format("Unknown disposition type: {0}", str));
        }

        private DispositionType(String code)
        {
            this.code = code;
        }

        public override string ToString()
        {
            return this.code;
        }

        public static IEnumerable<DispositionType> Values()
        {
            return new[] { Processed, Failed };
        }
    }
}
