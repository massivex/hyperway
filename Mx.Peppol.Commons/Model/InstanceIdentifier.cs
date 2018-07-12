using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Peppol.Common.Model
{
    public class InstanceIdentifier : AbstractSimpleIdentifier
    {
        public static InstanceIdentifier generateUUID()
        {
            return of(Guid.NewGuid().ToString());
        }

        public static InstanceIdentifier of(String value)
        {
            return new InstanceIdentifier(value);
        }

        public InstanceIdentifier(String value)
            : base(value)
        {

        }
    }
}
