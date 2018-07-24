namespace Mx.Hyperway.Outbound.Lookup
{
    using Mx.Peppol.Lookup.Fetcher;
    using Mx.Peppol.Mode;

    public class HyperwayHttpFetcher : BasicHttpFetcher
    {
        public HyperwayHttpFetcher(Mode mode)
            : base(mode)
        {
        }
    }
}
