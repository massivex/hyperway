namespace Mx.Hyperway.Api.Outbound
{
    using System.IO;

    using zipkin4net;

    /// <summary>
    /// Defines a standardized transmission service interface accepting the InputStream of the content to be sent. 
    /// </summary>
    public interface ITransmissionService
    {
        /// <summary>
        /// Sends content found in the InputStream. 
        /// </summary>
        /// <param name="inputStream">data</param>
        /// <returns></returns>
        ITransmissionResponse Send(Stream inputStream);

        /// <summary>
        /// Sends content found in the InputStream. 
        /// </summary>
        /// <param name="inputStream">data</param>
        /// <param name="root">root span</param>
        /// <returns></returns>
        ITransmissionResponse Send(Stream inputStream, Trace root);
    }
}
