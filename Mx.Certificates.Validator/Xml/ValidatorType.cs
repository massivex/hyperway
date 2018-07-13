namespace Mx.Certificates.Validator.Xml
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    public class ValidatorType : ExtensibleType
    {
        public ValidatorType()
        {
            this.Rules = new List<object>();
        }

        public string Name { get; set; }

        public long? Timeout { get; set; }

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