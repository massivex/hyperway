using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Peppol.Common.Model
{
    using Mx.Peppol.Common.Api;
    using Mx.Peppol.Common.Util;

    public abstract class AbstractQualifiedIdentifier : QualifiedIdentifier
    {

        protected readonly Scheme scheme;

        protected readonly String identifier;

        public AbstractQualifiedIdentifier(String identifier, Scheme scheme)
        {
            this.scheme = scheme;
            this.identifier = identifier == null ? null : identifier.Trim();
        }

        public Scheme getScheme()
        {
            return scheme;
        }

        public String getIdentifier()
        {
            return identifier;
        }

        public String urlencoded()
        {
            return ModelUtils.urlencode("%s::%s", this.scheme.getIdentifier(), this.identifier);
        }
    }
}
