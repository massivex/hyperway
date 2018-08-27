namespace Mx.Hyperway.DocumentSniffer.Document.Parsers
{
    using Mx.Peppol.Common.Model;

    /// <summary>
    /// Parser to retrieves information from PEPPOL Despatch Advice scenarios.
    /// Should be able to decode Despatch Advice document
    /// </summary>
    public class DespatchAdviceDocumentParser : AbstractDocumentParser
    {

    public DespatchAdviceDocumentParser(PlainUblParser parser): base(parser)
    {
        
    }


        public override ParticipantIdentifier Sender
        {
            get
            {
                string despatchAdvice = "//cac:DespatchSupplierParty/cac:Party/cbc:EndpointID";
                return this.ParticipantId(despatchAdvice);
            }
        }

        public override ParticipantIdentifier Receiver
        {
            get
            {
                string despatchAdvice = "//cac:DeliveryCustomerParty/cac:Party/cbc:EndpointID";
                return this.ParticipantId(despatchAdvice);
            }
        }
    }

}
