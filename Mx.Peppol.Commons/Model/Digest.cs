using System;

namespace Mx.Peppol.Common.Model
{
    using Mx.Peppol.Common.Code;
    using Mx.Tools;

    public class Digest
    {
        private readonly DigestMethod method;

        private readonly byte[] value;

        public static Digest of(DigestMethod method, byte[] value)
        {
            return new Digest(method, value);
        }

        private Digest(DigestMethod method, byte[] value)
        {
            this.method = method;
            this.value = value;
        }

        public DigestMethod getMethod()
        {
            return method;
        }

        public byte[] getValue()
        {
            return value;
        }

        public override bool Equals(Object o)
        {
            if (this == o) return true;
            if (!(o is Digest)) return false;

            Digest digest = (Digest)o;

            if (this.method != digest.method) return false;
            return Array.Equals(this.value, digest.value);

        }


        public override int GetHashCode()
        {
            int result = this.method.GetHashCode();
            result = 31 * result + this.value.GetHashCode();
            return result;
        }


        public override String ToString()
        {
            return "Digest{" + "method=" + this.method + ", value=" + this.value.ToStringValues() + '}';
        }
    }
}
