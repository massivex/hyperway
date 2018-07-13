namespace Mx.Certificates.Validator.Xml
{
    using System.Xml.Serialization;

    public class SigningType
    {
        [XmlAttribute("type")]
        public string Type { get; set; }
    }

    public enum SigningEnum
    {
        [XmlEnum("PUBLIC_SIGNED")]
        PUBLIC_SIGNED,

        [XmlEnum("SELF_SIGNED")]
        SELF_SIGNED

    }
}