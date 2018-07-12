using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Peppol.Lookup.Api
{
    using Mx.Peppol.Common.Model;

    public interface MetadataLocator
    {

        Uri lookup(String identifier); // throws LookupException;

        Uri lookup(ParticipantIdentifier participantIdentifier); // throws LookupException;
    }

}
