namespace Mx.Peppol.Common.Model
{
    using Mx.Peppol.Common.Api;

    public class Unsigned<T> : IPotentiallySigned<T>
    {
        public static Unsigned<T> Of(T content)
        {
            return new Unsigned<T>(content);
        }

        private Unsigned(T content)
        {
            this.Content = content;
        }


        public T Content { get; }


        public IPotentiallySigned<TS> OfSubset<TS>(TS s)
        {
            return new Unsigned<TS>(s);
        }


        public override bool Equals(object o)
        {
            if (this == o) return true;
            if (!(o is Unsigned<T>)) return false;

            Unsigned<T> unsigned = (Unsigned<T>)o;

            return this.Content.Equals(unsigned.Content);

        }

        public override int GetHashCode()
        {
            return this.Content.GetHashCode();
        }


        public override string ToString()
        {
            return "Unsigned{" + "content=" + this.Content + '}';
        }
    }
}
