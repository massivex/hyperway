using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Certificates.Validator.Xml
{
    using System.Xml.Serialization;

    using Mx.Certificates.Validator.Util;

    public class KeyUsageType
    {
        [XmlElement("Identifier")]
        public List<KeyUsage> Identifier { get; set; }
    }
}
