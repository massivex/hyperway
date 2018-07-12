using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Oxalis.Api.Transmission
{
    using Mx.Oxalis.Api.Model;
    using Mx.Peppol.Common.Model;

    public interface TransmissionVerifier
    {

        void verify(Header header, Direction direction); // throws VerifierException;
    }
}
