using System;

namespace Mx.Peppol.Common.Lang
{
    public class EndpointNotFoundException : PeppolException
    {

        public EndpointNotFoundException(string message)
            : base(message)
        {
        }
    }
}
