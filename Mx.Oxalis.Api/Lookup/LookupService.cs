namespace Mx.Hyperway.Api.Lookup
{
    using Mx.Peppol.Common.Model;

    using zipkin4net;

    public interface LookupService
    {

        /**
         * Performs lookup using metadata from content to be sent.
         */
        Endpoint lookup(Header header);

        /**
         * Performs lookup using metadata from content to be sent.
         */
        Endpoint lookup(Header header, Trace root);
    }
}
