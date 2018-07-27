using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Hyperway.Api.Persist
{
    using System.IO;

    using Mx.Hyperway.Api.Model;
    using Mx.Peppol.Common.Model;

    public interface PayloadPersister
    {

        FileInfo persist(TransmissionIdentifier transmissionIdentifier, Header header, Stream inputStream);
    }
}
