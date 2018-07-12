using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Oxalis.Api.Outbound
{
    using Mx.Peppol.Common.Model;

    public interface TransmissionRequest : TransmissionMessage
    {
        Endpoint getEndpoint();
    }

}
