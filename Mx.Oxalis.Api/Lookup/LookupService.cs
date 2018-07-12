using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Oxalis.Api.Lookup
{
    using Mx.Peppol.Common.Model;

    using zipkin4net;

    /**
     * Defines a standardized lookup service for use in Oxalis.
     *
     * @author erlend
     * @since 4.0.0
     */
    // TODO: FunctionalInterface ?!?
    // @FunctionalInterface
    public interface LookupService
    {

        /**
         * Performs lookup using metadata from content to be sent.
         *
         * @param header Metadata from content.
         * @return Endpoint information to be used when transmitting content.
         * @throws OxalisTransmissionException Thrown if no endpoint metadata were detected using metadata.
         */
        Endpoint lookup(Header header); // throws OxalisTransmissionException;

        /**
         * Performs lookup using metadata from content to be sent.
         *
         * @param header Metadata from content.
         * @param root   Current trace.
         * @return Endpoint information to be used when transmitting content.
         * @throws OxalisTransmissionException Thrown if no endpoint metadata were detected using metadata.
         */
        Endpoint lookup(Header header, Trace root); // throws OxalisTransmissionException
    }
}
