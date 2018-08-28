using System;

namespace Mx.Peppol.Common.Model
{
    using Mx.Peppol.Common.Code;
    using Mx.Tools;

    public class Digest
    {
        public static Digest Of(DigestMethod method, byte[] value)
        {
            return new Digest(method, value);
        }

        private Digest(DigestMethod method, byte[] value)
        {
            this.Method = method;
            this.Value = value;
        }

        public DigestMethod Method { get; }

        public byte[] Value { get; }

        public override bool Equals(object o)
        {
            if (this == o) return true;
            if (!(o is Digest)) return false;

            Digest digest = (Digest)o;

            if (this.Method != digest.Method) return false;
            return Array.Equals(this.Value, digest.Value);

        }


        public override int GetHashCode()
        {
            int result = this.Method.GetHashCode();
            result = 31 * result + this.Value.GetHashCode();
            return result;
        }


        public override string ToString()
        {
            return "Digest{" + "method=" + this.Method + ", value=" + this.Value.ToStringValues() + '}';
        }
    }
}
