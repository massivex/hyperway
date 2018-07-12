using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Oxalis.DocumentSniffer.Parsers
{
    using Mx.Oxalis.DocumentSniffer.Document;
    using Mx.Peppol.Common.Model;

    /**
     * Parser to retrieves information from PEPPOL Despatch Advice scenarios.
     * Should be able to decode Despatch Advice document
     *
     * @author thore
     */
    public class DespatchAdviceDocumentParser : AbstractDocumentParser
    {

    public DespatchAdviceDocumentParser(PlainUBLParser parser): base(parser)
    {
        
    }

    
    public override ParticipantIdentifier getSender()
    {
        String despatchAdvice = "//cac:DespatchSupplierParty/cac:Party/cbc:EndpointID";
        return participantId(despatchAdvice);
    }

    
    public override ParticipantIdentifier getReceiver()
    {
        String despatchAdvice = "//cac:DeliveryCustomerParty/cac:Party/cbc:EndpointID";
        return participantId(despatchAdvice);
    }
    }

}
