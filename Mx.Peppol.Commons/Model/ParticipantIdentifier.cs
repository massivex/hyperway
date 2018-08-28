using System;

namespace Mx.Peppol.Common.Model
{
    using System.Globalization;

    using Mx.Peppol.Common.Lang;

    public class ParticipantIdentifier : AbstractQualifiedIdentifier
    {
        /// <summary>
        /// Default scheme used when no scheme or ICD specified. 
        /// </summary>
        public static readonly Scheme DefaultScheme = Scheme.Of("iso6523-actorid-upis");

        public static ParticipantIdentifier Of(string value)
        {
            return Of(value, DefaultScheme);
        }

        public static ParticipantIdentifier Of(string value, Scheme scheme)
        {
            return new ParticipantIdentifier(value, scheme);
        }

        public static ParticipantIdentifier Parse(string str) // throws PeppolParsingException
        {
            string[] parts = str.Split(new[] { "::" }, 2, StringSplitOptions.None);

            if (parts.Length != 2)
            {
                throw new PeppolParsingException($"Unable to parse participant identifier '{str}'.");
            }
                

            return Of(parts[1], Scheme.Of(parts[0]));
        }

        /// <summary>
        /// Creation of participant identifier. 
        /// </summary>
        /// <param name="identifier">Normal identifier like '9908:987654321'.</param>
        /// <param name="scheme">Scheme for identifier.</param>
        private ParticipantIdentifier(string identifier, Scheme scheme)
            : base(identifier.Trim().ToLower(CultureInfo.InvariantCulture), scheme)
        {
        }


        public override bool Equals(object o)
        {
            if (this == o) return true;
            if (!(o is ParticipantIdentifier)) return false;

            ParticipantIdentifier that = (ParticipantIdentifier)o;

            if (!this.Scheme.Equals(that.Scheme)) return false;
            return this.Identifier.Equals(that.Identifier);
        }

        public override int GetHashCode()
        {
            int result = this.Scheme.GetHashCode();
            result = 31 * result + this.Identifier.GetHashCode();
            return result;
        }

        public override string ToString()
        {
            return $"{this.Scheme}::{this.Identifier}";
        }
    }
}
