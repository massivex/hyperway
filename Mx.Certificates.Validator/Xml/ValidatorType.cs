﻿namespace Mx.Certificates.Validator.Xml
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

        [XmlArrayItem("Cached", typeof(CachedType))]
        [XmlArrayItem("Chain", typeof(ChainType))]
        [XmlArrayItem("Class", typeof(ClassType))]
        [XmlArrayItem("CriticalExtensionRecognized", typeof(CriticalExtensionRecognizedType))]
        [XmlArrayItem("CriticalExtensionRequired", typeof(CriticalExtensionRequiredType))]
        [XmlArrayItem("CRL", typeof(CRLType))]
        [XmlArrayItem("Dummy", typeof(DummyType))]
        [XmlArrayItem("Expiration", typeof(ExpirationType))]
        [XmlArrayItem("Junction", typeof(JunctionType))]
        [XmlArrayItem("KeyUsage", typeof(KeyUsageType))]
        [XmlArrayItem("OCSP", typeof(OCSPType))]
        [XmlArrayItem("HandleError", typeof(HandleErrorType))]
        [XmlArrayItem("PrincipleName", typeof(PrincipleNameType))]
        [XmlArrayItem("RuleReference", typeof(RuleReferenceType))]
        [XmlArrayItem("Signing", typeof(SigningType))]
        [XmlArrayItem("Try", typeof(TryType))]
        [XmlArrayItem("ValidatorReference", typeof(ValidatorReferenceType))]
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