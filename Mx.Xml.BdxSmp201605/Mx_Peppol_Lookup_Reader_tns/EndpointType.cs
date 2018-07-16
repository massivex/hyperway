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
 * Generation  :  by Liquid XML Data Binder 16.1.7.8497
 * Using Schema: C:\src\massivex\hyperway\Mx.Peppol.Lookup\Reader\bdx-smp-201605.xsd
 **********************************************************************************************/

namespace Mx.Peppol.Lookup.Reader.tns
{
    /// <summary>
    /// This class represents the ComplexType EndpointType
    /// </summary>
    [LiquidTechnologies.Runtime.Standard20.XmlObjectInfo(LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementGroupType.Sequence,
                                                    LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.Element,
                                                    "EndpointType", "http://docs.oasis-open.org/bdxr/ns/SMP/2016/05", true, false, false)]
    public partial class EndpointType : Mx.Peppol.Lookup.Reader.BdxSmp201605.XmlCommonBase
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
        /// obtained from ToXml is valid against the schema C:\src\massivex\hyperway\Mx.Peppol.Lookup\Reader\bdx-smp-201605.xsd
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
        /// obtained from ToXml is valid against the schema C:\src\massivex\hyperway\Mx.Peppol.Lookup\Reader\bdx-smp-201605.xsd.
        /// </remarks>
        protected override void Init()
        {
            Mx.Peppol.Lookup.Reader.BdxSmp201605.Registration.iRegistrationIndicator = 0; // causes registration to take place
            m_TransportProfile = "";
            m_EndpointURI = "";
            m_RequireBusinessLevelSignature = LiquidTechnologies.Runtime.Standard20.Conversions.booleanFromString("false", LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.WhitespaceRule.Collapse);
            m_MinimumAuthenticationLevel = null;
            m_ServiceActivationDate = null;
            m_ServiceExpirationDate = null;
            m_Certificate = LiquidTechnologies.Runtime.Standard20.BinaryData.Empty;
            m_ServiceDescription = "";
            m_TechnicalContactUrl = "";
            m_TechnicalInformationUrl = null;
            m_Extension = new Mx.Peppol.Lookup.Reader.BdxSmp201605.XmlObjectCollection<Mx.Peppol.Lookup.Reader.tns.ExtensionType>("Extension", "http://docs.oasis-open.org/bdxr/ns/SMP/2016/05", 0, -1, false);

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
            Mx.Peppol.Lookup.Reader.tns.EndpointType newObject = new Mx.Peppol.Lookup.Reader.tns.EndpointType(_elementName);
            newObject.m_TransportProfile = m_TransportProfile;
            newObject.m_EndpointURI = m_EndpointURI;
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
            foreach (Mx.Peppol.Lookup.Reader.tns.ExtensionType o in m_Extension)
                newObject.m_Extension.Add((Mx.Peppol.Lookup.Reader.tns.ExtensionType)o.Clone());

// ##HAND_CODED_BLOCK_START ID="Additional clone"## DO NOT MODIFY ANYTHING OUTSIDE OF THESE TAGS

// Add Additional clone code here...

// ##HAND_CODED_BLOCK_END ID="Additional clone"## DO NOT MODIFY ANYTHING OUTSIDE OF THESE TAGS

            return newObject;
        }
        #endregion

        #region Member variables

        protected override string TargetNamespace
        {
            get { return "http://docs.oasis-open.org/bdxr/ns/SMP/2016/05"; }
        }

        #region Attribute - transportProfile
        /// <summary>
        /// Represents a mandatory Attribute in the XML document
        /// </summary>
        /// <remarks>
        /// This property is represented as an Attribute in the XML.
        /// It is mandatory and therefore must be populated within the XML.
        /// It is defaulted to "".
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.AttributeInfoPrimitive("transportProfile", "", LiquidTechnologies.Runtime.Standard20.Conversions.ConversionType.type_string, null, LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.WhitespaceRule.Preserve, "", -1, -1, "", "", "", "", -1, -1, -1, null)]
        public string TransportProfile
        {
            get
            {
                return m_TransportProfile;
            }
            set 
            {
                // Apply whitespace rules appropriately
                value = LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.Preserve(value);
                m_TransportProfile = value;
            }
        }
        protected string m_TransportProfile;

        #endregion

        #region Attribute - EndpointURI
        /// <summary>
        /// Represents a mandatory Element in the XML document
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is mandatory and therefore must be populated within the XML.
        /// It is defaulted to "".
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoSeqPrimMnd("EndpointURI", "http://docs.oasis-open.org/bdxr/ns/SMP/2016/05", null, LiquidTechnologies.Runtime.Standard20.Conversions.ConversionType.type_string, null, LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.WhitespaceRule.Collapse, "", -1, -1, "", "", "", "", -1, -1, -1, null)]
        public string EndpointURI
        {
            get
            {
                return m_EndpointURI;
            }
            set 
            {
                // Apply whitespace rules appropriately
                value = LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.Collapse(value);
                m_EndpointURI = value;
            }
        }
        protected string m_EndpointURI;

        #endregion

        #region Attribute - RequireBusinessLevelSignature
        /// <summary>
        /// Represents an optional Element in the XML document
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is optional, initially it is not valid.
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoSeqPrimOpt("RequireBusinessLevelSignature", "http://docs.oasis-open.org/bdxr/ns/SMP/2016/05", true, null, LiquidTechnologies.Runtime.Standard20.Conversions.ConversionType.type_boolean, null, LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.WhitespaceRule.Collapse, "", -1, -1, "", "", "", "", -1, -1, -1, "false")]
        public bool? RequireBusinessLevelSignature
        {
            get 
            { 
                return m_RequireBusinessLevelSignature;  
            }
            set 
            { 
                if (value == null)
                {
                    m_RequireBusinessLevelSignature = null;
                }
                else
                {
                    m_RequireBusinessLevelSignature = value;
                }
            }
        }
        protected bool? m_RequireBusinessLevelSignature;
        #endregion

        #region Attribute - MinimumAuthenticationLevel
        /// <summary>
        /// Represents an optional Element in the XML document
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is optional, initially it is not valid.
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoSeqPrimOpt("MinimumAuthenticationLevel", "http://docs.oasis-open.org/bdxr/ns/SMP/2016/05", true, null, LiquidTechnologies.Runtime.Standard20.Conversions.ConversionType.type_string, null, LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.WhitespaceRule.Preserve, "", -1, -1, "", "", "", "", -1, -1, -1, null)]
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
        /// Represents an optional Element in the XML document
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is optional, initially it is not valid.
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoSeqPrimOpt("ServiceActivationDate", "http://docs.oasis-open.org/bdxr/ns/SMP/2016/05", true, null, LiquidTechnologies.Runtime.Standard20.Conversions.ConversionType.type_datetime, null, LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.WhitespaceRule.Collapse, "", -1, -1, "", "", "", "", -1, -1, -1, null)]
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
        /// Represents an optional Element in the XML document
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is optional, initially it is not valid.
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoSeqPrimOpt("ServiceExpirationDate", "http://docs.oasis-open.org/bdxr/ns/SMP/2016/05", true, null, LiquidTechnologies.Runtime.Standard20.Conversions.ConversionType.type_datetime, null, LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.WhitespaceRule.Collapse, "", -1, -1, "", "", "", "", -1, -1, -1, null)]
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
        /// Represents a mandatory Element in the XML document
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is mandatory and therefore must be populated within the XML.
        /// It is defaulted to LiquidTechnologies.Runtime.Standard20.BinaryData.Empty.
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoSeqPrimMnd("Certificate", "http://docs.oasis-open.org/bdxr/ns/SMP/2016/05", null, LiquidTechnologies.Runtime.Standard20.Conversions.ConversionType.type_base64Binary, null, LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.WhitespaceRule.Collapse, "", -1, -1, "", "", "", "", -1, -1, -1, null)]
        public LiquidTechnologies.Runtime.Standard20.BinaryData Certificate
        {
            get
            {
                return m_Certificate;
            }
            set 
            {
                m_Certificate = value;
            }
        }
        protected LiquidTechnologies.Runtime.Standard20.BinaryData m_Certificate;

        #endregion

        #region Attribute - ServiceDescription
        /// <summary>
        /// Represents a mandatory Element in the XML document
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is mandatory and therefore must be populated within the XML.
        /// It is defaulted to "".
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoSeqPrimMnd("ServiceDescription", "http://docs.oasis-open.org/bdxr/ns/SMP/2016/05", null, LiquidTechnologies.Runtime.Standard20.Conversions.ConversionType.type_string, null, LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.WhitespaceRule.Preserve, "", -1, -1, "", "", "", "", -1, -1, -1, null)]
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
        /// Represents a mandatory Element in the XML document
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is mandatory and therefore must be populated within the XML.
        /// It is defaulted to "".
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoSeqPrimMnd("TechnicalContactUrl", "http://docs.oasis-open.org/bdxr/ns/SMP/2016/05", null, LiquidTechnologies.Runtime.Standard20.Conversions.ConversionType.type_string, null, LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.WhitespaceRule.Collapse, "", -1, -1, "", "", "", "", -1, -1, -1, null)]
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
        /// Represents an optional Element in the XML document
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is optional, initially it is not valid.
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoSeqPrimOpt("TechnicalInformationUrl", "http://docs.oasis-open.org/bdxr/ns/SMP/2016/05", true, null, LiquidTechnologies.Runtime.Standard20.Conversions.ConversionType.type_string, null, LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.WhitespaceRule.Collapse, "", -1, -1, "", "", "", "", -1, -1, -1, null)]
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
        /// A collection of Extensions
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// This collection may contain 0 to Many objects.
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoSeqClsCol("Extension", "http://docs.oasis-open.org/bdxr/ns/SMP/2016/05", LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.Element)]
        public Mx.Peppol.Lookup.Reader.BdxSmp201605.XmlObjectCollection<Mx.Peppol.Lookup.Reader.tns.ExtensionType> Extension
        {
            get { return m_Extension; }
        }
        protected Mx.Peppol.Lookup.Reader.BdxSmp201605.XmlObjectCollection<Mx.Peppol.Lookup.Reader.tns.ExtensionType> m_Extension;

        #endregion

        #region Attribute - Namespace
        public override string Namespace
        {
            get { return "http://docs.oasis-open.org/bdxr/ns/SMP/2016/05"; }
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


