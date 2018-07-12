using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Peppol.Common.Model
{
    using Mx.Tools;

    public class ServiceReference
    {

        private DocumentTypeIdentifier documentTypeIdentifier;

        private IEnumerable<ProcessIdentifier> processIdentifiers;

        public static ServiceReference of(DocumentTypeIdentifier documentTypeIdentifier, params ProcessIdentifier[] processIdentifiers)
        {
            return new ServiceReference(documentTypeIdentifier, processIdentifiers);
        }

        public static ServiceReference of(DocumentTypeIdentifier documentTypeIdentifier, IEnumerable<ProcessIdentifier> processIdentifiers)
        {
            return new ServiceReference(documentTypeIdentifier, processIdentifiers);
        }

        private ServiceReference(DocumentTypeIdentifier documentTypeIdentifier, IEnumerable<ProcessIdentifier> processIdentifiers)
        {
            this.documentTypeIdentifier = documentTypeIdentifier;
            this.processIdentifiers = processIdentifiers;
        }

        public DocumentTypeIdentifier getDocumentTypeIdentifier()
        {
            return documentTypeIdentifier;
        }

        public IEnumerable<ProcessIdentifier> getProcessIdentifiers()
        {
            return processIdentifiers;
        }

        
        public override bool Equals(Object o)
        {
            if (this == o) return true;
            if (!(o is ServiceReference)) return false;

            ServiceReference that = (ServiceReference)o;
            return Object.Equals(documentTypeIdentifier, that.documentTypeIdentifier) &&
                   Object.Equals(processIdentifiers, that.processIdentifiers);
        }

        public override int GetHashCode()
        {
            return Objects.HashAll(documentTypeIdentifier, processIdentifiers);
        }
    }

}
