using System;

namespace Mx.Peppol.Common.Model
{
    using Mx.Peppol.Common.Lang;

    /// <summary>
    /// DocumentTypeIdentifier is a combination of XML type and customizationId. 
    /// </summary>
    public class DocumentTypeIdentifier : AbstractQualifiedIdentifier
    {
        public static readonly Scheme DefaultScheme = Scheme.Of("busdox-docid-qns");

        public static DocumentTypeIdentifier Of(string identifier)
        {
            return new DocumentTypeIdentifier(identifier, DefaultScheme);
        }

        public static DocumentTypeIdentifier Of(string identifier, Scheme scheme)
        {
            return new DocumentTypeIdentifier(identifier, scheme);
        }

        public static DocumentTypeIdentifier Parse(string str)
        {
            string[] parts = str.Split(new[] { "::" }, 2, StringSplitOptions.None);
            if (parts.Length != 2)
            {
                throw new PeppolParsingException("Unable to parse document type identifier '{0]'.");
            }

            return Of(parts[1], Scheme.Of(parts[0]));
        }

        protected DocumentTypeIdentifier(string value, Scheme scheme)
            : base(value, scheme)
        {

        }

        public override bool Equals(object o)
        {
            if (this == o) return true;
            if (!(o is DocumentTypeIdentifier)) return false;

            var that = (DocumentTypeIdentifier)o;

            return this.ToString().Equals(that.ToString());
        }


        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }


        public override string ToString()
        {
            return $"{this.Scheme}::{this.Identifier}";
        }
    }
}
