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
	/// A single extension for private use.
    /// </summary>
    [LiquidTechnologies.Runtime.Standard20.XmlObjectInfo(LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementGroupType.Sequence,
                                                    LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.Element,
                                                    "ExtensionType", "http://docs.oasis-open.org/bdxr/ns/SMP/2016/05", true, false, false)]
    public partial class ExtensionType : Mx.Peppol.Lookup.Reader.BdxSmp201605.XmlCommonBase
    {
        #region Constructors
        /// <summary>
        /// Constructor for ExtensionType
        /// </summary>
        /// <remarks>
        /// The class is created with all the mandatory fields populated with the
        /// default data. 
        /// All Collection object are created.
        /// However any 1-n relationships (these are represented as collections) are
        /// empty. To comply with the schema these must be populated before the xml
        /// obtained from ToXml is valid against the schema C:\src\massivex\hyperway\Mx.Peppol.Lookup\Reader\bdx-smp-201605.xsd
        /// </remarks>
        public ExtensionType()
        {
            _elementName = "ExtensionType";
            Init();
        }
        public ExtensionType(string elementName)
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
            m_ExtensionID = null;
            m_ExtensionName = null;
            m_ExtensionAgencyID = null;
            m_ExtensionAgencyName = null;
            m_ExtensionAgencyURI = null;
            m_ExtensionVersionID = null;
            m_ExtensionURI = null;
            m_ExtensionReasonCode = null;
            m_ExtensionReason = null;
            m_AnyElement = null;

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
            Mx.Peppol.Lookup.Reader.tns.ExtensionType newObject = new Mx.Peppol.Lookup.Reader.tns.ExtensionType(_elementName);
            newObject.m_ExtensionID = m_ExtensionID;
            newObject.m_ExtensionName = m_ExtensionName;
            newObject.m_ExtensionAgencyID = m_ExtensionAgencyID;
            newObject.m_ExtensionAgencyName = m_ExtensionAgencyName;
            newObject.m_ExtensionAgencyURI = m_ExtensionAgencyURI;
            newObject.m_ExtensionVersionID = m_ExtensionVersionID;
            newObject.m_ExtensionURI = m_ExtensionURI;
            newObject.m_ExtensionReasonCode = m_ExtensionReasonCode;
            newObject.m_ExtensionReason = m_ExtensionReason;
            newObject.m_AnyElement = null;
            if (m_AnyElement != null)
                newObject.m_AnyElement = (LiquidTechnologies.Runtime.Standard20.Element)m_AnyElement.Clone();

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

        #region Attribute - ExtensionID
        /// <summary>
		/// An identifier for the Extension assigned by the creator of the extension.
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is optional, initially it is not valid.
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoSeqPrimOpt("ExtensionID", "http://docs.oasis-open.org/bdxr/ns/SMP/2016/05", true, null, LiquidTechnologies.Runtime.Standard20.Conversions.ConversionType.type_string, null, LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.WhitespaceRule.Collapse, "", -1, -1, "", "", "", "", -1, -1, -1, null)]
        public string ExtensionID
        {
            get 
            { 
                return m_ExtensionID;  
            }
            set 
            { 
                if (value == null)
                {
                    m_ExtensionID = null;
                }
                else
                {
                    // Apply whitespace rules appropriately
                    value = LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.Collapse(value); 
                    m_ExtensionID = value;
                }
            }
        }
        protected string m_ExtensionID;
        #endregion

        #region Attribute - ExtensionName
        /// <summary>
		/// A name for the Extension assigned by the creator of the extension.
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is optional, initially it is not valid.
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoSeqPrimOpt("ExtensionName", "http://docs.oasis-open.org/bdxr/ns/SMP/2016/05", true, null, LiquidTechnologies.Runtime.Standard20.Conversions.ConversionType.type_string, null, LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.WhitespaceRule.Preserve, "", -1, -1, "", "", "", "", -1, -1, -1, null)]
        public string ExtensionName
        {
            get 
            { 
                return m_ExtensionName;  
            }
            set 
            { 
                if (value == null)
                {
                    m_ExtensionName = null;
                }
                else
                {
                    // Apply whitespace rules appropriately
                    value = LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.Preserve(value); 
                    m_ExtensionName = value;
                }
            }
        }
        protected string m_ExtensionName;
        #endregion

        #region Attribute - ExtensionAgencyID
        /// <summary>
		/// An agency that maintains one or more Extensions.
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is optional, initially it is not valid.
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoSeqPrimOpt("ExtensionAgencyID", "http://docs.oasis-open.org/bdxr/ns/SMP/2016/05", true, null, LiquidTechnologies.Runtime.Standard20.Conversions.ConversionType.type_string, null, LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.WhitespaceRule.Preserve, "", -1, -1, "", "", "", "", -1, -1, -1, null)]
        public string ExtensionAgencyID
        {
            get 
            { 
                return m_ExtensionAgencyID;  
            }
            set 
            { 
                if (value == null)
                {
                    m_ExtensionAgencyID = null;
                }
                else
                {
                    // Apply whitespace rules appropriately
                    value = LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.Preserve(value); 
                    m_ExtensionAgencyID = value;
                }
            }
        }
        protected string m_ExtensionAgencyID;
        #endregion

        #region Attribute - ExtensionAgencyName
        /// <summary>
		/// The name of the agency that maintains the Extension.
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is optional, initially it is not valid.
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoSeqPrimOpt("ExtensionAgencyName", "http://docs.oasis-open.org/bdxr/ns/SMP/2016/05", true, null, LiquidTechnologies.Runtime.Standard20.Conversions.ConversionType.type_string, null, LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.WhitespaceRule.Preserve, "", -1, -1, "", "", "", "", -1, -1, -1, null)]
        public string ExtensionAgencyName
        {
            get 
            { 
                return m_ExtensionAgencyName;  
            }
            set 
            { 
                if (value == null)
                {
                    m_ExtensionAgencyName = null;
                }
                else
                {
                    // Apply whitespace rules appropriately
                    value = LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.Preserve(value); 
                    m_ExtensionAgencyName = value;
                }
            }
        }
        protected string m_ExtensionAgencyName;
        #endregion

        #region Attribute - ExtensionAgencyURI
        /// <summary>
		/// A URI for the Agency that maintains the Extension.
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is optional, initially it is not valid.
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoSeqPrimOpt("ExtensionAgencyURI", "http://docs.oasis-open.org/bdxr/ns/SMP/2016/05", true, null, LiquidTechnologies.Runtime.Standard20.Conversions.ConversionType.type_string, null, LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.WhitespaceRule.Collapse, "", -1, -1, "", "", "", "", -1, -1, -1, null)]
        public string ExtensionAgencyURI
        {
            get 
            { 
                return m_ExtensionAgencyURI;  
            }
            set 
            { 
                if (value == null)
                {
                    m_ExtensionAgencyURI = null;
                }
                else
                {
                    // Apply whitespace rules appropriately
                    value = LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.Collapse(value); 
                    m_ExtensionAgencyURI = value;
                }
            }
        }
        protected string m_ExtensionAgencyURI;
        #endregion

        #region Attribute - ExtensionVersionID
        /// <summary>
		/// The version of the Extension.
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is optional, initially it is not valid.
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoSeqPrimOpt("ExtensionVersionID", "http://docs.oasis-open.org/bdxr/ns/SMP/2016/05", true, null, LiquidTechnologies.Runtime.Standard20.Conversions.ConversionType.type_string, null, LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.WhitespaceRule.Collapse, "", -1, -1, "", "", "", "", -1, -1, -1, null)]
        public string ExtensionVersionID
        {
            get 
            { 
                return m_ExtensionVersionID;  
            }
            set 
            { 
                if (value == null)
                {
                    m_ExtensionVersionID = null;
                }
                else
                {
                    // Apply whitespace rules appropriately
                    value = LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.Collapse(value); 
                    m_ExtensionVersionID = value;
                }
            }
        }
        protected string m_ExtensionVersionID;
        #endregion

        #region Attribute - ExtensionURI
        /// <summary>
		/// A URI for the Extension.
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is optional, initially it is not valid.
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoSeqPrimOpt("ExtensionURI", "http://docs.oasis-open.org/bdxr/ns/SMP/2016/05", true, null, LiquidTechnologies.Runtime.Standard20.Conversions.ConversionType.type_string, null, LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.WhitespaceRule.Collapse, "", -1, -1, "", "", "", "", -1, -1, -1, null)]
        public string ExtensionURI
        {
            get 
            { 
                return m_ExtensionURI;  
            }
            set 
            { 
                if (value == null)
                {
                    m_ExtensionURI = null;
                }
                else
                {
                    // Apply whitespace rules appropriately
                    value = LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.Collapse(value); 
                    m_ExtensionURI = value;
                }
            }
        }
        protected string m_ExtensionURI;
        #endregion

        #region Attribute - ExtensionReasonCode
        /// <summary>
		/// A code for reason the Extension is being included.
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is optional, initially it is not valid.
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoSeqPrimOpt("ExtensionReasonCode", "http://docs.oasis-open.org/bdxr/ns/SMP/2016/05", true, null, LiquidTechnologies.Runtime.Standard20.Conversions.ConversionType.type_string, null, LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.WhitespaceRule.Collapse, "", -1, -1, "", "", "", "", -1, -1, -1, null)]
        public string ExtensionReasonCode
        {
            get 
            { 
                return m_ExtensionReasonCode;  
            }
            set 
            { 
                if (value == null)
                {
                    m_ExtensionReasonCode = null;
                }
                else
                {
                    // Apply whitespace rules appropriately
                    value = LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.Collapse(value); 
                    m_ExtensionReasonCode = value;
                }
            }
        }
        protected string m_ExtensionReasonCode;
        #endregion

        #region Attribute - ExtensionReason
        /// <summary>
		/// A description of the reason for the Extension.
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is optional, initially it is not valid.
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoSeqPrimOpt("ExtensionReason", "http://docs.oasis-open.org/bdxr/ns/SMP/2016/05", true, null, LiquidTechnologies.Runtime.Standard20.Conversions.ConversionType.type_string, null, LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.WhitespaceRule.Preserve, "", -1, -1, "", "", "", "", -1, -1, -1, null)]
        public string ExtensionReason
        {
            get 
            { 
                return m_ExtensionReason;  
            }
            set 
            { 
                if (value == null)
                {
                    m_ExtensionReason = null;
                }
                else
                {
                    // Apply whitespace rules appropriately
                    value = LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.Preserve(value); 
                    m_ExtensionReason = value;
                }
            }
        }
        protected string m_ExtensionReason;
        #endregion

        #region Attribute - AnyElement
        /// <summary>
        /// Represents an optional untyped element in the XML document
        /// </summary>
        /// <remarks>
        /// It is optional, initially it is null.
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoSeqUntpdOpt("AnyElement", "", "##other", "http://docs.oasis-open.org/bdxr/ns/SMP/2016/05")]
        public LiquidTechnologies.Runtime.Standard20.Element AnyElement
        {
            get
            {
                return m_AnyElement;
            }
            set
            {
                if (value != null)
                    LiquidTechnologies.Runtime.Standard20.Element.TestNamespace(value.Namespace, "##other", "http://docs.oasis-open.org/bdxr/ns/SMP/2016/05");
                m_AnyElement = value; 
            }
        }
        protected LiquidTechnologies.Runtime.Standard20.Element m_AnyElement;
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


