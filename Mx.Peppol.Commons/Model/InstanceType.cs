using System;

namespace Mx.Peppol.Common.Model
{
    public class InstanceType
    {

        private static readonly long serialVersionUID = -8577145245367335582L;

        private readonly String standard;

        private readonly String type;

        private readonly String version;

        public static InstanceType of(String standard, String type, String version)
        {
            return new InstanceType(standard, type, version);
        }

        public InstanceType(String standard, String type, String version)
        {
            this.standard = standard;
            this.type = type;
            this.version = version;
        }

        public String getStandard()
        {
            return standard;
        }

        public String getType()
        {
            return type;
        }

        public String getVersion()
        {
            return version;
        }


        public override bool Equals(Object o)
        {
            if (this == o) return true;
            if (!(o is InstanceType)) return false;

            InstanceType that = (InstanceType)o;

            if (!this.standard.Equals(that.standard)) return false;
            if (!this.type.Equals(that.type)) return false;
            return this.version.Equals(that.version);
        }

        public override int GetHashCode()
        {
            int result = this.standard.GetHashCode();
            result = 31 * result + this.type.GetHashCode();
            result = 31 * result + this.version.GetHashCode();
            return result;
        }


        public override string ToString()
        {
            return $"{this.standard}::{this.type}::{this.version}";
        }
    }
}
