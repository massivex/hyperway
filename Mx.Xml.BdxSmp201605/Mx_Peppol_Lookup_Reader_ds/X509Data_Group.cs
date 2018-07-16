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

namespace Mx.Peppol.Lookup.Reader.ds
{
    /// <summary>
    /// This class represents the Element X509Data_Group
    /// </summary>
    [LiquidTechnologies.Runtime.Standard20.XmlObjectInfo(LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementGroupType.Choice,
                                                    LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.PseudoElement,
                                                    "X509Data_Group", "http://www.w3.org/2000/09/xmldsig#", false, false, false)]
    public partial class X509Data_Group : Mx.Peppol.Lookup.Reader.BdxSmp201605.XmlCommonBase
    {
        #region Constructors
        /// <summary>
        /// Constructor for X509Data_Group
        /// </summary>
        /// <remarks>
        /// The class is created with all the mandatory fields populated with the
        /// default data. 
        /// All Collection object are created.
        /// However any 1-n relationships (these are represented as collections) are
        /// empty. To comply with the schema these must be populated before the xml
        /// obtained from ToXml is valid against the schema C:\src\massivex\hyperway\Mx.Peppol.Lookup\Reader\bdx-smp-201605.xsd
        /// </remarks>
        public X509Data_Group()
        {
            _elementName = "X509Data_Group";
            Init();
        }
        public X509Data_Group(string elementName)
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
            m_X509IssuerSerial = null;
            m_X509SKI = null;
            m_X509SubjectName = null;
            m_X509Certificate = null;
            m_X509CRL = null;
            m_AnyElement = null;

            _validElement = "";
// ##HAND_CODED_BLOCK_START ID="Additional Inits"## DO NOT MODIFY ANYTHING OUTSIDE OF THESE TAGS

// Add Additional initialization code here...

// ##HAND_CODED_BLOCK_END ID="Additional Inits"## DO NOT MODIFY ANYTHING OUTSIDE OF THESE TAGS
        }
        protected void ClearChoice(string selectedElement)
        {
            m_X509IssuerSerial = null;
            m_X509SKI = null;
            m_X509SubjectName = null;
            m_X509Certificate = null;
            m_X509CRL = null;
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
            Mx.Peppol.Lookup.Reader.ds.X509Data_Group newObject = new Mx.Peppol.Lookup.Reader.ds.X509Data_Group(_elementName);
            newObject.m_X509IssuerSerial = null;
            if (m_X509IssuerSerial != null)
                newObject.m_X509IssuerSerial = (Mx.Peppol.Lookup.Reader.ds.X509IssuerSerialType)m_X509IssuerSerial.Clone();
            newObject.m_X509SKI = m_X509SKI;
            newObject.m_X509SubjectName = m_X509SubjectName;
            newObject.m_X509Certificate = m_X509Certificate;
            newObject.m_X509CRL = m_X509CRL;
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

        #region Attribute - X509IssuerSerial
        /// <summary>
        /// Represents an optional Element in the XML document
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is optional, initially it is null.
        /// Only one Element within this class may be set at a time, setting this property when another element is already set will raise an exception. setting this property to null will allow another element to be selected
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoChoiceClsOpt("X509IssuerSerial", "http://www.w3.org/2000/09/xmldsig#", LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.Element, typeof(Mx.Peppol.Lookup.Reader.ds.X509IssuerSerialType))]
        public Mx.Peppol.Lookup.Reader.ds.X509IssuerSerialType X509IssuerSerial
        {
            get
            { 
                return m_X509IssuerSerial;
            }
            set
            { 
                // The class represents a choice, so prevent more than one element from being selected
                ClearChoice((value == null)?"":"X509IssuerSerial"); // remove selection
                if (value == null)
                    m_X509IssuerSerial = null;
                else
                {
                    SetElementName(value, "X509IssuerSerial");
                    m_X509IssuerSerial = value; 
                }
            }
        }
        protected Mx.Peppol.Lookup.Reader.ds.X509IssuerSerialType m_X509IssuerSerial;
        
        #endregion

        #region Attribute - X509SKI
        /// <summary>
        /// Represents an optional Element in the XML document
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is optional, initially it is not valid.
        /// Only one Element within this class may be set at a time, setting this property when another element is already set will raise an exception
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoChoicePrimOpt("X509SKI", "http://www.w3.org/2000/09/xmldsig#", true, null, LiquidTechnologies.Runtime.Standard20.Conversions.ConversionType.type_base64Binary, null, LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.WhitespaceRule.Collapse, "", -1, -1, "", "", "", "", -1, -1, -1, null)]
        public LiquidTechnologies.Runtime.Standard20.BinaryData X509SKI
        {
            get 
            { 
                return m_X509SKI;  
            }
            set 
            { 
                if (value == null)
                {
                    m_X509SKI = null;
                }
                else
                {
                    // The class represents a choice, so prevent more than one element from being selected
                    ClearChoice("X509SKI"); // remove selection
                    m_X509SKI = value;
                }
            }
        }
        protected LiquidTechnologies.Runtime.Standard20.BinaryData m_X509SKI;
        #endregion

        #region Attribute - X509SubjectName
        /// <summary>
        /// Represents an optional Element in the XML document
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is optional, initially it is not valid.
        /// Only one Element within this class may be set at a time, setting this property when another element is already set will raise an exception
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoChoicePrimOpt("X509SubjectName", "http://www.w3.org/2000/09/xmldsig#", true, null, LiquidTechnologies.Runtime.Standard20.Conversions.ConversionType.type_string, null, LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.WhitespaceRule.Preserve, "", -1, -1, "", "", "", "", -1, -1, -1, null)]
        public string X509SubjectName
        {
            get 
            { 
                return m_X509SubjectName;  
            }
            set 
            { 
                if (value == null)
                {
                    m_X509SubjectName = null;
                }
                else
                {
                    // Apply whitespace rules appropriately
                    value = LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.Preserve(value); 
                    // The class represents a choice, so prevent more than one element from being selected
                    ClearChoice("X509SubjectName"); // remove selection
                    m_X509SubjectName = value;
                }
            }
        }
        protected string m_X509SubjectName;
        #endregion

        #region Attribute - X509Certificate
        /// <summary>
        /// Represents an optional Element in the XML document
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is optional, initially it is not valid.
        /// Only one Element within this class may be set at a time, setting this property when another element is already set will raise an exception
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoChoicePrimOpt("X509Certificate", "http://www.w3.org/2000/09/xmldsig#", true, null, LiquidTechnologies.Runtime.Standard20.Conversions.ConversionType.type_base64Binary, null, LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.WhitespaceRule.Collapse, "", -1, -1, "", "", "", "", -1, -1, -1, null)]
        public LiquidTechnologies.Runtime.Standard20.BinaryData X509Certificate
        {
            get 
            { 
                return m_X509Certificate;  
            }
            set 
            { 
                if (value == null)
                {
                    m_X509Certificate = null;
                }
                else
                {
                    // The class represents a choice, so prevent more than one element from being selected
                    ClearChoice("X509Certificate"); // remove selection
                    m_X509Certificate = value;
                }
            }
        }
        protected LiquidTechnologies.Runtime.Standard20.BinaryData m_X509Certificate;
        #endregion

        #region Attribute - X509CRL
        /// <summary>
        /// Represents an optional Element in the XML document
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is optional, initially it is not valid.
        /// Only one Element within this class may be set at a time, setting this property when another element is already set will raise an exception
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoChoicePrimOpt("X509CRL", "http://www.w3.org/2000/09/xmldsig#", true, null, LiquidTechnologies.Runtime.Standard20.Conversions.ConversionType.type_base64Binary, null, LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.WhitespaceRule.Collapse, "", -1, -1, "", "", "", "", -1, -1, -1, null)]
        public LiquidTechnologies.Runtime.Standard20.BinaryData X509CRL
        {
            get 
            { 
                return m_X509CRL;  
            }
            set 
            { 
                if (value == null)
                {
                    m_X509CRL = null;
                }
                else
                {
                    // The class represents a choice, so prevent more than one element from being selected
                    ClearChoice("X509CRL"); // remove selection
                    m_X509CRL = value;
                }
            }
        }
        protected LiquidTechnologies.Runtime.Standard20.BinaryData m_X509CRL;
        #endregion

        #region Attribute - AnyElement
        /// <summary>
        /// Represents an optional untyped element in the XML document
        /// </summary>
        /// <remarks>
        /// It is optional, initially it is null.
        /// Only one Element within this class may be set at a time, setting this property when another element is already set will raise an exception. setting this property to null will allow another element to be selected
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoChoiceUntpdOpt("AnyElement", "", "##other", "http://docs.oasis-open.org/bdxr/ns/SMP/2016/05")]
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


