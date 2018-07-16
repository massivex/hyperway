using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Oxalis.Outbound.Lookup
{
    using Mx.Peppol.Lookup.Fetcher;
    using Mx.Peppol.Mode;

    public class OxalisApacheFetcher : BasicApacheFetcher
    {
        public OxalisApacheFetcher(Mode mode)
            : base(mode)
        {
        }
    }
}
