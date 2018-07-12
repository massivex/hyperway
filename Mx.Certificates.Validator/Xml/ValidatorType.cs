namespace Mx.Certificates.Validator.Xml
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    public class ValidatorType
    {
        public ValidatorType()
        {
            this.Rules = new List<ValidationRule>();
        }

        public string Name { get; set; }

        public int? Timeout { get; set; }

        [XmlArrayItem("Expiration", typeof(ExpirationRule))]
        [XmlArrayItem("Signing", typeof(SigningRule))]
        [XmlArrayItem("ChainRule", typeof(ChainRule))]
        public IList<ValidationRule> Rules { get; set; }

        public string ValidatorReference { get; set; }

        public bool ShouldSerializeName()
        {
            return !string.IsNullOrWhiteSpace(this.Name);
        }

        public bool ShouldSerializeTimeout()
        {
            return this.Timeout != null;
        }

        public bool ShouldSerializeValidatorReference()
        {
            return !string.IsNullOrWhiteSpace(this.ValidatorReference);
        }
    }
}