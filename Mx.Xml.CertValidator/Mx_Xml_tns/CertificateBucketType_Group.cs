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
 * Using Schema: C:\src\massivex\hyperway\Mx.Certificates.Validator\Xsd\certvalidator.xsd
 **********************************************************************************************/

namespace Mx.Xml.tns
{
    /// <summary>
    /// This class represents the Element CertificateBucketType_Group
    /// </summary>
    [LiquidTechnologies.Runtime.Standard20.XmlObjectInfo(LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementGroupType.Choice,
                                                    LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.PseudoElement,
                                                    "CertificateBucketType_Group", "http://difi.no/xsd/certvalidator/1.0", false, false, false)]
    public partial class CertificateBucketType_Group : Mx.Xml.CertValidator.XmlCommonBase
    {
        #region Constructors
        /// <summary>
        /// Constructor for CertificateBucketType_Group
        /// </summary>
        /// <remarks>
        /// The class is created with all the mandatory fields populated with the
        /// default data. 
        /// All Collection object are created.
        /// However any 1-n relationships (these are represented as collections) are
        /// empty. To comply with the schema these must be populated before the xml
        /// obtained from ToXml is valid against the schema C:\src\massivex\hyperway\Mx.Certificates.Validator\Xsd\certvalidator.xsd
        /// </remarks>
        public CertificateBucketType_Group()
        {
            _elementName = "CertificateBucketType_Group";
            Init();
        }
        public CertificateBucketType_Group(string elementName)
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
        /// obtained from ToXml is valid against the schema C:\src\massivex\hyperway\Mx.Certificates.Validator\Xsd\certvalidator.xsd.
        /// </remarks>
        protected override void Init()
        {
            Mx.Xml.CertValidator.Registration.iRegistrationIndicator = 0; // causes registration to take place
            m_Certificate = null;
            m_CertificateReference = null;
            m_CertificateStartsWith = null;

            _validElement = "";
// ##HAND_CODED_BLOCK_START ID="Additional Inits"## DO NOT MODIFY ANYTHING OUTSIDE OF THESE TAGS

// Add Additional initialization code here...

// ##HAND_CODED_BLOCK_END ID="Additional Inits"## DO NOT MODIFY ANYTHING OUTSIDE OF THESE TAGS
        }
        protected void ClearChoice(string selectedElement)
        {
            m_Certificate = null;
            m_CertificateReference = null;
            m_CertificateStartsWith = null;
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
            Mx.Xml.tns.CertificateBucketType_Group newObject = new Mx.Xml.tns.CertificateBucketType_Group(_elementName);
            newObject.m_Certificate = m_Certificate;
            newObject.m_CertificateReference = null;
            if (m_CertificateReference != null)
                newObject.m_CertificateReference = (Mx.Xml.tns.CertificateReferenceType)m_CertificateReference.Clone();
            newObject.m_CertificateStartsWith = null;
            if (m_CertificateStartsWith != null)
                newObject.m_CertificateStartsWith = (Mx.Xml.tns.CertificateStartsWithType)m_CertificateStartsWith.Clone();

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
            get { return "http://difi.no/xsd/certvalidator/1.0"; }
        }

        #region Attribute - Certificate
        /// <summary>
        /// Represents an optional Element in the XML document
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is optional, initially it is not valid.
        /// Only one Element within this class may be set at a time, setting this property when another element is already set will raise an exception
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoChoicePrimOpt("Certificate", "http://difi.no/xsd/certvalidator/1.0", true, null, LiquidTechnologies.Runtime.Standard20.Conversions.ConversionType.type_string, null, LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.WhitespaceRule.Preserve, "", -1, -1, "", "", "", "", -1, -1, -1, null)]
        public string Certificate
        {
            get 
            { 
                return m_Certificate;  
            }
            set 
            { 
                if (value == null)
                {
                    m_Certificate = null;
                }
                else
                {
                    // Apply whitespace rules appropriately
                    value = LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.Preserve(value); 
                    // The class represents a choice, so prevent more than one element from being selected
                    ClearChoice("Certificate"); // remove selection
                    m_Certificate = value;
                }
            }
        }
        protected string m_Certificate;
        #endregion

        #region Attribute - CertificateReference
        /// <summary>
        /// Represents an optional Element in the XML document
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is optional, initially it is null.
        /// Only one Element within this class may be set at a time, setting this property when another element is already set will raise an exception. setting this property to null will allow another element to be selected
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoChoiceClsOpt("CertificateReference", "http://difi.no/xsd/certvalidator/1.0", LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.Element, typeof(Mx.Xml.tns.CertificateReferenceType))]
        public Mx.Xml.tns.CertificateReferenceType CertificateReference
        {
            get
            { 
                return m_CertificateReference;
            }
            set
            { 
                // The class represents a choice, so prevent more than one element from being selected
                ClearChoice((value == null)?"":"CertificateReference"); // remove selection
                if (value == null)
                    m_CertificateReference = null;
                else
                {
                    SetElementName(value, "CertificateReference");
                    m_CertificateReference = value; 
                }
            }
        }
        protected Mx.Xml.tns.CertificateReferenceType m_CertificateReference;
        
        #endregion

        #region Attribute - CertificateStartsWith
        /// <summary>
        /// Represents an optional Element in the XML document
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is optional, initially it is null.
        /// Only one Element within this class may be set at a time, setting this property when another element is already set will raise an exception. setting this property to null will allow another element to be selected
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoChoiceClsOpt("CertificateStartsWith", "http://difi.no/xsd/certvalidator/1.0", LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.Element, typeof(Mx.Xml.tns.CertificateStartsWithType))]
        public Mx.Xml.tns.CertificateStartsWithType CertificateStartsWith
        {
            get
            { 
                return m_CertificateStartsWith;
            }
            set
            { 
                // The class represents a choice, so prevent more than one element from being selected
                ClearChoice((value == null)?"":"CertificateStartsWith"); // remove selection
                if (value == null)
                    m_CertificateStartsWith = null;
                else
                {
                    SetElementName(value, "CertificateStartsWith");
                    m_CertificateStartsWith = value; 
                }
            }
        }
        protected Mx.Xml.tns.CertificateStartsWithType m_CertificateStartsWith;
        
        #endregion

        public string ChoiceSelectedElement
        {
            get { return _validElement;  }
        }
        protected string _validElement;
        #region Attribute - Namespace
        public override string Namespace
        {
            get { return "http://difi.no/xsd/certvalidator/1.0"; }
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


