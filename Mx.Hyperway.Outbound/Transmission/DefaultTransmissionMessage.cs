namespace Mx.Hyperway.Outbound.Transmission
{
    using System.IO;

    using Mx.Hyperway.Api.Outbound;
    using Mx.Peppol.Common.Model;

    public class DefaultTransmissionMessage : ITransmissionMessage
    {

        private readonly Header header;

        private readonly Stream payload;

        public DefaultTransmissionMessage(Header header, Stream payload)
        {
            this.header = header;
            this.payload = payload;
        }


        public Header GetHeader()
        {
            return this.header;
        }


        public Stream GetPayload()
        {
            return this.payload;
        }
    }
}