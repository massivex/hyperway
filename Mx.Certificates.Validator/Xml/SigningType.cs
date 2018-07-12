namespace Mx.Certificates.Validator.Xml
{
    using System.Xml.Serialization;

    public class SigningType : ValidationRule
    {
        [XmlAttribute("type")]
        public string Type { get; set; }
    }

    public enum SigningEnum
    {
        [XmlEnum("PUBLIC_SIGNED")]
        PublicSigned,

        [XmlEnum("SELF_SIGNED")]
        SelfSigned

    }
}