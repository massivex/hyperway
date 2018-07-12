namespace Mx.Certificates.Validator.Xml
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Serialization;

    public class CertificateBucketType
    {
        public CertificateBucketType()
        {
            this.Certificates = new List<CertificateType>();
        }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlElement("Certificate")]
        public IList<CertificateType> Certificates { get; set; }

        [XmlElement("CertificateReference")]
        public IList<CertificateReferenceType> CertificateReferences { get; set; }

        [XmlElement("CertificateStartsWith")]
        public IList<CertificateStartsWith> CertificateStartsWiths { get; set; }

        public List<object> getCertificateOrCertificateReferenceOrCertificateStartsWith()
        {
            return this.Certificates
                .OfType<object>()
                .Union(this.CertificateReferences)
                .Union(this.CertificateStartsWiths)
                .ToList();
        }
    }
}