namespace Mx.Hyperway.DocumentSniffer.Document.Parsers
{
    using System;

    using Mx.Peppol.Common.Model;

    /**
     * Parser to retrieves information from PEPPOL Order scenarios.
     * Should be able to decode Order and OrderResponse documents.
     *
     * @author thore
     */
    public class OrderDocumentParser : AbstractDocumentParser
    {

        public OrderDocumentParser(PlainUBLParser parser)
            : base(parser)
        {

        }


        public override ParticipantIdentifier getSender()
        {
            String xpath = "//cac:BuyerCustomerParty/cac:Party/cbc:EndpointID";
            if (this.parser.localName().StartsWith("OrderResponse"))
            {
                // Matches both OrderResponse and OrderResponseSimple
                xpath = "//cac:SellerSupplierParty/cac:Party/cbc:EndpointID";
            }

            return this.participantId(xpath);
        }


        public override ParticipantIdentifier getReceiver()
        {
            String xpath = "//cac:SellerSupplierParty/cac:Party/cbc:EndpointID";
            if (this.parser.localName().StartsWith("OrderResponse"))
            {
                // Matches both OrderResponse and OrderResponseSimple
                xpath = "//cac:BuyerCustomerParty/cac:Party/cbc:EndpointID";
            }

            return this.participantId(xpath);
        }
    }
}
