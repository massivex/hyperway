using System;

namespace Mx.Peppol.Lookup.Api
{
    using System.IO;

    public class FetcherResponse
    {
        public FetcherResponse(Stream inputStream): this(inputStream, null) { }

        public FetcherResponse(Stream inputStream, String ns)
        {
            this.InputStream = inputStream;
            this.Namespace = ns;
        }

        public Stream InputStream { get; }

        public string Namespace { get; }
    }
}
