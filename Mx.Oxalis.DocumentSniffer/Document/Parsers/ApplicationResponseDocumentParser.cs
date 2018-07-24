namespace Mx.Hyperway.DocumentSniffer.Document.Parsers
{
    using System;

    using Mx.Peppol.Common.Model;

    /**
     * Parser to retrieves information from PEPPOL Application Response documents.
     * Should be able to decode Catalogue Response, Message Level Response and others based on ApplicationResponse
     *
     * @author thore
     */
    public class ApplicationResponseDocumentParser : AbstractDocumentParser
    {

    public ApplicationResponseDocumentParser(PlainUBLParser parser): base(parser)
    {
        
    }

    public override ParticipantIdentifier getSender()
    {
        String applicationResponse = "//cac:SenderParty/cbc:EndpointID";
        return this.participantId(applicationResponse);
    }

    public override ParticipantIdentifier getReceiver()
    {
        String applicationResponse = "//cac:ReceiverParty/cbc:EndpointID";
        return this.participantId(applicationResponse);
    }
    }

}
