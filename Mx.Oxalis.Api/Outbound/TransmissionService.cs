namespace Mx.Hyperway.Api.Outbound
{
    using System.IO;

    using zipkin4net;

    /**
     * Defines a standardized transmission service interface accepting the InputStream of the content to be sent.
     */
    public interface TransmissionService
    {

        /**
         * Sends content found in the InputStream.
         */
        TransmissionResponse send(Stream inputStream);

        /**
         * Sends content found in the InputStream.
         */
        TransmissionResponse send(Stream inputStream, Trace root);
    }
}
