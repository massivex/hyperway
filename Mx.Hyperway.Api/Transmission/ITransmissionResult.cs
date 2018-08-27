namespace Mx.Hyperway.Api.Transmission
{
    using System;
    using System.Collections.Generic;

    using Mx.Hyperway.Api.Model;
    using Mx.Peppol.Common.Model;

    public interface ITransmissionResult
    {
        /// <summary>
        /// Transmission id assigned during transmission 
        /// </summary>
        TransmissionIdentifier GetTransmissionIdentifier();

        Header GetHeader();

        DateTime GetTimestamp();

        Digest GetDigest();

        TransportProtocol GetTransportProtocol();

        /// <summary>
        /// The protocol used for the transmission 
        /// </summary>
        TransportProfile GetProtocol();

        IList<Receipt> GetReceipts();

        Receipt PrimaryReceipt();
    }
}
