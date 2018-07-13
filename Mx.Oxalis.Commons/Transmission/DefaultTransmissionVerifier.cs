using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Oxalis.Commons.Transmission
{
    using Mx.Oxalis.Api.Model;
    using Mx.Oxalis.Api.Transmission;
    using Mx.Peppol.Common.Model;

    public class DefaultTransmissionVerifier : TransmissionVerifier
    {

        public void verify(Header header, Direction direction)
        {
            // No action.
        }
    }

}
