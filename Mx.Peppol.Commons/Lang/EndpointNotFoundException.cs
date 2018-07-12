using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Peppol.Common.Lang
{
    public class EndpointNotFoundException : PeppolException
    {

        public EndpointNotFoundException(String message)
            : base(message)
        {
        }
    }
}
