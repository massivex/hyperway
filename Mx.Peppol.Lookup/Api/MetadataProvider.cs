using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Peppol.Lookup.Api
{
    using Mx.Peppol.Common.Model;

    public interface MetadataProvider
    {

        Uri resolveDocumentIdentifiers(Uri location, ParticipantIdentifier participantIdentifier);

        Uri resolveServiceMetadata(Uri location, ParticipantIdentifier participantIdentifier,
                                   DocumentTypeIdentifier documentTypeIdentifier);
    }

}
