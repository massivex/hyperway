using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Oxalis.Api.Outbound
{
    using zipkin4net;

    public interface Transmitter
    {

        /**
         * Transmit content of transmission request. (No tracing.)
         *
         * @param transmissionMessage Content to be transmitted.
         * @return Result of transmission.
         * @throws OxalisTransmissionException Thrown when transmission fails.
         */
        TransmissionResponse transmit(TransmissionMessage transmissionMessage); // throws OxalisTransmissionException;

        /**
            * Transmit content of transmission request. (With tracing.)
            *
            * @param transmissionMessage Content to be transmitted.
            * @param root                Current trace.
            * @return Result of transmission.
            * @throws OxalisTransmissionException Thrown when transmission fails.
            */
        TransmissionResponse
            transmit(TransmissionMessage transmissionMessage, Trace root); // throws OxalisTransmissionException

        //    {
        //    return transmit(transmissionMessage);
        //}
    }
}
