﻿using System;
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
            return lookup(ParticipantIdentifier.of(identifier));
        }

        public Uri lookup(ParticipantIdentifier participantIdentifier);
    }
}