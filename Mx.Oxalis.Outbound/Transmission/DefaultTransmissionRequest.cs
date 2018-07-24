namespace Mx.Hyperway.Outbound.Transmission
{
    using System.IO;

    using Mx.Hyperway.Api.Outbound;
    using Mx.Peppol.Common.Model;

    /**
     * Describes a request to transmit a payload (PEPPOL Document) to a designated end-point.
     * Instances of this class are to be deemed as value objects, as they are immutable.
     *
     */
    public class DefaultTransmissionRequest : TransmissionRequest
    {

        private static readonly long serialVersionUID = -4542158917465140099L;

        private readonly Endpoint endpoint;

        private readonly Header header;

        private readonly Stream payload;

        /**
         * Module private constructor grabbing the constructor data from the supplied builder.
         */
        public DefaultTransmissionRequest(Header header, Stream inputStream, Endpoint endpoint)
        {
            this.endpoint = endpoint;
            this.header = header;
            this.payload = inputStream;
        }

        public DefaultTransmissionRequest(TransmissionMessage transmissionMessage, Endpoint endpoint)
        {
            this.endpoint = endpoint;
            this.header = transmissionMessage.getHeader();
            this.payload = transmissionMessage.getPayload();
        }


        public Endpoint getEndpoint()
        {
            return this.endpoint;
        }


        public Header getHeader()
        {
            return this.header;
        }


        public Stream getPayload()
        {
            return this.payload;
        }
    }
}
