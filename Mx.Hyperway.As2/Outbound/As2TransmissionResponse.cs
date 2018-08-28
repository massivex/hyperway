namespace Mx.Hyperway.As2.Outbound
{
    using System;
    using System.Collections.Generic;

    using Mx.Hyperway.Api.Model;
    using Mx.Hyperway.Api.Outbound;
    using Mx.Hyperway.Api.Timestamp;
    using Mx.Peppol.Common.Model;
    using Mx.Tools;

    /// <summary>
    /// Implementation of {@link TransmissionResponse} for use in AS2. 
    /// </summary>
    public class As2TransmissionResponse : ITransmissionResponse
    {
        /// <summary>
        /// Original transmission request is kept to allow easy access to immutable objects part of the request. 
        /// </summary>
        private readonly ITransmissionRequest transmissionRequest;

        private readonly TransmissionIdentifier transmissionIdentifier;

        private readonly Digest digest;

        private readonly Receipt receipt;

        private readonly ImmutableList<Receipt> receipts;

        private readonly DateTime timestamp;

        public As2TransmissionResponse(
            TransmissionIdentifier transmissionIdentifier,
            ITransmissionRequest transmissionRequest,
            Digest digest,
            byte[] nativeEvidenceBytes,
            Timestamp timestamp,
            DateTime date)
        {
            this.transmissionIdentifier = transmissionIdentifier;
            this.transmissionRequest = transmissionRequest;
            this.digest = digest;
            this.receipt = Receipt.Of("message/disposition-notification", nativeEvidenceBytes);
            this.timestamp = date;

            List<Receipt> allReceipts = new List<Receipt>();
            allReceipts.Add(this.receipt);
            if (timestamp.GetReceipt() != null)
            {
                allReceipts.Add(timestamp.GetReceipt());
            }


            this.receipts = new ImmutableList<Receipt>(allReceipts);
        }

        public Header GetHeader()
        {
            return this.transmissionRequest.GetHeader();
        }

        public TransmissionIdentifier GetTransmissionIdentifier()
        {
            return this.transmissionIdentifier;
        }

        public byte[] GetNativeEvidenceBytes()
        {
            return this.PrimaryReceipt().Value;
        }

        public TransportProfile GetProtocol()
        {
            return this.GetEndpoint().TransportProfile;
        }

        public IList<Receipt> GetReceipts()
        {
            return this.receipts;
        }


        public Endpoint GetEndpoint()
        {
            return this.transmissionRequest.GetEndpoint();
        }


        public Receipt PrimaryReceipt()
        {
            return this.receipt;
        }


        public Digest GetDigest()
        {
            return this.digest;
        }


        public TransportProtocol GetTransportProtocol()
        {
            return TransportProtocol.As2;
        }


        public DateTime GetTimestamp()
        {
            return this.timestamp;
        }
    }

}
