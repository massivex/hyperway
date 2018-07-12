using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Peppol.Common.Model
{
    /**
     * DocumentTypeIdentifier is a combination of XML type and customizationId.
     */
    public class DocumentTypeIdentifier : AbstractQualifiedIdentifier
    {

    private static readonly long serialVersionUID = -3748163459655880167L;

    public static readonly Scheme DEFAULT_SCHEME = Scheme.of("busdox-docid-qns");

    public static DocumentTypeIdentifier of(String identifier)
    {
        return new DocumentTypeIdentifier(identifier, DEFAULT_SCHEME);
    }

    public static DocumentTypeIdentifier of(String identifier, Scheme scheme)
    {
        return new DocumentTypeIdentifier(identifier, scheme);
    }

    public static DocumentTypeIdentifier parse(String str) // throws PeppolParsingException
    {
        String[] parts;
        // TODO: different split method
        throw new NotSupportedException();
        // parts = str.Split(new string[] { "::" },  2);
        // if (parts.length != 2)
        // throw new PeppolParsingException(String.format("Unable to parse document type identifier '%s'.", str));
        // return of(parts[1], Scheme.of(parts[0]));
    }

    protected DocumentTypeIdentifier(String value, Scheme scheme): base(value, scheme)
    {
        
    }

    
    public override bool Equals(Object o)
    {
        if (this == o) return true;
        if (!(o is DocumentTypeIdentifier)) return false;

        DocumentTypeIdentifier that = (DocumentTypeIdentifier)o;

        return this.ToString().Equals(that.ToString());
    }


    public override int GetHashCode()
    {
        return this.ToString().GetHashCode();
    }

    
    public override String ToString()
    {
        return $"{this.scheme}::{this.identifier}";
    }
}

}
