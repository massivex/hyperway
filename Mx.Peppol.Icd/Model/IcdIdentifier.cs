namespace Mx.Peppol.Icd.Model
{
    using Mx.Peppol.Common.Model;
    using Mx.Peppol.Icd.Api;

    public class IcdIdentifier
    {
        public static IcdIdentifier Of(IIcd icd, string identifier)
        {
            return new IcdIdentifier(icd, identifier);
        }

        private IcdIdentifier(IIcd icd, string identifier)
        {
            this.Icd = icd;
            this.Identifier = identifier;
        }

        public IIcd Icd { get; }

        public string Identifier { get; }

        public ParticipantIdentifier ToParticipantIdentifier()
        {
            return ParticipantIdentifier.Of($"{this.Icd.Code}:{this.Identifier}", this.Icd.Scheme);
        }

        public override string ToString()
        {
            return $"{this.Icd.Scheme}::{this.Icd.Code}:{this.Identifier}";
        }
    }
}