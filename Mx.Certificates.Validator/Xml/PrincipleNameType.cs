namespace Mx.Certificates.Validator.Xml
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    public class PrincipleNameType : ValidationRule
    {
        [XmlAttribute("field")]
        public string Field { get; set; }

        [XmlAttribute("principal")]
        public PrincipalEnum Principal { get; set; }

        [XmlElement("Value")]
        public List<string> Values { get; set; }

        [XmlElement("Reference")]
        public string Reference { get; set; }
    }

    public enum PrincipalEnum
    {
        [XmlEnum("SUBJECT")]
        Subject,

        [XmlEnum("ISSUER")]
        Issuer
    }
}