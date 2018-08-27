namespace Mx.Hyperway.DocumentSniffer.Document.Parsers
{
    using System;

    using Mx.Peppol.Common.Model;

    /// <summary>
    /// Parser to retrieves information from PEPPOL Invoice scenarios.
    /// Should be able to decode Invoices in plain UBL and Norwegian EHF variants.
    /// </summary>
    public class InvoiceDocumentParser : AbstractDocumentParser
    {

        public InvoiceDocumentParser(PlainUblParser parser)
            : base(parser)
        {

        }


        public override ParticipantIdentifier Sender
        {
            get
            {
                string endpointFirst = "//cac:AccountingSupplierParty/cac:Party/cbc:EndpointID";
                string companySecond = "//cac:AccountingSupplierParty/cac:Party/cac:PartyLegalEntity/cbc:CompanyID";
                ParticipantIdentifier s;
                try
                {
                    s = this.ParticipantId(endpointFirst);
                }
                catch (Exception)
                {
                    s = this.ParticipantId(companySecond);
                }

                return s;
            }
        }

        public override ParticipantIdentifier Receiver
        {
            get
            {
                string endpointFirst = "//cac:AccountingCustomerParty/cac:Party/cbc:EndpointID";
                string companySecond = "//cac:AccountingCustomerParty/cac:Party/cac:PartyLegalEntity/cbc:CompanyID";
                ParticipantIdentifier s;
                try
                {
                    s = this.ParticipantId(endpointFirst);
                }
                catch (Exception)
                {
                    s = this.ParticipantId(companySecond);
                }

                return s;
            }
        }
    }

}
