using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Oxalis.Api.Evidence
{
    using System.IO;

    using Mx.Oxalis.Api.Transmission;

    // @FunctionalInterface
    // TODO: FunctionalInterface
    public interface EvidenceFactory
    {

        void write(Stream outputStream, TransmissionResult transmissionResult); // throws EvidenceException;
    }

}
