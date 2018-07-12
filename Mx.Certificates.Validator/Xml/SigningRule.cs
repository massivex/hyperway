namespace Mx.Certificates.Validator.Xml
{
    using System.Xml.Serialization;

    public class SigningRule : ValidationRule
    {
        [XmlAttribute("type")]
        public string Type { get; set; }
    }
}