using System;
using System.Collections.Generic;

namespace Mx.Peppol.Common.Model
{
    using Mx.Tools;

    public class ServiceReference
    {
        public static ServiceReference Of(DocumentTypeIdentifier documentTypeIdentifier, params ProcessIdentifier[] processIdentifiers)
        {
            return new ServiceReference(documentTypeIdentifier, processIdentifiers);
        }

        public static ServiceReference Of(DocumentTypeIdentifier documentTypeIdentifier, IEnumerable<ProcessIdentifier> processIdentifiers)
        {
            return new ServiceReference(documentTypeIdentifier, processIdentifiers);
        }

        private ServiceReference(DocumentTypeIdentifier documentTypeIdentifier, IEnumerable<ProcessIdentifier> processIdentifiers)
        {
            this.DocumentTypeIdentifier = documentTypeIdentifier;
            this.ProcessIdentifiers = processIdentifiers;
        }

        public DocumentTypeIdentifier DocumentTypeIdentifier { get; }

        public IEnumerable<ProcessIdentifier> ProcessIdentifiers { get; }

        public override bool Equals(Object o)
        {
            if (this == o) return true;
            if (!(o is ServiceReference)) return false;

            ServiceReference that = (ServiceReference)o;
            return Object.Equals(this.DocumentTypeIdentifier, that.DocumentTypeIdentifier) &&
                   Object.Equals(this.ProcessIdentifiers, that.ProcessIdentifiers);
        }

        public override int GetHashCode()
        {
            return Objects.HashAll(this.DocumentTypeIdentifier, this.ProcessIdentifiers);
        }
    }

}
