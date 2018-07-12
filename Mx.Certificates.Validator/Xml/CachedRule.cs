namespace Mx.Certificates.Validator.Xml
{
    using System.Xml.Serialization;

    public class CachedRule : ValidationRule
    {
        [XmlAttribute("timeout")]
        public int Timeout { get; set; }

        // TODO: Complete nodes management
    }
}