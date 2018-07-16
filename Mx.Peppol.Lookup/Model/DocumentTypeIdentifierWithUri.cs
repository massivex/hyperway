using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Peppol.Lookup.Model
{
    using Mx.Peppol.Common.Model;

    public class DocumentTypeIdentifierWithUri : DocumentTypeIdentifier
    {

    private readonly Uri uri;

    public static DocumentTypeIdentifierWithUri of(String identifier, Scheme scheme, Uri uri)
    {
        return new DocumentTypeIdentifierWithUri(identifier, scheme, uri);
    }

    private DocumentTypeIdentifierWithUri(String identifier, Scheme scheme, Uri uri): base(identifier, scheme)
    {
        this.uri = uri;
    }

    public Uri getUri()
    {
        return this.uri;
    }
    }
}
