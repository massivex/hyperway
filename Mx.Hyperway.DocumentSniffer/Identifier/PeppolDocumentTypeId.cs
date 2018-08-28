namespace Mx.Hyperway.DocumentSniffer.Identifier
{
    using System;
    using System.Text;
    using System.Text.RegularExpressions;

    using Mx.Peppol.Common.Model;

    /// <summary>
    /// Represents a type safe PEPPOL Document Identifier, textually represented thus:
    /// <![CDATA[ root NS>::<document element local name>##<customization id>::<version> ]]>
    /// </summary>
    public class PeppolDocumentTypeId
    {
        private static readonly Regex DocumentIdPattern = new Regex("(.*)::(.*)##(.*)::(.*)", RegexOptions.Compiled);

        public PeppolDocumentTypeId(
            string rootNameSpace,
            string localName,
            CustomizationIdentifier customizationIdentifier,
            string version)
        {
            this.RootNameSpace = rootNameSpace;
            this.LocalName = localName;
            this.CustomizationIdentifier = customizationIdentifier;
            this.Version = version;
        }

        /// <summary>
        /// Parses the supplied text string into the separate components of a PEPPOL Document Identifier. 
        /// </summary>
        /// <param name="documentIdAsText">textual representation of a document identifier.</param>
        /// <returns>type safe instance of DocumentTypeIdentifier</returns>
        public static PeppolDocumentTypeId ValueOf(string documentIdAsText)
        {
            if (documentIdAsText == null)
            {
                throw new ArgumentNullException(nameof(documentIdAsText), "Value 'null' is not a valid document type identifier.");
            }

            Match matcher = DocumentIdPattern.Match(documentIdAsText.Trim());
            if (matcher.Success)
            {
                string rootNameSpace = matcher.Groups[1].Value;
                string localName = matcher.Groups[2].Value;
                string customizationIdAsText = matcher.Groups[3].Value;
                string version = matcher.Groups[4].Value;
                CustomizationIdentifier customizationIdentifier =
                    CustomizationIdentifier.ValueOf(customizationIdAsText);
                return new PeppolDocumentTypeId(rootNameSpace, localName, customizationIdentifier, version);
            }
            else
                throw new ArgumentException(
                    $"Unable to parse '{documentIdAsText}' into PEPPOL Document Type Identifier");
        }

        /// <summary>
        /// Provides a textual representation of this document type identifier 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.RootNameSpace);
            sb.Append("::").Append(this.LocalName);
            sb.Append("##").Append(this.CustomizationIdentifier);
            sb.Append("::").Append(this.Version);
            return sb.ToString();
        }

        public string RootNameSpace { get;  }
    

        public string LocalName { get; }

        public CustomizationIdentifier CustomizationIdentifier { get; }

        public string Version { get; }

        public override bool Equals(object o)
        {
            if (this == o) return true;
            if (!(o is PeppolDocumentTypeId)) return false;
            PeppolDocumentTypeId that = (PeppolDocumentTypeId)o;
            if (!this.CustomizationIdentifier.Equals(that.CustomizationIdentifier)) return false;
            if (!this.LocalName.Equals(that.LocalName)) return false;
            if (!this.RootNameSpace.Equals(that.RootNameSpace)) return false;
            if (!this.Version.Equals(that.Version)) return false;
            return true;
        }


        public override int GetHashCode()
        {
            int result = this.RootNameSpace.GetHashCode();
            result = 31 * result + this.LocalName.GetHashCode();
            result = 31 * result + this.CustomizationIdentifier.GetHashCode();
            result = 31 * result + this.Version.GetHashCode();
            return result;
        }

        public DocumentTypeIdentifier ToVefa()
        {
            return DocumentTypeIdentifier.Of(this.ToString());
        }
    }
}
