using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Oxalis.Api.Transformer
{
    using System.IO;

    using Mx.Peppol.Common.Model;

    public interface ContentDetector
    {
        Header parse(Stream inputStream); // throws OxalisContentException;
    }
}
