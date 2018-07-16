using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Peppol.Lookup.Provider
{
    using Mx.Peppol.Common.Model;
    using Mx.Peppol.Lookup.Api;

    public class DefaultProvider : MetadataProvider
    {

        public Uri resolveDocumentIdentifiers(Uri location, ParticipantIdentifier participant)
        {
            var relativeUri = new Uri($"/{participant.urlencoded()}");
            return new Uri(location, relativeUri);
        }

        public Uri resolveServiceMetadata(
            Uri location,
            ParticipantIdentifier participantIdentifier,
            DocumentTypeIdentifier documentTypeIdentifier)
        {
            var relativeUriText =
                $"/{participantIdentifier.urlencoded()}/services/{documentTypeIdentifier.urlencoded()}";
            var relativeUri = new Uri(relativeUriText);
            return new Uri(location, relativeUri);

        }
    }
}