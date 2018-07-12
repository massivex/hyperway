namespace Mx.Certificates.Validator.Xml
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    public class CertificateBucketXml
    {
        public CertificateBucketXml()
        {
            this.Certificates = new List<CertificateXml>();
        }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlElement("Certificate")]
        public IList<CertificateXml> Certificates { get; set; }
    }
}