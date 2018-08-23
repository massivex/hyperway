using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Hyperway.As2.Inbound
{
    using Mx.Hyperway.Api.Inbound;
    using Mx.Hyperway.Api.Model;
    using Mx.Hyperway.Api.Timestamp;
    using Mx.Peppol.Common.Model;
    using Org.BouncyCastle.X509;

    public class As2InboundMetadata : InboundMetadata
    {
        private readonly TransmissionIdentifier transmissionIdentifier;

        private readonly Header header;

        private readonly DateTime timestamp;

        private readonly TransportProfile transportProfile;

        private readonly Digest digest;

        private readonly Receipt receipt;

        private readonly List<Receipt> receipts;

        private readonly Org.BouncyCastle.X509.X509Certificate certificate;

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
            if (timestamp.getReceipt() != null)
            {
                this.receipts.Add(timestamp.getReceipt());
            }
            this.certificate = certificate;
        }

        public TransmissionIdentifier getTransmissionIdentifier()
        {
            return this.transmissionIdentifier;
        }

        public Header getHeader()
        {
            return this.header;
        }

        public DateTime getTimestamp()
        {
            return this.timestamp;
        }

        public Digest getDigest()
        {
            return this.digest;
        }

        public TransportProtocol getTransportProtocol()
        {
            return TransportProtocol.AS2;
        }

        public TransportProfile getProtocol()
        {
            return this.transportProfile;
        }

        public IList<Receipt> getReceipts()
        {
            return this.receipts;
        }

        public Receipt primaryReceipt()
        {
            return this.receipt;
        }

        public X509Certificate getCertificate()
        {
            return this.certificate;
        }
    }
}
