namespace Mx.Hyperway.DocumentSniffer.Identifier
{
    using System;

    /// <summary>
    /// Represents a PEPPOL Customization Identifier contained within a PEPPOL Document Identifier. 
    /// </summary>
    /// <see>PEPPOL Policy for use of identifiers v3.0 of 2014-02-03</see>
    public class CustomizationIdentifier
    {

        private readonly string value;

        public CustomizationIdentifier(string customizationIdentifier)
        {
            customizationIdentifier = customizationIdentifier?.Trim();
            this.value = customizationIdentifier;
        }

        public static CustomizationIdentifier ValueOf(string s)
        {
            return new CustomizationIdentifier(s);
        }

        public override bool Equals(object o)
        {
            if (this == o) return true;
            if (!(o is CustomizationIdentifier)) return false;
            CustomizationIdentifier that = (CustomizationIdentifier)o;
            return this.value.Equals(that.value);
        }


        public override int GetHashCode()
        {
            return this.value.GetHashCode();
        }


        public override string ToString()
        {
            return this.value;
        }
    }
}