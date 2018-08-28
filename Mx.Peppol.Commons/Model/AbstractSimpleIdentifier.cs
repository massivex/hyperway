namespace Mx.Peppol.Common.Model
{
    using Mx.Peppol.Common.Api;

    public abstract class AbstractSimpleIdentifier : ISimpleIdentifier
    {
        protected AbstractSimpleIdentifier(string value)
        {
            this.Identifier = value?.Trim();
        }

        public string Identifier { get; }

        public override string ToString()
        {
            return this.Identifier;
        }

        public override bool Equals(object o)
        {
            if (this == o) return true;
            if (!(o is AbstractSimpleIdentifier)) return false;

            AbstractSimpleIdentifier that = (AbstractSimpleIdentifier)o;

            return this.Identifier.Equals(that.Identifier);
        }

        public override int GetHashCode()
        {
            return this.Identifier.GetHashCode();
        }
    }
}
