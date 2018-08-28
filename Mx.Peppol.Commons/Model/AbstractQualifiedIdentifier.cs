
namespace Mx.Peppol.Common.Model
{
    using Mx.Peppol.Common.Api;
    using Mx.Peppol.Common.Util;

    public abstract class AbstractQualifiedIdentifier : IQualifiedIdentifier
    {
        protected AbstractQualifiedIdentifier(string identifier, Scheme scheme)
        {
            this.Scheme = scheme;
            this.Identifier = identifier?.Trim();
        }

        public Scheme Scheme { get; }

        public string Identifier { get; }

        public string Urlencoded()
        {
            return ModelUtils.Urlencode("{0}::{1}", this.Scheme.Identifier, this.Identifier);
        }
    }
}
