namespace Mx.Tools
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    public class XmlTools
    {
        public static UTF8Encoding Utf8NoBom = new UTF8Encoding(false);

        public static void WriteXmlFragment<TObject>(XmlTextWriter writer, TObject obj)
        {
            XmlSerializer s = new XmlSerializer(typeof(TObject));
            XmlWriterSettings settings = new XmlWriterSettings
                                             {
                                                 OmitXmlDeclaration = true,
                                                 Encoding = Utf8NoBom
            };
            XmlWriter w = XmlWriter.Create(writer, settings);
            s.Serialize(w, obj);
            w.Flush();
        }

        public static void AddXmlFragment(Stream s, XmlTextWriter writer)
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreProcessingInstructions = true;
            bool contentFound = false;
            using (XmlReader r = XmlReader.Create(s, settings))
            {
                while (r.Read())
                {
                    if (r.NodeType == XmlNodeType.Element)
                    {
                        string result = r.ReadOuterXml();
                        writer.WriteRaw(result);
                        writer.Flush();
                        contentFound = true;
                        break;
                    }
                }
            }

            if (!contentFound)
            {
                throw new InvalidOperationException("Unable to find an element inside input stream");
            }
        }

        public static TObject Read<TObject>(Stream s)
        {
            XmlSerializer ser = new XmlSerializer(typeof(TObject));
            var result = (TObject) ser.Deserialize(s);
            return result;
        }
    }
}
