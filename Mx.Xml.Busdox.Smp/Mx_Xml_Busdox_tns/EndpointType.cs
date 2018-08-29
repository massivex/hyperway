using System;
using System.Xml;

/**********************************************************************************************
 * Copyright (c) 2001-2018 Liquid Technologies Limited. All rights reserved.
 * See www.liquid-technologies.com for product details.
 *
 * Please see products End User License Agreement for distribution permissions.
 *
 * WARNING: THIS FILE IS GENERATED
 * Changes made outside of ##HAND_CODED_BLOCK_START blocks will be overwritten
 *
 * Generation  :  by Liquid XML Data Binder 16.1.11.8608
 * Using Schema: C:\src\massivex\hyperway\Mx.Xml.Busdox.Smp\ServiceMetadataPublishingTypes-1.0.xsd
 **********************************************************************************************/

namespace Mx.Xml.Busdox.tns
{
    /// <summary>
    /// This class represents the ComplexType EndpointType
    /// </summary>
    [LiquidTechnologies.Runtime.Standard20.XmlObjectInfo(LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementGroupType.Sequence,
                                                    LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.Element,
                                                    "EndpointType", "http://busdox.org/serviceMetadata/publishing/1.0/", true, false, false)]
    public partial class EndpointType : Mx.Xml.Busdox.Smp.XmlCommonBase
    {
        #region Constructors
        /// <summary>
        /// Constructor for EndpointType
        /// </summary>
        /// <remarks>
        /// The class is created with all the mandatory fields populated with the
        /// default data. 
        /// All Collection object are created.
        /// However any 1-n relationships (these are represented as collections) are
        /// empty. To comply with the schema these must be populated before the xml
        /// obtained from ToXml is valid against the schema C:\src\massivex\hyperway\Mx.Xml.Busdox.Smp\ServiceMetadataPublishingTypes-1.0.xsd
        /// </remarks>
        public EndpointType()
        {
            _elementName = "EndpointType";
            Init();
        }
        public EndpointType(string elementName)
        {
            _elementName = elementName;
            Init();
        }
        #endregion

        #region Initialization methods for the class
        /// <summary>
        /// Initializes the class
        /// </summary>
        /// <remarks>
        /// This creates all the mandatory fields (populated with the default data) 
        /// All Collection object are created.
        /// However any 1-n relationships (these are represented as collections) are
        /// empty. To comply with the schema these must be populated before the xml
        /// obtained from ToXml is valid against the schema C:\src\massivex\hyperway\Mx.Xml.Busdox.Smp\ServiceMetadataPublishingTypes-1.0.xsd.
        /// </remarks>
        protected override void Init()
        {
            Mx.Xml.Busdox.Smp.Registration.iRegistrationIndicator = 0; // causes registration to take place
            m_TransportProfile = null;
            m_EndpointReference = new Mx.Xml.Busdox.wsa.EndpointReference("EndpointReference");
            m_RequireBusinessLevelSignature = false;
            m_MinimumAuthenticationLevel = null;
            m_ServiceActivationDate = null;
            m_ServiceExpirationDate = null;
            m_Certificate = "";
            m_ServiceDescription = "";
            m_TechnicalContactUrl = "";
            m_TechnicalInformationUrl = null;
            m_Extension = null;

// ##HAND_CODED_BLOCK_START ID="Additional Inits"## DO NOT MODIFY ANYTHING OUTSIDE OF THESE TAGS

// Add Additional initialization code here...

// ##HAND_CODED_BLOCK_END ID="Additional Inits"## DO NOT MODIFY ANYTHING OUTSIDE OF THESE TAGS
        }
        #endregion

        #region ICloneable Interface
        /// <summary>
        /// Allows the class to be copied
        /// </summary>
        /// <remarks>
        /// Performs a 'deep copy' of all the data in the class (and its children)
        /// </remarks>
        public override object Clone()
        {
            Mx.Xml.Busdox.tns.EndpointType newObject = new Mx.Xml.Busdox.tns.EndpointType(_elementName);
            newObject.m_TransportProfile = m_TransportProfile;
            newObject.m_EndpointReference = null;
            if (m_EndpointReference != null)
                newObject.m_EndpointReference = (Mx.Xml.Busdox.wsa.EndpointReference)m_EndpointReference.Clone();
            newObject.m_RequireBusinessLevelSignature = m_RequireBusinessLevelSignature;
            newObject.m_MinimumAuthenticationLevel = m_MinimumAuthenticationLevel;
            if (m_ServiceActivationDate == null)
                newObject.m_ServiceActivationDate = null;
            else
                newObject.m_ServiceActivationDate = (LiquidTechnologies.Runtime.Standard20.XmlDateTime)m_ServiceActivationDate.Clone();
            if (m_ServiceExpirationDate == null)
                newObject.m_ServiceExpirationDate = null;
            else
                newObject.m_ServiceExpirationDate = (LiquidTechnologies.Runtime.Standard20.XmlDateTime)m_ServiceExpirationDate.Clone();
            newObject.m_Certificate = m_Certificate;
            newObject.m_ServiceDescription = m_ServiceDescription;
            newObject.m_TechnicalContactUrl = m_TechnicalContactUrl;
            newObject.m_TechnicalInformationUrl = m_TechnicalInformationUrl;
            newObject.m_Extension = null;
            if (m_Extension != null)
                newObject.m_Extension = (Mx.Xml.Busdox.tns.ExtensionType)m_Extension.Clone();

// ##HAND_CODED_BLOCK_START ID="Additional clone"## DO NOT MODIFY ANYTHING OUTSIDE OF THESE TAGS

// Add Additional clone code here...

// ##HAND_CODED_BLOCK_END ID="Additional clone"## DO NOT MODIFY ANYTHING OUTSIDE OF THESE TAGS

            return newObject;
        }
        #endregion

        #region Member variables

        protected override string TargetNamespace
        {
            get { return "http://busdox.org/serviceMetadata/publishing/1.0/"; }
        }

        #region Attribute - transportProfile
        /// <summary>
		/// Indicates the type of BUSDOX transport that is being used between access points, e.g. the BUSDOX START profile. This specification defines the following identifier URI which denotes the BUSDOX START transport: "busdox-transport-start"
        /// </summary>
        /// <remarks>
        /// This property is represented as an Attribute in the XML.
        /// It is optional, initially it is not valid.
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.AttributeInfoPrimitive("transportProfile", "", true, LiquidTechnologies.Runtime.Standard20.Conversions.ConversionType.type_string, null, LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.WhitespaceRule.Preserve, "", -1, -1, "", "", "", "", -1, -1, -1, null)]
        public string TransportProfile
        {
            get 
            { 
                return m_TransportProfile;  
            }
            set 
            { 
                if (value == null)
                {
                    m_TransportProfile = null;
                }
                else
                {
                    // Apply whitespace rules appropriately
                    value = LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.Preserve(value); 
                    m_TransportProfile = value;
                }
            }
        }
        protected string m_TransportProfile;
        #endregion

        #region Attribute - EndpointReference
        /// <summary>
		/// The address of an endpoint, as an WS-Addressing Endpoint Reference
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is mandatory and therefore must be populated within the XML.
        /// If this property is set, then the object will be COPIED. If the property is set to null an exception is raised.
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoSeqClsMnd("EndpointReference", "http://www.w3.org/2005/08/addressing", LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.Element, typeof(Mx.Xml.Busdox.wsa.EndpointReference), true)]
        public Mx.Xml.Busdox.wsa.EndpointReference EndpointReference
        {
            get 
            { 
                return m_EndpointReference;  
            }
            set 
            { 
                Throw_IfPropertyIsNull(value, "EndpointReference");
                m_EndpointReference = value;
            }
        }
        protected Mx.Xml.Busdox.wsa.EndpointReference m_EndpointReference;
        
        #endregion

        #region Attribute - RequireBusinessLevelSignature
        /// <summary>
		/// Set to "true" if the recipient requires business-level signatures for the message, meaning a signature applied to the business message before the message is put on the transport. This is independent of the transport-level signatures that a specific transport profile, such as the START profile, might mandate. This flag does not indicate which type of business-level signature might be required. Setting or consuming business-level signatures would typically be the responsibility of the final senders and receivers of messages, rather than a set of APs.
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is mandatory and therefore must be populated within the XML.
        /// It is defaulted to false.
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoSeqPrimMnd("RequireBusinessLevelSignature", "http://busdox.org/serviceMetadata/publishing/1.0/", null, LiquidTechnologies.Runtime.Standard20.Conversions.ConversionType.type_boolean, null, LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.WhitespaceRule.Collapse, "", -1, -1, "", "", "", "", -1, -1, -1, null)]
        public bool RequireBusinessLevelSignature
        {
            get
            {
                return m_RequireBusinessLevelSignature;
            }
            set 
            {
                m_RequireBusinessLevelSignature = value;
            }
        }
        protected bool m_RequireBusinessLevelSignature;

        #endregion

        #region Attribute - MinimumAuthenticationLevel
        /// <summary>
		/// Indicates the minimum authentication level that recipient requires. The specific semantics of this field is defined in a specific instance of the BUSDOX infrastructure. It could for example reflect the value of the "urn:eu:busdox:attribute:assurance-level" SAML attribute defined in the START specification.
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is optional, initially it is not valid.
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoSeqPrimOpt("MinimumAuthenticationLevel", "http://busdox.org/serviceMetadata/publishing/1.0/", true, null, LiquidTechnologies.Runtime.Standard20.Conversions.ConversionType.type_string, null, LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.WhitespaceRule.Preserve, "", -1, -1, "", "", "", "", -1, -1, -1, null)]
        public string MinimumAuthenticationLevel
        {
            get 
            { 
                return m_MinimumAuthenticationLevel;  
            }
            set 
            { 
                if (value == null)
                {
                    m_MinimumAuthenticationLevel = null;
                }
                else
                {
                    // Apply whitespace rules appropriately
                    value = LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.Preserve(value); 
                    m_MinimumAuthenticationLevel = value;
                }
            }
        }
        protected string m_MinimumAuthenticationLevel;
        #endregion

        #region Attribute - ServiceActivationDate
        /// <summary>
		/// Activation date of the service. Senders should ignore services that are not yet activated.
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is optional, initially it is not valid.
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoSeqPrimOpt("ServiceActivationDate", "http://busdox.org/serviceMetadata/publishing/1.0/", true, null, LiquidTechnologies.Runtime.Standard20.Conversions.ConversionType.type_datetime, null, LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.WhitespaceRule.Collapse, "", -1, -1, "", "", "", "", -1, -1, -1, null)]
        public LiquidTechnologies.Runtime.Standard20.XmlDateTime ServiceActivationDate
        {
            get 
            { 
                return m_ServiceActivationDate;  
            }
            set 
            { 
                if (value == null)
                {
                    m_ServiceActivationDate = null;
                }
                else
                {
                    if (m_ServiceActivationDate == null)
                        m_ServiceActivationDate = new LiquidTechnologies.Runtime.Standard20.XmlDateTime(LiquidTechnologies.Runtime.Standard20.XmlDateTime.DateType.dateTime);
                    m_ServiceActivationDate.SetDateTime(value, m_ServiceActivationDate.Type);
                }
            }
        }
        protected LiquidTechnologies.Runtime.Standard20.XmlDateTime m_ServiceActivationDate;
        #endregion

        #region Attribute - ServiceExpirationDate
        /// <summary>
		/// Expiration date of the service. Senders should ignore services that are expired.
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is optional, initially it is not valid.
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoSeqPrimOpt("ServiceExpirationDate", "http://busdox.org/serviceMetadata/publishing/1.0/", true, null, LiquidTechnologies.Runtime.Standard20.Conversions.ConversionType.type_datetime, null, LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.WhitespaceRule.Collapse, "", -1, -1, "", "", "", "", -1, -1, -1, null)]
        public LiquidTechnologies.Runtime.Standard20.XmlDateTime ServiceExpirationDate
        {
            get 
            { 
                return m_ServiceExpirationDate;  
            }
            set 
            { 
                if (value == null)
                {
                    m_ServiceExpirationDate = null;
                }
                else
                {
                    if (m_ServiceExpirationDate == null)
                        m_ServiceExpirationDate = new LiquidTechnologies.Runtime.Standard20.XmlDateTime(LiquidTechnologies.Runtime.Standard20.XmlDateTime.DateType.dateTime);
                    m_ServiceExpirationDate.SetDateTime(value, m_ServiceExpirationDate.Type);
                }
            }
        }
        protected LiquidTechnologies.Runtime.Standard20.XmlDateTime m_ServiceExpirationDate;
        #endregion

        #region Attribute - Certificate
        /// <summary>
		/// Holds the complete signing certificate of the recipient AP, as a PEM base 64 encoded X509 DER formatted value.
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is mandatory and therefore must be populated within the XML.
        /// It is defaulted to "".
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoSeqPrimMnd("Certificate", "http://busdox.org/serviceMetadata/publishing/1.0/", null, LiquidTechnologies.Runtime.Standard20.Conversions.ConversionType.type_string, null, LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.WhitespaceRule.Preserve, "", -1, -1, "", "", "", "", -1, -1, -1, null)]
        public string Certificate
        {
            get
            {
                return m_Certificate;
            }
            set 
            {
                // Apply whitespace rules appropriately
                value = LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.Preserve(value);
                m_Certificate = value;
            }
        }
        protected string m_Certificate;

        #endregion

        #region Attribute - ServiceDescription
        /// <summary>
		/// A human readable description of the service
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is mandatory and therefore must be populated within the XML.
        /// It is defaulted to "".
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoSeqPrimMnd("ServiceDescription", "http://busdox.org/serviceMetadata/publishing/1.0/", null, LiquidTechnologies.Runtime.Standard20.Conversions.ConversionType.type_string, null, LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.WhitespaceRule.Preserve, "", -1, -1, "", "", "", "", -1, -1, -1, null)]
        public string ServiceDescription
        {
            get
            {
                return m_ServiceDescription;
            }
            set 
            {
                // Apply whitespace rules appropriately
                value = LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.Preserve(value);
                m_ServiceDescription = value;
            }
        }
        protected string m_ServiceDescription;

        #endregion

        #region Attribute - TechnicalContactUrl
        /// <summary>
		/// Represents a link to human readable contact information. This might also be an email address.
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is mandatory and therefore must be populated within the XML.
        /// It is defaulted to "".
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoSeqPrimMnd("TechnicalContactUrl", "http://busdox.org/serviceMetadata/publishing/1.0/", null, LiquidTechnologies.Runtime.Standard20.Conversions.ConversionType.type_string, null, LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.WhitespaceRule.Collapse, "", -1, -1, "", "", "", "", -1, -1, -1, null)]
        public string TechnicalContactUrl
        {
            get
            {
                return m_TechnicalContactUrl;
            }
            set 
            {
                // Apply whitespace rules appropriately
                value = LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.Collapse(value);
                m_TechnicalContactUrl = value;
            }
        }
        protected string m_TechnicalContactUrl;

        #endregion

        #region Attribute - TechnicalInformationUrl
        /// <summary>
		/// A URL to human readable documentation of the service format. This could for example be a web site containing links to XML Schemas, WSDLs, Schematrons and other relevant resources.
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is optional, initially it is not valid.
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoSeqPrimOpt("TechnicalInformationUrl", "http://busdox.org/serviceMetadata/publishing/1.0/", true, null, LiquidTechnologies.Runtime.Standard20.Conversions.ConversionType.type_string, null, LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.WhitespaceRule.Collapse, "", -1, -1, "", "", "", "", -1, -1, -1, null)]
        public string TechnicalInformationUrl
        {
            get 
            { 
                return m_TechnicalInformationUrl;  
            }
            set 
            { 
                if (value == null)
                {
                    m_TechnicalInformationUrl = null;
                }
                else
                {
                    // Apply whitespace rules appropriately
                    value = LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.Collapse(value); 
                    m_TechnicalInformationUrl = value;
                }
            }
        }
        protected string m_TechnicalInformationUrl;
        #endregion

        #region Attribute - Extension
        /// <summary>
		/// The extension element may contain any XML element. Clients MAY ignore this element.
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is optional, initially it is null.
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoSeqClsOpt("Extension", "http://busdox.org/serviceMetadata/publishing/1.0/", LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.Element, typeof(Mx.Xml.Busdox.tns.ExtensionType))]
        public Mx.Xml.Busdox.tns.ExtensionType Extension
        {
            get
            { 
                return m_Extension;
            }
            set
            { 
                if (value == null)
                    m_Extension = null;
                else
                {
                    SetElementName(value, "Extension");
                    m_Extension = value; 
                }
            }
        }
        protected Mx.Xml.Busdox.tns.ExtensionType m_Extension;
        
        #endregion

        #region Attribute - Namespace
        public override string Namespace
        {
            get { return "http://busdox.org/serviceMetadata/publishing/1.0/"; }
        }    
        #endregion    

        #region Attribute - GetBase
        public override LiquidTechnologies.Runtime.Standard20.XmlObjectBase GetBase()
        {
            return this;
        }
        #endregion
        #endregion


// ##HAND_CODED_BLOCK_START ID="Additional Methods"## DO NOT MODIFY ANYTHING OUTSIDE OF THESE TAGS

// Add Additional Methods and members here...

// ##HAND_CODED_BLOCK_END ID="Additional Methods"## DO NOT MODIFY ANYTHING OUTSIDE OF THESE TAGS
    }
}


