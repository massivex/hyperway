namespace Mx.Hyperway.As2.Outbound
{
    using System;
    using System.Collections.Generic;

    using Mx.Hyperway.Api.Model;
    using Mx.Hyperway.Api.Outbound;
    using Mx.Hyperway.Api.Timestamp;
    using Mx.Peppol.Common.Model;
    using Mx.Tools;

    /**
     * Implementation of {@link TransmissionResponse} for use in AS2.
     *
     * @author steinar
     * @author thore
     * @author erlend
     */
    public class As2TransmissionResponse : TransmissionResponse
    {

        /**
         * Original transmission request is kept to allow easy access to immutable objects part of the request.
         */
        private readonly TransmissionRequest transmissionRequest;

        private readonly TransmissionIdentifier transmissionIdentifier;

        private readonly Digest digest;

        private readonly Receipt receipt;

        private readonly ImmutableList<Receipt> receipts;

        private readonly DateTime timestamp;

        public As2TransmissionResponse(
            TransmissionIdentifier transmissionIdentifier,
            TransmissionRequest transmissionRequest,
            Digest digest,
            byte[] nativeEvidenceBytes,
            Timestamp timestamp,
            DateTime date)
        {
            this.transmissionIdentifier = transmissionIdentifier;
            this.transmissionRequest = transmissionRequest;
            this.digest = digest;
            this.receipt = Receipt.of("message/disposition-notification", nativeEvidenceBytes);
            this.timestamp = date;

            List<Receipt> receipts = new List<Receipt>();
            receipts.Add(this.receipt);
            if (timestamp.getReceipt() != null)
            {
                receipts.Add(timestamp.getReceipt());
            }


            this.receipts = new ImmutableList<Receipt>(receipts);
        }

        public Header getHeader()
        {
            return this.transmissionRequest.getHeader();
        }

        public TransmissionIdentifier getTransmissionIdentifier()
        {
            return this.transmissionIdentifier;
        }

        public byte[] getNativeEvidenceBytes()
        {
            return this.primaryReceipt().getValue();
        }

        public TransportProfile getProtocol()
        {
            return this.getEndpoint().getTransportProfile();
        }

        public IList<Receipt> getReceipts()
        {
            return this.receipts;
        }


        public Endpoint getEndpoint()
        {
            return this.transmissionRequest.getEndpoint();
        }


        public Receipt primaryReceipt()
        {
            return this.receipt;
        }


        public Digest getDigest()
        {
            return this.digest;
        }


        public TransportProtocol getTransportProtocol()
        {
            return TransportProtocol.AS2;
        }


        public DateTime getTimestamp()
        {
            return this.timestamp;
        }
    }

}
