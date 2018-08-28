namespace Mx.Peppol.Common.Model
{
    using System.Linq;

    using Mx.Tools;

    public class Receipt
    {
        public static Receipt Of(string type, byte[] value)
        {
            return new Receipt(type, value);
        }

        public static Receipt Of(byte[] value)
        {
            return Of(null, value);
        }

        private Receipt(string type, byte[] value)
        {
            this.Type = type;
            this.Value = value;
        }

        public string Type { get; }

        public byte[] Value { get; }

        public override bool Equals(object o)
        {
            if (this == o) return true;
            if (!(o is Receipt)) return false;

            Receipt receipt = (Receipt)o;

            if (!this.Type?.Equals(receipt.Type) ?? receipt.Type != null) return false;
            return this.Value.SequenceEqual(receipt.Value);

        }


        public override int GetHashCode()
        {
            int result = this.Type != null ? this.Type.GetHashCode() : 0;
            result = 31 * result + this.Value.GetHashCode();
            return result;
        }


        public override string ToString()
        {
            return "Receipt{" + "type='" + this.Type + '\'' + ", value=" + this.Value.ToStringValues() + '}';
        }
    }

}
