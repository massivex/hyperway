namespace Mx.Hyperway.Api.Lookup
{
    using Mx.Peppol.Common.Model;

    using zipkin4net;

    public interface ILookupService
    {
        /// <summary>
        /// Performs lookup using metadata from content to be sent.
        /// </summary>
        Endpoint Lookup(Header header);

        /// <summary>
        /// Performs lookup using metadata from content to be sent.
        /// </summary>
        Endpoint Lookup(Header header, Trace root);
    }
}
