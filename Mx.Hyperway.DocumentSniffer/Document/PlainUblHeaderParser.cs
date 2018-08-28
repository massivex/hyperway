namespace Mx.Hyperway.DocumentSniffer.Document
{
    using System;
    using System.Xml;
    using System.Xml.Linq;

    using Mx.Hyperway.DocumentSniffer.Document.Parsers;
    using Mx.Hyperway.DocumentSniffer.Identifier;
    using Mx.Peppol.Common.Model;
    using Mx.Tools;

    /// <summary>
    /// Parses the common PEPPOL header information, enough to decide document type and profile 
    /// </summary>
    public class PlainUblHeaderParser : PlainUblParser
    {

        public static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(PlainUblHeaderParser));

    public PlainUblHeaderParser(XDocument document, IXmlNamespaceResolver nsResolver): base(document, nsResolver)
    {
        
    }

    public CustomizationIdentifier FetchCustomizationId()
    {
        String value = this.RetriveValueForXpath("//cbc:CustomizationID");
        return CustomizationIdentifier.ValueOf(value);
    }

    public ProcessIdentifier FetchProcessTypeId()
    {
        String value = this.RetriveValueForXpath("//cbc:ProfileID");
        return ProcessIdentifier.Of(value);
    }

    public PeppolDocumentTypeId FetchDocumentTypeId()
    {
        CustomizationIdentifier customizationIdentifier = this.FetchCustomizationId();
        return new PeppolDocumentTypeId(this.RootNameSpace(), this.LocalName(), customizationIdentifier, this.UblVersion());
    }

    public IPeppolDocumentParser CreateDocumentParser()
    {
        String type = this.LocalName();

        Log.Debug("Creating DocumentParser for type : " + this.LocalName());
        
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
