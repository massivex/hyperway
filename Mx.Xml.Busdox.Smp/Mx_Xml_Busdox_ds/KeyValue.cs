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

namespace Mx.Xml.Busdox.ds
{
    /// <summary>
    /// This class represents the Element KeyValue
    /// </summary>
    [LiquidTechnologies.Runtime.Standard20.XmlObjectInfo(LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementGroupType.Choice,
                                                    LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.Element,
                                                    "KeyValue", "http://www.w3.org/2000/09/xmldsig#", true, false, false)]
    public partial class KeyValue : Mx.Xml.Busdox.Smp.XmlCommonBase
    {
        #region Constructors
        /// <summary>
        /// Constructor for KeyValue
        /// </summary>
        /// <remarks>
        /// The class is created with all the mandatory fields populated with the
        /// default data. 
        /// All Collection object are created.
        /// However any 1-n relationships (these are represented as collections) are
        /// empty. To comply with the schema these must be populated before the xml
        /// obtained from ToXml is valid against the schema C:\src\massivex\hyperway\Mx.Xml.Busdox.Smp\ServiceMetadataPublishingTypes-1.0.xsd
        /// </remarks>
        public KeyValue()
        {
            _elementName = "KeyValue";
            Init();
        }
        public KeyValue(string elementName)
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
            m_DSAKeyValue = null;
            m_RSAKeyValue = null;
            m_AnyElement = null;

            _primitiveValue = "";
            _validElement = "";
// ##HAND_CODED_BLOCK_START ID="Additional Inits"## DO NOT MODIFY ANYTHING OUTSIDE OF THESE TAGS

// Add Additional initialization code here...

// ##HAND_CODED_BLOCK_END ID="Additional Inits"## DO NOT MODIFY ANYTHING OUTSIDE OF THESE TAGS
        }
        protected void ClearChoice(string selectedElement)
        {
            m_DSAKeyValue = null;
            m_RSAKeyValue = null;
            m_AnyElement = null;
            _validElement = selectedElement;
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
            Mx.Xml.Busdox.ds.KeyValue newObject = new Mx.Xml.Busdox.ds.KeyValue(_elementName);
            newObject.m_DSAKeyValue = null;
            if (m_DSAKeyValue != null)
                newObject.m_DSAKeyValue = (Mx.Xml.Busdox.ds.DSAKeyValue)m_DSAKeyValue.Clone();
            newObject.m_RSAKeyValue = null;
            if (m_RSAKeyValue != null)
                newObject.m_RSAKeyValue = (Mx.Xml.Busdox.ds.RSAKeyValue)m_RSAKeyValue.Clone();
            newObject.m_AnyElement = null;
            if (m_AnyElement != null)
                newObject.m_AnyElement = (LiquidTechnologies.Runtime.Standard20.Element)m_AnyElement.Clone();

            newObject._validElement = _validElement;
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
            get { return "http://www.w3.org/2000/09/xmldsig#"; }
        }

        #region Attribute - DSAKeyValue
        /// <summary>
        /// Represents an optional Element in the XML document
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is optional, initially it is null.
        /// Only one Element within this class may be set at a time, setting this property when another element is already set will raise an exception. setting this property to null will allow another element to be selected
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoChoiceClsOpt("DSAKeyValue", "http://www.w3.org/2000/09/xmldsig#", LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.Element, typeof(Mx.Xml.Busdox.ds.DSAKeyValue))]
        public Mx.Xml.Busdox.ds.DSAKeyValue DSAKeyValue
        {
            get
            { 
                return m_DSAKeyValue;
            }
            set
            { 
                // The class represents a choice, so prevent more than one element from being selected
                ClearChoice((value == null)?"":"DSAKeyValue"); // remove selection
                if (value == null)
                    m_DSAKeyValue = null;
                else
                {
                    m_DSAKeyValue = value; 
                }
            }
        }
        protected Mx.Xml.Busdox.ds.DSAKeyValue m_DSAKeyValue;
        
        #endregion

        #region Attribute - RSAKeyValue
        /// <summary>
        /// Represents an optional Element in the XML document
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is optional, initially it is null.
        /// Only one Element within this class may be set at a time, setting this property when another element is already set will raise an exception. setting this property to null will allow another element to be selected
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoChoiceClsOpt("RSAKeyValue", "http://www.w3.org/2000/09/xmldsig#", LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.Element, typeof(Mx.Xml.Busdox.ds.RSAKeyValue))]
        public Mx.Xml.Busdox.ds.RSAKeyValue RSAKeyValue
        {
            get
            { 
                return m_RSAKeyValue;
            }
            set
            { 
                // The class represents a choice, so prevent more than one element from being selected
                ClearChoice((value == null)?"":"RSAKeyValue"); // remove selection
                if (value == null)
                    m_RSAKeyValue = null;
                else
                {
                    m_RSAKeyValue = value; 
                }
            }
        }
        protected Mx.Xml.Busdox.ds.RSAKeyValue m_RSAKeyValue;
        
        #endregion

        #region Attribute - AnyElement
        /// <summary>
        /// Represents an optional untyped element in the XML document
        /// </summary>
        /// <remarks>
        /// It is optional, initially it is null.
        /// Only one Element within this class may be set at a time, setting this property when another element is already set will raise an exception. setting this property to null will allow another element to be selected
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoChoiceUntpdOpt("AnyElement", "", "##other", "http://busdox.org/serviceMetadata/publishing/1.0/")]
        public LiquidTechnologies.Runtime.Standard20.Element AnyElement
        {
            get
            {
                return m_AnyElement;
            }
            set
            {
                if (value != null)
                    LiquidTechnologies.Runtime.Standard20.Element.TestNamespace(value.Namespace, "##other", "http://busdox.org/serviceMetadata/publishing/1.0/");
                // The class represents a choice, so prevent more than one element from being selected
                ClearChoice((value == null)?"":"AnyElement");
                m_AnyElement = value; 
            }
        }
        protected LiquidTechnologies.Runtime.Standard20.Element m_AnyElement;
        #endregion

        public string ChoiceSelectedElement
        {
            get { return _validElement;  }
        }
        protected string _validElement;
        #region Attribute - PrimitiveValue

        /// <summary>
        /// The InnerText for this element
        /// </summary>
        /// <remarks>
        /// This class represents the element KeyValue, this
        /// element is open, and as such we can place data into it.
        /// i.e. <KeyValue>Some Data</KeyValue>
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
            get { return "http://www.w3.org/2000/09/xmldsig#"; }
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


