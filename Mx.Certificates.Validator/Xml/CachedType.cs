namespace Mx.Certificates.Validator.Xml
{
    using System.Xml.Serialization;

    public class CachedType : ValidationRule
    {
        [XmlAttribute("name")]
        public int Name { get; set; }

        [XmlAttribute("timeout")]
        public int Timeout { get; set; }
    }
}