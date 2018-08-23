namespace Mx.Hyperway.As2.Code
{
    using System;
    using System.Collections.Generic;

    public class DispositionType
    {
        public static readonly DispositionType PROCESSED = new DispositionType("processed");

        public static readonly DispositionType FAILED = new DispositionType("failed");

        private String code;

        public static DispositionType of(String str)
        {
            foreach (DispositionType modifier in values())
            {
                if (modifier.code.Equals(str))
                {
                    return modifier;
                }

            }

            throw new ArgumentException(String.Format("Unknown disposition type: {0}", str));
        }

        DispositionType(String code)
        {
            this.code = code;
        }

        public override string ToString()
        {
            return this.code;
        }

        public static IEnumerable<DispositionType> values()
        {
            return new[] { PROCESSED, FAILED };
        }
    }
}
