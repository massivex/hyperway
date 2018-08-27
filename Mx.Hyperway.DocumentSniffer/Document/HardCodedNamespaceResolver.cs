namespace Mx.Hyperway.DocumentSniffer.Document
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml;

    /// <summary>
    /// Namespace resolver hard coded for UBL based documents only. 
    /// </summary>
    public class HardCodedNamespaceResolver : IXmlNamespaceResolver
    {

        private static readonly Dictionary<string, string> NamespaceMap =
            new Dictionary<string, string>
                {
                    { "xsi", "http://www.w3.org/2001/XMLSchema-instance" },
                    {
                        "cac",
                        "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2"
                    },
                    {
                        "cbc",
                        "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2"
                    },
                    {
                        "ext",
                        "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2"
                    }
                };

        public IDictionary<string, string> GetNamespacesInScope(XmlNamespaceScope scope)
        {
            return NamespaceMap;
        }

        public string LookupNamespace(string prefix)
        {
            if (prefix == null)
            {
                throw new ArgumentException("No prefix provided!");
            }

            if (!NamespaceMap.ContainsKey(prefix))
            {
                return null;
            }

            string uri = NamespaceMap[prefix];
            return uri;
        }

        public string LookupPrefix(string namespaceName)
        {
            if (namespaceName == null)
            {
                throw new ArgumentException("No namespace provided!");
            }

            if (!NamespaceMap.ContainsValue(namespaceName))
            {
                return null;
            }

            string prefix = NamespaceMap.Where(x => x.Value == namespaceName).Select(x => x.Key).First();
            return prefix;
        }
    }
}