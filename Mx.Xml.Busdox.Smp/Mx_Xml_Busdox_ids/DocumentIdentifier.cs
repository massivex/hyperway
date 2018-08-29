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

namespace Mx.Xml.Busdox.ids
{
    /// <summary>
    /// This class represents the Element DocumentIdentifier
    /// </summary>
    [LiquidTechnologies.Runtime.Standard20.XmlObjectInfo(LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementGroupType.Sequence,
                                                    LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.Element,
                                                    "DocumentIdentifier", "http://busdox.org/transport/identifiers/1.0/", true, false, false)]
    public partial class DocumentIdentifier : Mx.Xml.Busdox.Smp.XmlCommonBase
    {
        #region Constructors
        /// <summary>
        /// Constructor for DocumentIdentifier
        /// </summary>
        /// <remarks>
        /// The class is created with all the mandatory fields populated with the
        /// default data. 
        /// All Collection object are created.
        /// However any 1-n relationships (these are represented as collections) are
        /// empty. To comply with the schema these must be populated before the xml
        /// obtained from ToXml is valid against the schema C:\src\massivex\hyperway\Mx.Xml.Busdox.Smp\ServiceMetadataPublishingTypes-1.0.xsd
        /// </remarks>
        public DocumentIdentifier()
        {
            _elementName = "DocumentIdentifier";
            Init();
        }
        public DocumentIdentifier(string elementName)
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
            m_Scheme = null;

            _primitiveValue = "";
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
            Mx.Xml.Busdox.ids.DocumentIdentifier newObject = new Mx.Xml.Busdox.ids.DocumentIdentifier(_elementName);
            newObject.m_Scheme = m_Scheme;

            newObject._primitiveValue = _primitiveValue;
// ##HAND_CODED_BLOCK_START ID="Additional clone"## DO NOT MODIFY ANYTHING OUTSIDE OF THESE TAGS

// Add Additional clone code here...

// ##HAND_CODED_BLOCK_END ID="Additional clone"## DO NOT MODIFY ANYTHING OUTSIDE OF THESE TAGS

            return newObject;
        }
        #endregion

        #region Member variables

        protected override string TargetNamespace
        {
            get { return "http://busdox.org/transport/identifiers/1.0/"; }
        }

        #region Attribute - scheme
        /// <summary>
        /// Represents an optional Attribute in the XML document
        /// </summary>
        /// <remarks>
        /// This property is represented as an Attribute in the XML.
        /// It is optional, initially it is not valid.
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.AttributeInfoPrimitive("scheme", "", true, LiquidTechnologies.Runtime.Standard20.Conversions.ConversionType.type_string, null, LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.WhitespaceRule.Preserve, "", -1, -1, "", "", "", "", -1, -1, -1, null)]
        public string Scheme
        {
            get 
            { 
                return m_Scheme;  
            }
            set 
            { 
                if (value == null)
                {
                    m_Scheme = null;
                }
                else
                {
                    // Apply whitespace rules appropriately
                    value = LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.Preserve(value); 
                    m_Scheme = value;
                }
            }
        }
        protected string m_Scheme;
        #endregion

        #region Attribute - PrimitiveValue

        /// <summary>
        /// The InnerText for this element
        /// </summary>
        /// <remarks>
        /// This class represents the element DocumentIdentifier, this
        /// element is open, and as such we can place data into it.
        /// i.e. <DocumentIdentifier>Some Data</DocumentIdentifier>
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.PrimitiveValueInfo(LiquidTechnologies.Runtime.Standard20.Conversions.ConversionType.type_string, null, LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.WhitespaceRule.Preserve, "", -1, -1, "", "", "", "", -1, -1, -1)]
        public string PrimitiveValue
        {
            get
            {
                return _primitiveValue;
            }
            set
            {
                // Apply whitespace rules appropriately
                value = LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.Preserve(value);
                _primitiveValue = value;
            }
        }
        protected string _primitiveValue;
        #endregion
        #region Attribute - Namespace
        public override string Namespace
        {
            get { return "http://busdox.org/transport/identifiers/1.0/"; }
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


