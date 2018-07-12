using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Certificates.Validator.Xml
{
    using System.Xml.Serialization;

    public class CriticalExtensionRecognizedType
    {
        [XmlElement("Value")]
        public List<string> Values { get; set; }
    }
}
