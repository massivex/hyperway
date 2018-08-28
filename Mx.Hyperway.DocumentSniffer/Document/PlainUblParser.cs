namespace Mx.Hyperway.DocumentSniffer.Document
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml;
    using System.Xml.Linq;
    using System.Xml.XPath;

    /// <summary>
    /// Simple parser that is UBL aware and handles xpath with namespaces. 
    /// </summary>
    public class PlainUblParser
    {

        private readonly XDocument document;

        private readonly IXmlNamespaceResolver nsResolver;

        public PlainUblParser(XDocument document, IXmlNamespaceResolver nsResolver)
        {
            this.document = document;
            this.nsResolver = nsResolver;
        }

        public String LocalName()
        {
            return this.document.Document?.Root?.Name.LocalName;
        }

        public String RootNameSpace()
        {
            return this.document.Document?.Root?.Name.NamespaceName;
        }

        public String UblVersion()
        {
            return this.RetriveValueForXpath("//cbc:UBLVersionID");
        }

        public bool CanParse()
        {
            return ("" + this.RootNameSpace()).StartsWith("urn:oasis:names:specification:ubl:schema:xsd:");
        }

        public XElement RetrieveElementForXpath(String s)
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

        public String RetriveValueForXpath(String s)
        {
            try
            {
                // TODO: Verificare il tipo di path che viene restituito
                var value = ((IEnumerable<object>)this.document.XPathEvaluate(s, this.nsResolver)).OfType<XElement>().Single().Value;
                return value.Trim();
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Unable to evaluate " + s + "; " + e.Message, e);
            }
        }
    }
}
