using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Peppol.Common.Model
{
    using Mx.Peppol.Common.Lang;

    public class ProcessIdentifier : AbstractQualifiedIdentifier
    {

        private static readonly long serialVersionUID = 7486398061021950763L;

    public static readonly Scheme DEFAULT_SCHEME = Scheme.of("cenbii-procid-ubl");

    public static readonly ProcessIdentifier NO_PROCESS = ProcessIdentifier.of("bdx:noprocess", Scheme.of("bdx-procid-transport"));

    public static ProcessIdentifier of(String identifier)
    {
        return new ProcessIdentifier(identifier, DEFAULT_SCHEME);
    }

    public static ProcessIdentifier of(String identifier, Scheme scheme)
    {
        return new ProcessIdentifier(identifier, scheme);
    }

    public static ProcessIdentifier parse(String str) // throws PeppolParsingException
    {
        String [] parts = str.Split(new string[] { "::" }, 2, StringSplitOptions.None);

        if (parts.Length != 2)
        {
            throw new PeppolParsingException($"Unable to parse process identifier '{str}'.");
        }

        return of(parts[1], Scheme.of(parts[0]));
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

            if (!this.identifier.Equals(that.identifier)) return false;
            return this.scheme.Equals(that.scheme);

        }

        
        public override int GetHashCode()
        {
            int result = this.identifier.GetHashCode();
            result = 31 * result + this.scheme.GetHashCode();
            return result;
        }


        public override string ToString()
        {
            return $"{this.scheme}::{this.identifier}";
        }
    }
}
