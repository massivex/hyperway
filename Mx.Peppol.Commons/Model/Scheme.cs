namespace Mx.Peppol.Common.Model
{

    public class Scheme : AbstractSimpleIdentifier
    {
        public static readonly Scheme None = Of("NONE");

        public static Scheme Of(string value)
        {
            return new Scheme(value);
        }

        protected Scheme(string value): base(value) { }
    }

}
