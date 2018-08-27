namespace Mx.Hyperway.Api.Model
{
    using System;

    /// <summary>
    /// Unique identifier for a PEPPOL Access Point.
    /// <p>
    /// This identifier is typically represented by the Common Name (CN) attribute of the distinguished name of the certificate of the Subject.
    /// </p>
    /// </summary>
    public class AccessPointIdentifier
    {
        private readonly string accessPointIdentifierValue;

        /// <summary>
        /// Creates an instance using whatever text value is supplied. 
        /// </summary>
        /// <param name="accessPointIdentifierValue">the textual representation of the identifier</param>
        public AccessPointIdentifier(string accessPointIdentifierValue)
        {
            this.accessPointIdentifierValue = accessPointIdentifierValue;
        }

        public override string ToString()
        {
            return this.accessPointIdentifierValue;
        }

        public override bool Equals(Object o)
        {
            if (this == o) return true;
            if (!(o is AccessPointIdentifier)) return false;

            AccessPointIdentifier that = (AccessPointIdentifier)o;

            return this.accessPointIdentifierValue.Equals(that.accessPointIdentifierValue);
        }

        public override int GetHashCode()
        {
            return this.accessPointIdentifierValue.GetHashCode();
        }
    }
}
