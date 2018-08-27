using System;
using System.Collections.Generic;

namespace Mx.Hyperway.As2.Inbound
{
    using Mx.Hyperway.Api.Inbound;
    using Mx.Hyperway.Api.Model;
    using Mx.Hyperway.Api.Timestamp;
    using Mx.Peppol.Common.Model;
    using Org.BouncyCastle.X509;

    public class As2InboundMetadata : IInboundMetadata
    {
        private readonly TransmissionIdentifier transmissionIdentifier;

        private readonly Header header;

        private readonly DateTime timestamp;

        private readonly TransportProfile transportProfile;

        private readonly Digest digest;

        private readonly Receipt receipt;

        private readonly List<Receipt> receipts;

        private readonly X509Certificate certificate;

        public As2InboundMetadata(
            TransmissionIdentifier transmissionIdentifier,
            Header header,
            Timestamp timestamp,
            TransportProfile transportProfile,
            Digest digest,
            X509Certificate certificate,
            byte[] primaryReceipt)
        {
            this.transmissionIdentifier = transmissionIdentifier;
            this.header = header;
            this.timestamp = this.timestamp.Date;
            this.transportProfile = transportProfile;
            this.digest = digest;
            this.receipt = Receipt.of("message/disposition-notification", primaryReceipt);
            this.receipts = new List<Receipt>();
            this.receipts.Add(this.receipt);
            if (timestamp.GetReceipt() != null)
            {
                this.receipts.Add(timestamp.GetReceipt());
            }
            this.certificate = certificate;
        }

        public TransmissionIdentifier GetTransmissionIdentifier()
        {
            return this.transmissionIdentifier;
        }

        public Header GetHeader()
        {
            return this.header;
        }

        public DateTime GetTimestamp()
        {
            return this.timestamp;
        }

        public Digest GetDigest()
        {
            return this.digest;
        }

        public TransportProtocol GetTransportProtocol()
        {
            return TransportProtocol.AS2;
        }

        public TransportProfile GetProtocol()
        {
            return this.transportProfile;
        }

        public IList<Receipt> GetReceipts()
        {
            return this.receipts;
        }

        public Receipt PrimaryReceipt()
        {
            return this.receipt;
        }

        public X509Certificate GetCertificate()
        {
            return this.certificate;
        }
    }
}
