using System;

namespace Mx.Peppol.Common.Model
{
    using Mx.Peppol.Common.Lang;

    public class ProcessIdentifier : AbstractQualifiedIdentifier
    {
        public static readonly Scheme DefaultScheme = Scheme.Of("cenbii-procid-ubl");

        public static readonly ProcessIdentifier NoProcess = Of("bdx:noprocess", Scheme.Of("bdx-procid-transport"));

        public static ProcessIdentifier Of(String identifier)
        {
            return new ProcessIdentifier(identifier, DefaultScheme);
        }

        public static ProcessIdentifier Of(String identifier, Scheme scheme)
        {
            return new ProcessIdentifier(identifier, scheme);
        }

        public static ProcessIdentifier Parse(String str) // throws PeppolParsingException
        {
            String[] parts = str.Split(new[] { "::" }, 2, StringSplitOptions.None);

            if (parts.Length != 2)
            {
                throw new PeppolParsingException($"Unable to parse process identifier '{str}'.");
            }

            return Of(parts[1], Scheme.Of(parts[0]));
        }

        private ProcessIdentifier(String value, Scheme scheme)
            : base(value, scheme)
        {
        }


        public override bool Equals(Object o)
        {
            if (this == o) return true;
            if (!(o is ProcessIdentifier)) return false;

            ProcessIdentifier that = (ProcessIdentifier)o;

            if (!this.Identifier.Equals(that.Identifier)) return false;
            return this.Scheme.Equals(that.Scheme);

        }


        public override int GetHashCode()
        {
            int result = this.Identifier.GetHashCode();
            result = 31 * result + this.Scheme.GetHashCode();
            return result;
        }


        public override string ToString()
        {
            return $"{this.Scheme}::{this.Identifier}";
        }
    }
}