using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Oxalis.DocumentSniffer.Identifier
{
    /**
     * Represents a PEPPOL Customization Identifier contained within a PEPPOL Document Identifier.
     *
     * @author Steinar Overbeck Cook steinar@sendregning.no
     * @author Thore Johnsen thore@sendregning.no
     *
     * @see "PEPPOL Policy for use of identifiers v3.0 of 2014-02-03"
     */
    public class CustomizationIdentifier
    {

        private String value;

        public CustomizationIdentifier(String customizationIdentifier)
        {
            if (customizationIdentifier != null) customizationIdentifier = customizationIdentifier.Trim();
            this.value = customizationIdentifier;
        }

        public static CustomizationIdentifier valueOf(String s)
        {
            return new CustomizationIdentifier(s);
        }

        public override bool Equals(Object o)
        {
            if (this == o) return true;
            if (!(o is CustomizationIdentifier)) return false;
            CustomizationIdentifier that = (CustomizationIdentifier)o;
            return value.Equals(that.value);
        }


        public override int GetHashCode()
        {
            return value.GetHashCode();
        }


        public override String ToString()
        {
            return value;
        }
    }
}