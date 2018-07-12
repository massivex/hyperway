using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Certificates.Validator.Xml
{
    using System.Xml.Serialization;

    public enum JunctionEnum
    {
        [XmlEnum("AND")]
        AND,
        [XmlEnum("OR")]
        OR,
        [XmlEnum("XOR")]
        XOR
    }
}
