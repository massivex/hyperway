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
            var relativeUri = new Uri($"/{participant.Urlencoded()}");
            return new Uri(location, relativeUri);
        }

        public Uri resolveServiceMetadata(
            Uri location,
            ParticipantIdentifier participantIdentifier,
            DocumentTypeIdentifier documentTypeIdentifier)
        {
            var relativeUriText =
                $"/{participantIdentifier.Urlencoded()}/services/{documentTypeIdentifier.Urlencoded()}";
            var relativeUri = new Uri(relativeUriText, UriKind.Relative);
            return new Uri(location, relativeUri);

        }
    }
}