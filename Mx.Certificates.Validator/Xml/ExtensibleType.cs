using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Certificates.Validator.Xml
{
    using System.Xml.Serialization;

    public class ExtensibleType
    {
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
        public List<object> Rules { get; set; }
    }
}
