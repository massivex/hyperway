namespace Mx.Certificates.Validator.Xml
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    public class PrincipleNameRule : ValidationRule
    {
        [XmlAttribute("field")]
        public string Field { get; set; }

        [XmlAttribute("principal")]
        public string Principal { get; set; }

        [XmlElement("Value")]
        public List<string> Values { get; set; }
    }
}