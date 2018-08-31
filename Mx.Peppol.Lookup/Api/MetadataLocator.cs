using System;

namespace Mx.Peppol.Lookup.Api
{
    using Mx.Peppol.Common.Model;

    public interface IMetadataLocator
    {
        Uri Lookup(string identifier);

        Uri Lookup(ParticipantIdentifier participantIdentifier);
    }
}
