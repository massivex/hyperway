namespace Mx.Hyperway.Api.Transmission
{
    using System;
    using System.Collections.Generic;

    using Mx.Hyperway.Api.Model;
    using Mx.Peppol.Common.Model;

    public interface TransmissionResult
    {
        /**
         * Transmission id assigned during transmission
        */
        TransmissionIdentifier getTransmissionIdentifier();

        Header getHeader();

        DateTime getTimestamp();

        Digest getDigest();

        TransportProtocol getTransportProtocol();

        /**
         * The protocol used for the transmission
         */
        TransportProfile getProtocol();

        IList<Receipt> getReceipts();

        Receipt primaryReceipt();
    }
}
