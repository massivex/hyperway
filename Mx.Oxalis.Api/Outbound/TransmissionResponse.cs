namespace Mx.Hyperway.Api.Outbound
{
    using System;

    using Mx.Hyperway.Api.Transmission;
    using Mx.Peppol.Common.Model;

    public interface TransmissionResponse : TransmissionResult
    {

        Endpoint getEndpoint();

        /**
         * {@inheritDoc}
         */

        TransportProfile getProtocol();
        //{
        //    return getEndpoint().getTransportProfile();
        //}


        /**
         * Provides access to the native transmission evidence like for instance the MDN for AS2
         */
        [Obsolete]
        byte[] getNativeEvidenceBytes();

        //{
        //    return primaryReceipt().getValue();
        //}
    }
}
