namespace Mx.Tools
{
    using System.Xml;

    public static class XmlTextReaderExtensions
    {
        public static bool IsElement(this XmlTextReader reader, QName qName)
        {
            if (reader.NodeType != XmlNodeType.Element)
            {
                return false;
            }

            if (!string.IsNullOrEmpty(qName.Prefix) && qName.Prefix != reader.Prefix)
            {
                return false;
            }

            if (reader.NamespaceURI == qName.NamespaceUri && reader.Name == qName.LocalPart)
            {
                return true;
            }

            return false;
        }

        public static bool ReadToNextElement(this XmlTextReader reader, int maxDeep = 4)
        {
            var found = false;
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    found = true;
                    break;
                }

                maxDeep--;

                if (maxDeep <= 0)
                {
                    break;
                }
            }

            return found;
        }
    }
}
