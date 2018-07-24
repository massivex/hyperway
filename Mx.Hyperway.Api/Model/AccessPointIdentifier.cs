namespace Mx.Hyperway.Api.Model
{
    using System;

    /**
     * Unique identifier for a PEPPOL Access Point.
     * <p>
     * This identifier is typically represented by the Common Name (CN) attribute of the distinguished name of the
     * certificate of the Subject.
     * <p>
     * However; the usage of the common name is only a recommendation, not a mandatory rule.
     *
     * @author steinar
     *         Date: 10.02.13
     *         Time: 21:00
     */
    public class AccessPointIdentifier
    {
        private readonly string accessPointIdentifierValue;

        /**
         * Creates an instance using whatever text value is supplied.
         *
         * @param accessPointIdentifierValue the textual representation of the identifier
         */
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
