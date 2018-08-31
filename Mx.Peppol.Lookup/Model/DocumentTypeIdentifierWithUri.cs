using System;

namespace Mx.Peppol.Lookup.Model
{
    using Mx.Peppol.Common.Model;

    public class DocumentTypeIdentifierWithUri : DocumentTypeIdentifier
    {
        public static DocumentTypeIdentifierWithUri Of(string identifier, Scheme scheme, Uri uri)
        {
            return new DocumentTypeIdentifierWithUri(identifier, scheme, uri);
        }

        private DocumentTypeIdentifierWithUri(string identifier, Scheme scheme, Uri uri)
            : base(identifier, scheme)
        {
            this.Uri = uri;
        }

        public Uri Uri { get; }
    }
}