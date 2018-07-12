using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Certificates.Validator.Xml
{
    using System.Xml.Serialization;

    public class ExpirationType
    {
        [XmlAttribute("millis")]
        public long? Millis { get; set; }
    }
}
