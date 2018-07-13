﻿namespace Mx.Tools
{
    using System.IO;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    public class XmlTools
    {
        public static void WriteXmlFragment<TObject>(XmlTextWriter writer, TObject obj)
        {
            XmlSerializer s = new XmlSerializer(typeof(TObject));
            XmlWriterSettings settings = new XmlWriterSettings
                                             {
                                                 OmitXmlDeclaration = true,
                                                 Encoding = System.Text.Encoding.UTF8
                                             };
            XmlWriter w = XmlWriter.Create(writer, settings);
            s.Serialize(w, obj);
        }

        public static void AddXmlFragment(Stream s, XmlTextWriter writer)
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreProcessingInstructions = true;
            using (XmlReader r = XmlReader.Create(s, settings))
            {
                string result = r.ReadOuterXml();
                writer.WriteRaw(result);
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
