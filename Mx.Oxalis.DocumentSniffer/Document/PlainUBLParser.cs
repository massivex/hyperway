using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Oxalis.DocumentSniffer.Document
{
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
            return document.Document.Root.Name.LocalName;
        }

        public String rootNameSpace()
        {
            return document.Document.Root.Name.NamespaceName;
        }

        public String ublVersion()
        {
            return retriveValueForXpath("//cbc:UBLVersionID");
        }

        public bool canParse()
        {
            return ("" + rootNameSpace()).StartsWith("urn:oasis:names:specification:ubl:schema:xsd:");
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
                var value = ((IEnumerable<object>)this.document.XPathEvaluate(s, this.nsResolver)).OfType<XText>().Single().Value;
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
