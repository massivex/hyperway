using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Certificates.Validator.Xml
{
    using System.Xml.Serialization;

    public class OCSPType
    {
        [XmlElement("IntermediateBucketReference")]
        public CertificateBucketReferenceType IntermediateBucketReference { get; set; }

    }
}
