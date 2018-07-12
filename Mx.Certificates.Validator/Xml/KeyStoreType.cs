using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Certificates.Validator.Xml
{
    using System.Xml.Serialization;

    public class KeyStoreType
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("password")]
        public string Password { get; set; }

        [XmlText]
        public string Value { get; set; }
    }
}
