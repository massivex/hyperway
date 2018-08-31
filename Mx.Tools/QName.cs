namespace Mx.Tools
{
    public class QName
    {
        public QName(string namespaceUri, string localPart)
            : this(namespaceUri, localPart, null)
        {
        }

        public QName(string namespaceUri, string localPart, string prefix)
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
