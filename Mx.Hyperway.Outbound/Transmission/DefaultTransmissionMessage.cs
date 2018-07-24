namespace Mx.Hyperway.Outbound.Transmission
{
    using System.IO;

    using Mx.Hyperway.Api.Outbound;
    using Mx.Peppol.Common.Model;

    public class DefaultTransmissionMessage : TransmissionMessage {

    private static readonly long serialVersionUID = -2292244133544793106L;

    private readonly Header header;

    private readonly Stream payload;

    public DefaultTransmissionMessage(Header header, Stream payload)
    {
        this.header = header;
        this.payload = payload;
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
