using System;
using System.Collections.Generic;
using System.Text;

using Mx.Oxalis.Api.Outbound;

namespace Mx.Oxalis.Api.Outbound
{
    using System.IO;

    using zipkin4net;
    using zipkin4net.Tracers.Zipkin;

    /**
     * Defines a standardized transmission service interface accepting the InputStream of the content to be sent.
     * <p>
     * Typical implementation:
     * <pre>
     * {@code
     * public TransmissionResponse send(InputStream inputStream) throws IOException, OxalisTransmissionException {
     *      TransmissionRequestFactory transmissionRequestFactory = // Fetch or find locally.
     *      TransmissionRequest transmissionRequest = transmissionRequestFactory.newInstance(inputStream);
     *
     *      Transmitter transmitter = // Fetch or find locally.
     *      TransmissionResponse transmissionResponse = transmitter.transmit(transmissionRequest)
     *
     *      return transmissionResponse;
     * }
     * }
     * </pre>
     */
    public interface TransmissionService
    {

        /**
         * Sends content found in the InputStream.
         *
         * @param inputStream InputStream containing content to be sent.
         * @return Transmission response containing information from the performed transmission.
         * @throws IOException                 Thrown on any IO exception.
         * @throws OxalisTransmissionException Thrown if there were any problems making Oxalis unable to send the content.
         */
        TransmissionResponse send(Stream inputStream);
        //  throws IOException, OxalisTransmissionException, OxalisContentException;

        /**
         * Sends content found in the InputStream.
         *
         * @param inputStream InputStream containing content to be sent.
         * @param root        Current trace.
         * @return Transmission response containing information from the performed transmission.
         * @throws IOException                 Thrown on any IO exception.
         * @throws OxalisTransmissionException Thrown if there were any problems making Oxalis unable to send the content.
         */
        TransmissionResponse send(Stream inputStream, Trace root);
        // throws IOException, OxalisTransmissionException, OxalisContentException { return send(inputStream); }

    }
}
