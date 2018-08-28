using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Peppol.Lookup.Locator
{
    using Mx.Peppol.Common.Model;
    using Mx.Peppol.Lookup.Api;

    public abstract class AbstractLocator : MetadataLocator
    {
        public Uri lookup(String identifier) // throws LookupException
        {
            return lookup(ParticipantIdentifier.Of(identifier));
        }

        public abstract Uri lookup(ParticipantIdentifier participantIdentifier);
    }
}
