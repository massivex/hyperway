namespace Mx.Hyperway.DocumentSniffer.Document.Parsers
{
    using Mx.Peppol.Common.Model;

    /// <summary>
    /// Parser to retrieves information from PEPPOL Catalogue scenarios.
    /// Should be able to decode Catalogue (for catalogue response see ApplicationResponse)
    /// </summary>
    public class CatalogueDocumentParser : AbstractDocumentParser
    {

    public CatalogueDocumentParser(PlainUblParser parser) : base(parser)
    {
    }

        public override ParticipantIdentifier Sender
        {
            get
            {
                string catalogue = "//cac:ProviderParty/cbc:EndpointID";
                return this.ParticipantId(catalogue);
            }
        }

        public override ParticipantIdentifier Receiver
        {
            get
            {
                string catalogue = "//cac:ReceiverParty/cbc:EndpointID";
                return this.ParticipantId(catalogue);
            }
        }
    }
}
