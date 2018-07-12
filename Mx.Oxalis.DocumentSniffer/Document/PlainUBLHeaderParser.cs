using System;

namespace Mx.Oxalis.DocumentSniffer.Document
{
    using System.Xml;
    using System.Xml.Linq;

    using Mx.Oxalis.DocumentSniffer.Identifier;
    using Mx.Oxalis.DocumentSniffer.Parsers;
    using Mx.Peppol.Common.Model;
    using Mx.Tools;

    /**
     * Parses the common PEPPOL header information, enough to decide document type and profile
     *
     * @author steinar
     * @author thore
     * @author arun
     */
    public class PlainUBLHeaderParser : PlainUBLParser
    {

        public static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(PlainUBLHeaderParser));

    public PlainUBLHeaderParser(XDocument document, IXmlNamespaceResolver nsResolver): base(document, nsResolver)
    {
        
    }

    public CustomizationIdentifier fetchCustomizationId()
    {
        String value = retriveValueForXpath("//cbc:CustomizationID");
        return CustomizationIdentifier.valueOf(value);
    }

    public ProcessIdentifier fetchProcessTypeId()
    {
        String value = retriveValueForXpath("//cbc:ProfileID");
        return ProcessIdentifier.of(value);
    }

    public PeppolDocumentTypeId fetchDocumentTypeId()
    {
        CustomizationIdentifier customizationIdentifier = fetchCustomizationId();
        return new PeppolDocumentTypeId(rootNameSpace(), localName(), customizationIdentifier, ublVersion());
    }

    public PEPPOLDocumentParser createDocumentParser()
    {
        String type = localName();

        log.Debug("Creating DocumentParser for type : " + localName());
        
        // despatch advice scenario
        if ("DespatchAdvice".EqualsIgnoreCase(type))
        {
            return new DespatchAdviceDocumentParser(this);
        }
        
        // catalogue scenario
        if ("Catalogue".EqualsIgnoreCase(type))
        {
            return new CatalogueDocumentParser(this);
        }

        // invoice scenario
        if ("CreditNote".EqualsIgnoreCase(type))
        {
            return new InvoiceDocumentParser(this);
        }

        if ("Invoice".EqualsIgnoreCase(type))
        {
            return new InvoiceDocumentParser(this);
        }

        if ("Reminder".EqualsIgnoreCase(type))
        {
            return new InvoiceDocumentParser(this);
        }
        
        // order scenario
        if ("Order".EqualsIgnoreCase(type))
        {
            return new OrderDocumentParser(this);
        }

        if ("OrderResponse".EqualsIgnoreCase(type))
        {
            return new OrderDocumentParser(this);
        }

        if ("OrderResponseSimple".EqualsIgnoreCase(type))
        {
            return new OrderDocumentParser(this);
        }
        
        // application response used by CatalogueResponse, MessageLevelResponse
        if ("ApplicationResponse".EqualsIgnoreCase(type))
        {
            return new ApplicationResponseDocumentParser(this);
        }

        // unknown scenario - for now we do not have a backup plan
        throw new InvalidOperationException("Cannot decide which PEPPOLDocumentParser to use for type " + type);
    }

}

}
