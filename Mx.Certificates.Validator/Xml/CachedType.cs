namespace Mx.Certificates.Validator.Xml
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    public class CachedType : ExtensibleType
    {
        [XmlAttribute("name")]
        public int Name { get; set; }

        [XmlAttribute("timeout")]
        public int Timeout { get; set; }
    }
}