namespace Mx.Tools
{
    using System;

    public class QName
    {
        public QName(String namespaceUri, String localPart)
            : this(namespaceUri, localPart, null)
        {
        }

        public QName(String namespaceUri, String localPart, String prefix)
        {
            this.NamespaceUri = namespaceUri;
            this.LocalPart = localPart;
            this.Prefix = prefix;
        }

        public string NamespaceUri { get; }
        public string LocalPart { get; }
        public string Prefix { get; }
    }
}
