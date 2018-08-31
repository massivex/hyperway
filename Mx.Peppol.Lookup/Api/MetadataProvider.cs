using System;

namespace Mx.Peppol.Lookup.Api
{
    using Mx.Peppol.Common.Model;

    public interface IMetadataProvider
    {

        Uri ResolveDocumentIdentifiers(Uri location, ParticipantIdentifier participantIdentifier);

        Uri ResolveServiceMetadata(Uri location, ParticipantIdentifier participantIdentifier,
                                   DocumentTypeIdentifier documentTypeIdentifier);
    }

}
