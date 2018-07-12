using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Oxalis.Api.Outbound
{
    using zipkin4net;
    using zipkin4net.Tracers.Zipkin;

    public interface MessageSender
    {

        /**
         * Protocol specific transmission of transmission requested. (Without tracing.)
         *
         * @param transmissionRequest Requested transmission to take place.
         * @return Response content of a successful transmission.
         * @throws OxalisTransmissionException Thrown when transmission was not sent according to protocol specific rules or
         *                                     because something went wrong during transmission.
         */
        TransmissionResponse send(TransmissionRequest transmissionRequest); // throws OxalisTransmissionException;

        /**
            * Protocol specific transmission of transmission requested. (With tracing.)
            *
            * @param transmissionRequest Requested transmission to take place.
            * @param root                Current trace.
            * @return Response content of a successful transmission.
            * @throws OxalisTransmissionException Thrown when transmission was not sent according to protocol specific rules or
            *                                     because something went wrong during transmission.
            */
        TransmissionResponse send(TransmissionRequest transmissionRequest, Trace root); // throws OxalisTransmissionException

        //{
        //    return send(transmissionRequest);
        //}

    }
}