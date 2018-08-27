namespace Mx.Hyperway.DocumentSniffer.Document.Parsers
{
    using System;

    using Mx.Peppol.Common.Model;

    /// <summary>
    /// Parser to retrieves information from PEPPOL Order scenarios.
    /// Should be able to decode Order and OrderResponse documents.
    /// </summary>
    public class OrderDocumentParser : AbstractDocumentParser
    {

        public OrderDocumentParser(PlainUblParser parser)
            : base(parser)
        {

        }


        public override ParticipantIdentifier Sender
        {
            get
            {
                String xpath = "//cac:BuyerCustomerParty/cac:Party/cbc:EndpointID";
                if (this.Parser.LocalName().StartsWith("OrderResponse"))
                {
                    // Matches both OrderResponse and OrderResponseSimple
                    xpath = "//cac:SellerSupplierParty/cac:Party/cbc:EndpointID";
                }

                return this.ParticipantId(xpath);
            }
        }

        public override ParticipantIdentifier Receiver
        {
            get
            {
                String xpath = "//cac:SellerSupplierParty/cac:Party/cbc:EndpointID";
                if (this.Parser.LocalName().StartsWith("OrderResponse"))
                {
                    // Matches both OrderResponse and OrderResponseSimple
                    xpath = "//cac:BuyerCustomerParty/cac:Party/cbc:EndpointID";
                }

                return this.ParticipantId(xpath);
            }
        }
    }
}
