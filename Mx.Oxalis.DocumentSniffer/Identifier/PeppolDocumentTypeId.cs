namespace Mx.Hyperway.DocumentSniffer.Identifier
{
    using System;
    using System.Text;
    using System.Text.RegularExpressions;

    using Mx.Peppol.Common.Model;

    /**
     * Represents a type safe PEPPOL Document Identifier, textually represented thus:
     * <p>
     * <pre>
     *     {@literal <root NS>::<document element local name>##<customization id>::<version>}
     * </pre>
     */
    public class PeppolDocumentTypeId
    {

        private readonly String rootNameSpace;

        private readonly String localName;

        private readonly CustomizationIdentifier customizationIdentifier;

        private readonly String version;

        /**
         * <pre>
         *     &lt;root NS>::&lt;document element local name>##&lt;customization id>::&lt;version>
         * </pre>
         */
        static Regex documentIdPattern = new Regex("(.*)::(.*)##(.*)::(.*)", RegexOptions.Compiled);

        public PeppolDocumentTypeId(
            String rootNameSpace,
            String localName,
            CustomizationIdentifier customizationIdentifier,
            String version)
        {
            this.rootNameSpace = rootNameSpace;
            this.localName = localName;
            this.customizationIdentifier = customizationIdentifier;
            this.version = version;
        }

        /**
         * Parses the supplied text string into the separate components of a PEPPOL Document Identifier.
         *
         * @param documentIdAsText textual representation of a document identifier.
         * @return type safe instance of DocumentTypeIdentifier
         */
        public static PeppolDocumentTypeId valueOf(String documentIdAsText)
        {
            if (documentIdAsText == null)
            {
                throw new ArgumentNullException("Value 'null' is not a valid document type identifier.");
            }

            Match matcher = documentIdPattern.Match(documentIdAsText.Trim());
            if (matcher.Success)
            {
                String rootNameSpace = matcher.Groups[1].Value;
                String localName = matcher.Groups[2].Value;
                String customizationIdAsText = matcher.Groups[3].Value;
                String version = matcher.Groups[4].Value;
                CustomizationIdentifier customizationIdentifier =
                    CustomizationIdentifier.valueOf(customizationIdAsText);
                return new PeppolDocumentTypeId(rootNameSpace, localName, customizationIdentifier, version);
            }
            else
                throw new ArgumentException(
                    $"Unable to parse '{documentIdAsText}' into PEPPOL Document Type Identifier");
        }

        /**
         * Provides a textual representation of this document type identifier
         *
         * @return textual value.
         */
        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.rootNameSpace);
            sb.Append("::").Append(this.localName);
            sb.Append("##").Append(this.customizationIdentifier);
            sb.Append("::").Append(this.version);
            return sb.ToString();
        }

        public String getRootNameSpace()
        {
            return this.rootNameSpace;
        }

        public String getLocalName()
        {
            return this.localName;
        }

        public CustomizationIdentifier getCustomizationIdentifier()
        {
            return this.customizationIdentifier;
        }

        public String getVersion()
        {
            return this.version;
        }

        public bool Equals(Object o)
        {
            if (this == o) return true;
            if (!(o is PeppolDocumentTypeId)) return false;
            PeppolDocumentTypeId that = (PeppolDocumentTypeId)o;
            if (!this.customizationIdentifier.Equals(that.customizationIdentifier)) return false;
            if (!this.localName.Equals(that.localName)) return false;
            if (!this.rootNameSpace.Equals(that.rootNameSpace)) return false;
            if (!this.version.Equals(that.version)) return false;
            return true;
        }


        public override int GetHashCode()
        {
            int result = this.rootNameSpace.GetHashCode();
            result = 31 * result + this.localName.GetHashCode();
            result = 31 * result + this.customizationIdentifier.GetHashCode();
            result = 31 * result + this.version.GetHashCode();
            return result;
        }

        public DocumentTypeIdentifier toVefa()
        {
            return DocumentTypeIdentifier.of(this.ToString());
        }
    }
}
