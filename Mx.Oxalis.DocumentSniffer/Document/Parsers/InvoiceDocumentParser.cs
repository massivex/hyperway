namespace Mx.Hyperway.DocumentSniffer.Document.Parsers
{
    using System;

    using Mx.Peppol.Common.Model;

    /**
     * Parser to retrieves information from PEPPOL Invoice scenarios.
     * Should be able to decode Invoices in plain UBL and Norwegian EHF variants.
     *
     * @author thore
     */
    public class InvoiceDocumentParser : AbstractDocumentParser
    {

        public InvoiceDocumentParser(PlainUBLParser parser)
            : base(parser)
        {

        }


        public override ParticipantIdentifier getSender()
        {
            String endpointFirst = "//cac:AccountingSupplierParty/cac:Party/cbc:EndpointID";
            String companySecond = "//cac:AccountingSupplierParty/cac:Party/cac:PartyLegalEntity/cbc:CompanyID";
            ParticipantIdentifier s;
            try
            {
                s = this.participantId(endpointFirst);
            }
            catch (Exception)
            {
                s = this.participantId(companySecond);
            }

            return s;
        }


        public override ParticipantIdentifier getReceiver()
        {
            String endpointFirst = "//cac:AccountingCustomerParty/cac:Party/cbc:EndpointID";
            String companySecond = "//cac:AccountingCustomerParty/cac:Party/cac:PartyLegalEntity/cbc:CompanyID";
            ParticipantIdentifier s;
            try
            {
                s = this.participantId(endpointFirst);
            }
            catch (Exception)
            {
                s = this.participantId(companySecond);
            }

            return s;
        }

    }

}
