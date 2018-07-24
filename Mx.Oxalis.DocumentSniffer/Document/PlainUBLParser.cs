namespace Mx.Hyperway.DocumentSniffer.Document
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml;
    using System.Xml.Linq;
    using System.Xml.XPath;

    /**
     * Simple parser that is UBL aware and handles xpath with namespaces.
     *
     * @author thore
     */
    public class PlainUBLParser
    {

        private readonly XDocument document;

        private readonly IXmlNamespaceResolver nsResolver;

        public PlainUBLParser(XDocument document, IXmlNamespaceResolver nsResolver)
        {
            this.document = document;
            this.nsResolver = nsResolver;
        }

        public String localName()
        {
            return this.document.Document.Root.Name.LocalName;
        }

        public String rootNameSpace()
        {
            return this.document.Document.Root.Name.NamespaceName;
        }

        public String ublVersion()
        {
            return this.retriveValueForXpath("//cbc:UBLVersionID");
        }

        public bool canParse()
        {
            return ("" + this.rootNameSpace()).StartsWith("urn:oasis:names:specification:ubl:schema:xsd:");
        }

        public XElement retrieveElementForXpath(String s)
        {
            try
            {
                // Element element = (Element)xPath.evaluate(s, document, XPathConstants.NODE);
                XElement element = this.document.XPathSelectElement(s, this.nsResolver);
                if (element == null)
                {
                    throw new InvalidOperationException("No element in XPath: " + s);
                }
                return element;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Unable to evaluate " + s + "; " + e.Message, e);
            }
        }

        public String retriveValueForXpath(String s)
        {
            try
            {
                // TODO: Verificare il tipo di path che viene restituito
                var value = ((IEnumerable<object>)this.document.XPathEvaluate(s, this.nsResolver)).OfType<XElement>().Single().Value;
                // XElement element = this.document.x
                // String value = xPath.evaluate(s, document);
                if (value == null)
                {
                    throw new InvalidOperationException("Unable to find value for Xpath expr " + s);
                }
                return value.Trim();
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Unable to evaluate " + s + "; " + e.Message, e);
            }
        }

    }

}
