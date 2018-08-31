using System;

namespace Mx.Peppol.Lookup.Locator
{
    using Mx.Peppol.Common.Model;
    using Mx.Peppol.Lookup.Api;

    public abstract class AbstractLocator : IMetadataLocator
    {
        public Uri Lookup(string identifier)
        {
            return this.Lookup(ParticipantIdentifier.Of(identifier));
        }

        public abstract Uri Lookup(ParticipantIdentifier participantIdentifier);
    }
}
