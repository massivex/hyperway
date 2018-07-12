using System.Collections.Generic;
using System.Text;

namespace Mx.Certificates.Validator.Xml
{
    using System.Xml.Serialization;

    [XmlRoot("ValidatorRecipe")]
    public class ValidatorRecipe
    {
        public ValidatorRecipe()
        {
            this.Validators = new List<ValidatorType>();
            this.CertificateBuckets = new List<CertificateBucketType>();
            this.KeyStores = new List<KeyStoreType>();
        }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("version")]
        public string Version { get; set; }

        [XmlElement("Validator")]
        public IList<ValidatorType> Validators { get; set; }

        [XmlElement("CertificateBucket")]
        public IList<CertificateBucketType> CertificateBuckets { get; set; }

        [XmlElement("KeyStore")]
        public IList<KeyStoreType> KeyStores { get; set; }
    }
}
