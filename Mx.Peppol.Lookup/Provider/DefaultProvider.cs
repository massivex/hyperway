using System;

namespace Mx.Peppol.Lookup.Provider
{
    using Mx.Peppol.Common.Model;
    using Mx.Peppol.Lookup.Api;

    public class DefaultProvider : IMetadataProvider
    {

        public Uri ResolveDocumentIdentifiers(Uri location, ParticipantIdentifier participant)
        {
            var relativeUri = new Uri($"/{participant.Urlencoded()}");
            return new Uri(location, relativeUri);
        }

        public Uri ResolveServiceMetadata(
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