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
    /// This class represents the Element KeyInfo_Group
    /// </summary>
    [LiquidTechnologies.Runtime.Standard20.XmlObjectInfo(LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementGroupType.Choice,
                                                    LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.PseudoElement,
                                                    "KeyInfo_Group", "http://www.w3.org/2000/09/xmldsig#", false, false, false)]
    public partial class KeyInfo_Group : Mx.Xml.Busdox.Smp.XmlCommonBase
    {
        #region Constructors
        /// <summary>
        /// Constructor for KeyInfo_Group
        /// </summary>
        /// <remarks>
        /// The class is created with all the mandatory fields populated with the
        /// default data. 
        /// All Collection object are created.
        /// However any 1-n relationships (these are represented as collections) are
        /// empty. To comply with the schema these must be populated before the xml
        /// obtained from ToXml is valid against the schema C:\src\massivex\hyperway\Mx.Xml.Busdox.Smp\ServiceMetadataPublishingTypes-1.0.xsd
        /// </remarks>
        public KeyInfo_Group()
        {
            _elementName = "KeyInfo_Group";
            Init();
        }
        public KeyInfo_Group(string elementName)
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
            m_KeyName = null;
            m_KeyValue = null;
            m_RetrievalMethod = null;
            m_X509Data = null;
            m_PGPData = null;
            m_SPKIData = null;
            m_MgmtData = null;
            m_AnyElement = null;

            _validElement = "";
// ##HAND_CODED_BLOCK_START ID="Additional Inits"## DO NOT MODIFY ANYTHING OUTSIDE OF THESE TAGS

// Add Additional initialization code here...

// ##HAND_CODED_BLOCK_END ID="Additional Inits"## DO NOT MODIFY ANYTHING OUTSIDE OF THESE TAGS
        }
        protected void ClearChoice(string selectedElement)
        {
            m_KeyName = null;
            m_KeyValue = null;
            m_RetrievalMethod = null;
            m_X509Data = null;
            m_PGPData = null;
            m_SPKIData = null;
            m_MgmtData = null;
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
            Mx.Xml.Busdox.ds.KeyInfo_Group newObject = new Mx.Xml.Busdox.ds.KeyInfo_Group(_elementName);
            newObject.m_KeyName = m_KeyName;
            newObject.m_KeyValue = null;
            if (m_KeyValue != null)
                newObject.m_KeyValue = (Mx.Xml.Busdox.ds.KeyValue)m_KeyValue.Clone();
            newObject.m_RetrievalMethod = null;
            if (m_RetrievalMethod != null)
                newObject.m_RetrievalMethod = (Mx.Xml.Busdox.ds.RetrievalMethod)m_RetrievalMethod.Clone();
            newObject.m_X509Data = null;
            if (m_X509Data != null)
                newObject.m_X509Data = (Mx.Xml.Busdox.ds.X509Data)m_X509Data.Clone();
            newObject.m_PGPData = null;
            if (m_PGPData != null)
                newObject.m_PGPData = (Mx.Xml.Busdox.ds.PGPData)m_PGPData.Clone();
            newObject.m_SPKIData = null;
            if (m_SPKIData != null)
                newObject.m_SPKIData = (Mx.Xml.Busdox.ds.SPKIData)m_SPKIData.Clone();
            newObject.m_MgmtData = m_MgmtData;
            newObject.m_AnyElement = null;
            if (m_AnyElement != null)
                newObject.m_AnyElement = (LiquidTechnologies.Runtime.Standard20.Element)m_AnyElement.Clone();

            newObject._validElement = _validElement;
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

        #region Attribute - KeyName
        /// <summary>
        /// Represents an optional Element in the XML document
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is optional, initially it is not valid.
        /// Only one Element within this class may be set at a time, setting this property when another element is already set will raise an exception
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoChoicePrimOpt("KeyName", "http://www.w3.org/2000/09/xmldsig#", true, null, LiquidTechnologies.Runtime.Standard20.Conversions.ConversionType.type_string, null, LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.WhitespaceRule.Preserve, "", -1, -1, "", "", "", "", -1, -1, -1, null)]
        public string KeyName
        {
            get 
            { 
                return m_KeyName;  
            }
            set 
            { 
                if (value == null)
                {
                    m_KeyName = null;
                }
                else
                {
                    // Apply whitespace rules appropriately
                    value = LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.Preserve(value); 
                    // The class represents a choice, so prevent more than one element from being selected
                    ClearChoice("KeyName"); // remove selection
                    m_KeyName = value;
                }
            }
        }
        protected string m_KeyName;
        #endregion

        #region Attribute - KeyValue
        /// <summary>
        /// Represents an optional Element in the XML document
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is optional, initially it is null.
        /// Only one Element within this class may be set at a time, setting this property when another element is already set will raise an exception. setting this property to null will allow another element to be selected
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoChoiceClsOpt("KeyValue", "http://www.w3.org/2000/09/xmldsig#", LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.Element, typeof(Mx.Xml.Busdox.ds.KeyValue))]
        public Mx.Xml.Busdox.ds.KeyValue KeyValue
        {
            get
            { 
                return m_KeyValue;
            }
            set
            { 
                // The class represents a choice, so prevent more than one element from being selected
                ClearChoice((value == null)?"":"KeyValue"); // remove selection
                if (value == null)
                    m_KeyValue = null;
                else
                {
                    m_KeyValue = value; 
                }
            }
        }
        protected Mx.Xml.Busdox.ds.KeyValue m_KeyValue;
        
        #endregion

        #region Attribute - RetrievalMethod
        /// <summary>
        /// Represents an optional Element in the XML document
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is optional, initially it is null.
        /// Only one Element within this class may be set at a time, setting this property when another element is already set will raise an exception. setting this property to null will allow another element to be selected
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoChoiceClsOpt("RetrievalMethod", "http://www.w3.org/2000/09/xmldsig#", LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.Element, typeof(Mx.Xml.Busdox.ds.RetrievalMethod))]
        public Mx.Xml.Busdox.ds.RetrievalMethod RetrievalMethod
        {
            get
            { 
                return m_RetrievalMethod;
            }
            set
            { 
                // The class represents a choice, so prevent more than one element from being selected
                ClearChoice((value == null)?"":"RetrievalMethod"); // remove selection
                if (value == null)
                    m_RetrievalMethod = null;
                else
                {
                    m_RetrievalMethod = value; 
                }
            }
        }
        protected Mx.Xml.Busdox.ds.RetrievalMethod m_RetrievalMethod;
        
        #endregion

        #region Attribute - X509Data
        /// <summary>
        /// Represents an optional Element in the XML document
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is optional, initially it is null.
        /// Only one Element within this class may be set at a time, setting this property when another element is already set will raise an exception. setting this property to null will allow another element to be selected
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoChoiceClsOpt("X509Data", "http://www.w3.org/2000/09/xmldsig#", LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.Element, typeof(Mx.Xml.Busdox.ds.X509Data))]
        public Mx.Xml.Busdox.ds.X509Data X509Data
        {
            get
            { 
                return m_X509Data;
            }
            set
            { 
                // The class represents a choice, so prevent more than one element from being selected
                ClearChoice((value == null)?"":"X509Data"); // remove selection
                if (value == null)
                    m_X509Data = null;
                else
                {
                    m_X509Data = value; 
                }
            }
        }
        protected Mx.Xml.Busdox.ds.X509Data m_X509Data;
        
        #endregion

        #region Attribute - PGPData
        /// <summary>
        /// Represents an optional Element in the XML document
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is optional, initially it is null.
        /// Only one Element within this class may be set at a time, setting this property when another element is already set will raise an exception. setting this property to null will allow another element to be selected
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoChoiceClsOpt("PGPData", "http://www.w3.org/2000/09/xmldsig#", LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.Element, typeof(Mx.Xml.Busdox.ds.PGPData))]
        public Mx.Xml.Busdox.ds.PGPData PGPData
        {
            get
            { 
                return m_PGPData;
            }
            set
            { 
                // The class represents a choice, so prevent more than one element from being selected
                ClearChoice((value == null)?"":"PGPData"); // remove selection
                if (value == null)
                    m_PGPData = null;
                else
                {
                    m_PGPData = value; 
                }
            }
        }
        protected Mx.Xml.Busdox.ds.PGPData m_PGPData;
        
        #endregion

        #region Attribute - SPKIData
        /// <summary>
        /// Represents an optional Element in the XML document
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is optional, initially it is null.
        /// Only one Element within this class may be set at a time, setting this property when another element is already set will raise an exception. setting this property to null will allow another element to be selected
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoChoiceClsOpt("SPKIData", "http://www.w3.org/2000/09/xmldsig#", LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.Element, typeof(Mx.Xml.Busdox.ds.SPKIData))]
        public Mx.Xml.Busdox.ds.SPKIData SPKIData
        {
            get
            { 
                return m_SPKIData;
            }
            set
            { 
                // The class represents a choice, so prevent more than one element from being selected
                ClearChoice((value == null)?"":"SPKIData"); // remove selection
                if (value == null)
                    m_SPKIData = null;
                else
                {
                    m_SPKIData = value; 
                }
            }
        }
        protected Mx.Xml.Busdox.ds.SPKIData m_SPKIData;
        
        #endregion

        #region Attribute - MgmtData
        /// <summary>
        /// Represents an optional Element in the XML document
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is optional, initially it is not valid.
        /// Only one Element within this class may be set at a time, setting this property when another element is already set will raise an exception
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoChoicePrimOpt("MgmtData", "http://www.w3.org/2000/09/xmldsig#", true, null, LiquidTechnologies.Runtime.Standard20.Conversions.ConversionType.type_string, null, LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.WhitespaceRule.Preserve, "", -1, -1, "", "", "", "", -1, -1, -1, null)]
        public string MgmtData
        {
            get 
            { 
                return m_MgmtData;  
            }
            set 
            { 
                if (value == null)
                {
                    m_MgmtData = null;
                }
                else
                {
                    // Apply whitespace rules appropriately
                    value = LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.Preserve(value); 
                    // The class represents a choice, so prevent more than one element from being selected
                    ClearChoice("MgmtData"); // remove selection
                    m_MgmtData = value;
                }
            }
        }
        protected string m_MgmtData;
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


