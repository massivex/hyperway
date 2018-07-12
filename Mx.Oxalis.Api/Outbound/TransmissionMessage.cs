using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Oxalis.Api.Outbound
{
    using System.IO;

    using Mx.Peppol.Common.Model;

    public interface TransmissionMessage
    {

        Header getHeader();

        Stream getPayload();
    }
}
