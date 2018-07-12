using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Certificates.Validator.Xml
{
    using System.Xml.Serialization;

    public class CertificateBucketReferenceType
    {
        [XmlText]
        public string Value { get; set; }
    }
}
