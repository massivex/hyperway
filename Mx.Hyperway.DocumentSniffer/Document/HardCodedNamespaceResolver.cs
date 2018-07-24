namespace Mx.Hyperway.DocumentSniffer.Document
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml;

    /**
     * Namespace resolver hard coded for UBL based documents only.
     *
     * @author steinar
     * @author thore
     * @author erlend
     */
    public class HardCodedNamespaceResolver : IXmlNamespaceResolver
    {

        private static readonly Dictionary<String, String> NAMESPACE_MAP =
            new Dictionary<String, String>
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
            return NAMESPACE_MAP;
        }

        public string LookupNamespace(string prefix)
        {
            if (prefix == null)
            {
                throw new ArgumentException("No prefix provided!");
            }

            if (!NAMESPACE_MAP.ContainsKey(prefix))
            {
                return null;
            }

            String uri = NAMESPACE_MAP[prefix];
            return uri;
        }

        public string LookupPrefix(string namespaceName)
        {
            if (namespaceName == null)
            {
                throw new ArgumentException("No namespace provided!");
            }

            if (!NAMESPACE_MAP.ContainsValue(namespaceName))
            {
                return null;
            }

            String prefix = NAMESPACE_MAP.Where(x => x.Value == namespaceName).Select(x => x.Key).First();
            return prefix;
        }
    }
}