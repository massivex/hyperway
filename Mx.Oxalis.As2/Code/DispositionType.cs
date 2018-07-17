using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Oxalis.As2.Code
{
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

        public String ToString()
        {
            return code;
        }

        public static IEnumerable<DispositionType> values()
        {
            return new[] { PROCESSED, FAILED };
        }
    }
}
