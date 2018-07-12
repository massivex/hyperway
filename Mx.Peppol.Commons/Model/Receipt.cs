using System;
using System.Collections.Generic;
using System.Text;
using Mx.Peppol.Common.Util;

namespace Mx.Peppol.Common.Model
{
    using System.Linq;

    using Mx.Tools;

    public class Receipt
    {

        private static readonly long serialVersionUID = -2334768925814974368L;

        private readonly String type;

        private readonly byte[] value;

        public static Receipt of(String type, byte[] value)
        {
            return new Receipt(type, value);
        }

        public static Receipt of(byte[] value)
        {
            return of(null, value);
        }

        private Receipt(String type, byte[] value)
        {
            this.type = type;
            this.value = value;
        }

        public String getType()
        {
            return this.type;
        }

        public byte[] getValue()
        {
            return this.value;
        }


        public override bool Equals(Object o)
        {
            if (this == o) return true;
            if (!(o is Receipt)) return false;

            Receipt receipt = (Receipt)o;

            if (!type?.Equals(receipt.type) ?? receipt.type != null) return false;
            return this.value.SequenceEqual(receipt.value);

        }


        public override int GetHashCode()
        {
            int result = this.type != null ? this.type.GetHashCode() : 0;
            result = 31 * result + this.value.GetHashCode();
            return result;
        }


        public override string ToString()
        {
            return "Receipt{" + "type='" + this.type + '\'' + ", value=" + this.value.ToStringValues() + '}';
        }
    }

}
