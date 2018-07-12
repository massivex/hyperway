using System;

namespace Mx.Peppol.Common.Model
{
    using System.Globalization;

    using Mx.Peppol.Common.Lang;

    public class ParticipantIdentifier : AbstractQualifiedIdentifier
    {

        private static readonly long serialVersionUID = -8052874032415088055L;

        /**
         * Default scheme used when no scheme or ICD specified.
         */
        public static readonly Scheme DEFAULT_SCHEME = Scheme.of("iso6523-actorid-upis");

        public static ParticipantIdentifier of(String value)
        {
            return of(value, DEFAULT_SCHEME);
        }

        public static ParticipantIdentifier of(String value, Scheme scheme)
        {
            return new ParticipantIdentifier(value, scheme);
        }

        public static ParticipantIdentifier parse(String str) // throws PeppolParsingException
        {
            String [] parts = str.Split(new[] { "::" }, 2, StringSplitOptions.None);

            if (parts.Length != 2)
            {
                throw new PeppolParsingException($"Unable to parse participant identifier '{str}'.");
            }
                

            return of(parts[1], Scheme.of(parts[0]));
        }

        /**
         * Creation of participant identifier.
         *
         * @param identifier Normal identifier like '9908:987654321'.
         * @param scheme     Scheme for identifier.
         */
        private ParticipantIdentifier(String identifier, Scheme scheme)
            : base(identifier.Trim().ToLower(CultureInfo.InvariantCulture), scheme)
        {
        }


        public override bool Equals(Object o)
        {
            if (this == o) return true;
            if (!(o is ParticipantIdentifier)) return false;

            ParticipantIdentifier that = (ParticipantIdentifier)o;

            if (!this.scheme.Equals(that.scheme)) return false;
            return this.identifier.Equals(that.identifier);
        }

        public override int GetHashCode()
        {
            int result = this.scheme.GetHashCode();
            result = 31 * result + this.identifier.GetHashCode();
            return result;
        }

        public override String ToString()
        {
            return $"{this.scheme}::{this.identifier}";
        }
    }
}
