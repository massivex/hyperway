namespace Mx.Hyperway.DocumentSniffer.Document.Parsers
{
    using Mx.Peppol.Common.Model;

    /// <summary>
    /// Parser to retrieves information from PEPPOL Application Response documents.
    /// Should be able to decode Catalogue Response, Message Level Response and others based on ApplicationResponse
    /// </summary>
    public class ApplicationResponseDocumentParser : AbstractDocumentParser
    {

    public ApplicationResponseDocumentParser(PlainUblParser parser): base(parser)
    {
        
    }

        public override ParticipantIdentifier Sender
        {
            get
            {
                string applicationResponse = "//cac:SenderParty/cbc:EndpointID";
                return this.ParticipantId(applicationResponse);
            }
        }

        public override ParticipantIdentifier Receiver
        {
            get
            {
                string applicationResponse = "//cac:ReceiverParty/cbc:EndpointID";
                return this.ParticipantId(applicationResponse);
            }
        }
    }

}
