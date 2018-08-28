using System;

namespace Mx.Peppol.Common.Model
{
    public class InstanceIdentifier : AbstractSimpleIdentifier
    {
        public static InstanceIdentifier GenerateUuid()
        {
            return Of(Guid.NewGuid().ToString());
        }

        public static InstanceIdentifier Of(string value)
        {
            return new InstanceIdentifier(value);
        }

        public InstanceIdentifier(string value)
            : base(value)
        {

        }
    }
}
