using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Peppol.Common.Model
{
    using Mx.Peppol.Common.Api;

    public abstract class AbstractSimpleIdentifier : SimpleIdentifier
    {

        protected readonly String value;

        protected AbstractSimpleIdentifier(String value)
        {
            this.value = value == null ? null : value.Trim();
        }

        public String getIdentifier()
        {
            return value;
        }

        public override String ToString()
        {
            return value;
        }

        public override bool Equals(Object o)
        {
            if (this == o) return true;
            if (!(o is AbstractSimpleIdentifier)) return false;

            AbstractSimpleIdentifier that = (AbstractSimpleIdentifier)o;

            return value.Equals(that.value);
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }
    }
}
