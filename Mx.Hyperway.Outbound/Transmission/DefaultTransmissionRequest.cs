namespace Mx.Hyperway.Outbound.Transmission
{
    using System.IO;

    using Mx.Hyperway.Api.Outbound;
    using Mx.Peppol.Common.Model;

    /// <summary>
    /// Describes a request to transmit a payload (PEPPOL Document) to a designated end-point.
    /// Instances of this class are to be deemed as value objects, as they are immutable.
    /// </summary>
    public class DefaultTransmissionRequest : ITransmissionRequest
    {
        private readonly Endpoint endpoint;

        private readonly Header header;

        private readonly Stream payload;
        
        /// <summary>
        /// Module private constructor grabbing the constructor data from the supplied builder.
        /// </summary>
        public DefaultTransmissionRequest(Header header, Stream inputStream, Endpoint endpoint)
        {
            this.endpoint = endpoint;
            this.header = header;
            this.payload = inputStream;
        }

        public DefaultTransmissionRequest(ITransmissionMessage transmissionMessage, Endpoint endpoint)
        {
            this.endpoint = endpoint;
            this.header = transmissionMessage.GetHeader();
            this.payload = transmissionMessage.GetPayload();
        }


        public Endpoint GetEndpoint()
        {
            return this.endpoint;
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
