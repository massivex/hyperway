namespace Mx.Certificates.Validator.Xml
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    public class ChainType
    {
        [XmlElement("RootBucketReference")]
        public string RootBucketReference { get; set; }

        [XmlElement("IntermediateBucketReference")]
        public string IntermediateBucketReference { get; set; }

        [XmlElement("Policy")]
        public List<string> Policies { get; set; }
    }
}