namespace Mx.Peppol.Common.Model
{
    public class InstanceType
    {
        public static InstanceType Of(string standard, string type, string version)
        {
            return new InstanceType(standard, type, version);
        }

        public InstanceType(string standard, string type, string version)
        {
            this.Standard = standard;
            this.Type = type;
            this.Version = version;
        }

        public string Standard { get; }

        public string Type { get; }

        public string Version { get; }

        public override bool Equals(object o)
        {
            if (this == o) return true;
            if (!(o is InstanceType)) return false;

            InstanceType that = (InstanceType)o;

            if (!this.Standard.Equals(that.Standard)) return false;
            if (!this.Type.Equals(that.Type)) return false;
            return this.Version.Equals(that.Version);
        }

        public override int GetHashCode()
        {
            int result = this.Standard.GetHashCode();
            result = 31 * result + this.Type.GetHashCode();
            result = 31 * result + this.Version.GetHashCode();
            return result;
        }


        public override string ToString()
        {
            return $"{this.Standard}::{this.Type}::{this.Version}";
        }
    }
}
