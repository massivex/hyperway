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
    /// This class represents the Element ExtensibleType_Type_Group
    /// </summary>
    [LiquidTechnologies.Runtime.Standard20.XmlObjectInfo(LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementGroupType.Choice,
                                                    LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.PseudoElement,
                                                    "ExtensibleType_Type_Group", "http://difi.no/xsd/certvalidator/1.0", false, false, false)]
    public partial class ExtensibleType_Type_Group : Mx.Xml.CertValidator.XmlCommonBase
    {
        #region Constructors
        /// <summary>
        /// Constructor for ExtensibleType_Type_Group
        /// </summary>
        /// <remarks>
        /// The class is created with all the mandatory fields populated with the
        /// default data. 
        /// All Collection object are created.
        /// However any 1-n relationships (these are represented as collections) are
        /// empty. To comply with the schema these must be populated before the xml
        /// obtained from ToXml is valid against the schema C:\src\massivex\hyperway\Mx.Certificates.Validator\Xsd\certvalidator.xsd
        /// </remarks>
        public ExtensibleType_Type_Group()
        {
            _elementName = "ExtensibleType_Type_Group";
            Init();
        }
        public ExtensibleType_Type_Group(string elementName)
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
            m_Cached = null;
            m_Chain = null;
            m_Class = null;
            m_CriticalExtensionRecognized = null;
            m_CriticalExtensionRequired = null;
            m_CRL = null;
            m_Dummy = null;
            m_Expiration = null;
            m_HandleError = null;
            m_Junction = null;
            m_KeyUsage = null;
            m_OCSP = null;
            m_PrincipleName = null;
            m_RuleReference = null;
            m_Signing = null;
            m_Try = null;
            m_ValidatorReference = null;

            _validElement = "";
// ##HAND_CODED_BLOCK_START ID="Additional Inits"## DO NOT MODIFY ANYTHING OUTSIDE OF THESE TAGS

// Add Additional initialization code here...

// ##HAND_CODED_BLOCK_END ID="Additional Inits"## DO NOT MODIFY ANYTHING OUTSIDE OF THESE TAGS
        }
        protected void ClearChoice(string selectedElement)
        {
            m_Cached = null;
            m_Chain = null;
            m_Class = null;
            m_CriticalExtensionRecognized = null;
            m_CriticalExtensionRequired = null;
            m_CRL = null;
            m_Dummy = null;
            m_Expiration = null;
            m_HandleError = null;
            m_Junction = null;
            m_KeyUsage = null;
            m_OCSP = null;
            m_PrincipleName = null;
            m_RuleReference = null;
            m_Signing = null;
            m_Try = null;
            m_ValidatorReference = null;
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
            Mx.Xml.tns.ExtensibleType_Type_Group newObject = new Mx.Xml.tns.ExtensibleType_Type_Group(_elementName);
            newObject.m_Cached = null;
            if (m_Cached != null)
                newObject.m_Cached = (Mx.Xml.tns.CachedType)m_Cached.Clone();
            newObject.m_Chain = null;
            if (m_Chain != null)
                newObject.m_Chain = (Mx.Xml.tns.ChainType)m_Chain.Clone();
            newObject.m_Class = m_Class;
            newObject.m_CriticalExtensionRecognized = null;
            if (m_CriticalExtensionRecognized != null)
                newObject.m_CriticalExtensionRecognized = (Mx.Xml.tns.CriticalExtensionRecognizedType)m_CriticalExtensionRecognized.Clone();
            newObject.m_CriticalExtensionRequired = null;
            if (m_CriticalExtensionRequired != null)
                newObject.m_CriticalExtensionRequired = (Mx.Xml.tns.CriticalExtensionRequiredType)m_CriticalExtensionRequired.Clone();
            newObject.m_CRL = null;
            if (m_CRL != null)
                newObject.m_CRL = (Mx.Xml.tns.CRLType)m_CRL.Clone();
            newObject.m_Dummy = m_Dummy;
            newObject.m_Expiration = null;
            if (m_Expiration != null)
                newObject.m_Expiration = (Mx.Xml.tns.ExpirationType)m_Expiration.Clone();
            newObject.m_HandleError = null;
            if (m_HandleError != null)
                newObject.m_HandleError = (Mx.Xml.tns.HandleErrorType)m_HandleError.Clone();
            newObject.m_Junction = null;
            if (m_Junction != null)
                newObject.m_Junction = (Mx.Xml.tns.JunctionType)m_Junction.Clone();
            newObject.m_KeyUsage = null;
            if (m_KeyUsage != null)
                newObject.m_KeyUsage = (Mx.Xml.tns.KeyUsageType)m_KeyUsage.Clone();
            newObject.m_OCSP = null;
            if (m_OCSP != null)
                newObject.m_OCSP = (Mx.Xml.tns.OCSPType)m_OCSP.Clone();
            newObject.m_PrincipleName = null;
            if (m_PrincipleName != null)
                newObject.m_PrincipleName = (Mx.Xml.tns.PrincipleNameType)m_PrincipleName.Clone();
            newObject.m_RuleReference = null;
            if (m_RuleReference != null)
                newObject.m_RuleReference = (Mx.Xml.tns.RuleReferenceType)m_RuleReference.Clone();
            newObject.m_Signing = null;
            if (m_Signing != null)
                newObject.m_Signing = (Mx.Xml.tns.SigningType)m_Signing.Clone();
            newObject.m_Try = null;
            if (m_Try != null)
                newObject.m_Try = (Mx.Xml.tns.TryType)m_Try.Clone();
            newObject.m_ValidatorReference = null;
            if (m_ValidatorReference != null)
                newObject.m_ValidatorReference = (Mx.Xml.tns.ValidatorReferenceType)m_ValidatorReference.Clone();

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

        #region Attribute - Cached
        /// <summary>
        /// Represents an optional Element in the XML document
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is optional, initially it is null.
        /// Only one Element within this class may be set at a time, setting this property when another element is already set will raise an exception. setting this property to null will allow another element to be selected
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoChoiceClsOpt("Cached", "http://difi.no/xsd/certvalidator/1.0", LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.Element, typeof(Mx.Xml.tns.CachedType))]
        public Mx.Xml.tns.CachedType Cached
        {
            get
            { 
                return m_Cached;
            }
            set
            { 
                // The class represents a choice, so prevent more than one element from being selected
                ClearChoice((value == null)?"":"Cached"); // remove selection
                if (value == null)
                    m_Cached = null;
                else
                {
                    SetElementName(value, "Cached");
                    m_Cached = value; 
                }
            }
        }
        protected Mx.Xml.tns.CachedType m_Cached;
        
        #endregion

        #region Attribute - Chain
        /// <summary>
        /// Represents an optional Element in the XML document
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is optional, initially it is null.
        /// Only one Element within this class may be set at a time, setting this property when another element is already set will raise an exception. setting this property to null will allow another element to be selected
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoChoiceClsOpt("Chain", "http://difi.no/xsd/certvalidator/1.0", LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.Element, typeof(Mx.Xml.tns.ChainType))]
        public Mx.Xml.tns.ChainType Chain
        {
            get
            { 
                return m_Chain;
            }
            set
            { 
                // The class represents a choice, so prevent more than one element from being selected
                ClearChoice((value == null)?"":"Chain"); // remove selection
                if (value == null)
                    m_Chain = null;
                else
                {
                    SetElementName(value, "Chain");
                    m_Chain = value; 
                }
            }
        }
        protected Mx.Xml.tns.ChainType m_Chain;
        
        #endregion

        #region Attribute - Class
        /// <summary>
        /// Represents an optional Element in the XML document
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is optional, initially it is not valid.
        /// Only one Element within this class may be set at a time, setting this property when another element is already set will raise an exception
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoChoicePrimOpt("Class", "http://difi.no/xsd/certvalidator/1.0", true, null, LiquidTechnologies.Runtime.Standard20.Conversions.ConversionType.type_string, null, LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.WhitespaceRule.Preserve, "", -1, -1, "", "", "", "", -1, -1, -1, null)]
        public string class_
        {
            get 
            { 
                return m_Class;  
            }
            set 
            { 
                if (value == null)
                {
                    m_Class = null;
                }
                else
                {
                    // Apply whitespace rules appropriately
                    value = LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.Preserve(value); 
                    // The class represents a choice, so prevent more than one element from being selected
                    ClearChoice("class_"); // remove selection
                    m_Class = value;
                }
            }
        }
        protected string m_Class;
        #endregion

        #region Attribute - CriticalExtensionRecognized
        /// <summary>
        /// Represents an optional Element in the XML document
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is optional, initially it is null.
        /// Only one Element within this class may be set at a time, setting this property when another element is already set will raise an exception. setting this property to null will allow another element to be selected
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoChoiceClsOpt("CriticalExtensionRecognized", "http://difi.no/xsd/certvalidator/1.0", LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.Element, typeof(Mx.Xml.tns.CriticalExtensionRecognizedType))]
        public Mx.Xml.tns.CriticalExtensionRecognizedType CriticalExtensionRecognized
        {
            get
            { 
                return m_CriticalExtensionRecognized;
            }
            set
            { 
                // The class represents a choice, so prevent more than one element from being selected
                ClearChoice((value == null)?"":"CriticalExtensionRecognized"); // remove selection
                if (value == null)
                    m_CriticalExtensionRecognized = null;
                else
                {
                    SetElementName(value, "CriticalExtensionRecognized");
                    m_CriticalExtensionRecognized = value; 
                }
            }
        }
        protected Mx.Xml.tns.CriticalExtensionRecognizedType m_CriticalExtensionRecognized;
        
        #endregion

        #region Attribute - CriticalExtensionRequired
        /// <summary>
        /// Represents an optional Element in the XML document
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is optional, initially it is null.
        /// Only one Element within this class may be set at a time, setting this property when another element is already set will raise an exception. setting this property to null will allow another element to be selected
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoChoiceClsOpt("CriticalExtensionRequired", "http://difi.no/xsd/certvalidator/1.0", LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.Element, typeof(Mx.Xml.tns.CriticalExtensionRequiredType))]
        public Mx.Xml.tns.CriticalExtensionRequiredType CriticalExtensionRequired
        {
            get
            { 
                return m_CriticalExtensionRequired;
            }
            set
            { 
                // The class represents a choice, so prevent more than one element from being selected
                ClearChoice((value == null)?"":"CriticalExtensionRequired"); // remove selection
                if (value == null)
                    m_CriticalExtensionRequired = null;
                else
                {
                    SetElementName(value, "CriticalExtensionRequired");
                    m_CriticalExtensionRequired = value; 
                }
            }
        }
        protected Mx.Xml.tns.CriticalExtensionRequiredType m_CriticalExtensionRequired;
        
        #endregion

        #region Attribute - CRL
        /// <summary>
        /// Represents an optional Element in the XML document
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is optional, initially it is null.
        /// Only one Element within this class may be set at a time, setting this property when another element is already set will raise an exception. setting this property to null will allow another element to be selected
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoChoiceClsOpt("CRL", "http://difi.no/xsd/certvalidator/1.0", LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.Element, typeof(Mx.Xml.tns.CRLType))]
        public Mx.Xml.tns.CRLType CRL
        {
            get
            { 
                return m_CRL;
            }
            set
            { 
                // The class represents a choice, so prevent more than one element from being selected
                ClearChoice((value == null)?"":"CRL"); // remove selection
                if (value == null)
                    m_CRL = null;
                else
                {
                    SetElementName(value, "CRL");
                    m_CRL = value; 
                }
            }
        }
        protected Mx.Xml.tns.CRLType m_CRL;
        
        #endregion

        #region Attribute - Dummy
        /// <summary>
        /// Represents an optional Element in the XML document
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is optional, initially it is not valid.
        /// Only one Element within this class may be set at a time, setting this property when another element is already set will raise an exception
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoChoicePrimOpt("Dummy", "http://difi.no/xsd/certvalidator/1.0", true, null, LiquidTechnologies.Runtime.Standard20.Conversions.ConversionType.type_string, null, LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.WhitespaceRule.Preserve, "", -1, -1, "", "", "", "", -1, -1, -1, null)]
        public string Dummy
        {
            get 
            { 
                return m_Dummy;  
            }
            set 
            { 
                if (value == null)
                {
                    m_Dummy = null;
                }
                else
                {
                    // Apply whitespace rules appropriately
                    value = LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.Preserve(value); 
                    // The class represents a choice, so prevent more than one element from being selected
                    ClearChoice("Dummy"); // remove selection
                    m_Dummy = value;
                }
            }
        }
        protected string m_Dummy;
        #endregion

        #region Attribute - Expiration
        /// <summary>
        /// Represents an optional Element in the XML document
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is optional, initially it is null.
        /// Only one Element within this class may be set at a time, setting this property when another element is already set will raise an exception. setting this property to null will allow another element to be selected
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoChoiceClsOpt("Expiration", "http://difi.no/xsd/certvalidator/1.0", LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.Element, typeof(Mx.Xml.tns.ExpirationType))]
        public Mx.Xml.tns.ExpirationType Expiration
        {
            get
            { 
                return m_Expiration;
            }
            set
            { 
                // The class represents a choice, so prevent more than one element from being selected
                ClearChoice((value == null)?"":"Expiration"); // remove selection
                if (value == null)
                    m_Expiration = null;
                else
                {
                    SetElementName(value, "Expiration");
                    m_Expiration = value; 
                }
            }
        }
        protected Mx.Xml.tns.ExpirationType m_Expiration;
        
        #endregion

        #region Attribute - HandleError
        /// <summary>
        /// Represents an optional Element in the XML document
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is optional, initially it is null.
        /// Only one Element within this class may be set at a time, setting this property when another element is already set will raise an exception. setting this property to null will allow another element to be selected
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoChoiceClsOpt("HandleError", "http://difi.no/xsd/certvalidator/1.0", LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.Element, typeof(Mx.Xml.tns.HandleErrorType))]
        public Mx.Xml.tns.HandleErrorType HandleError
        {
            get
            { 
                return m_HandleError;
            }
            set
            { 
                // The class represents a choice, so prevent more than one element from being selected
                ClearChoice((value == null)?"":"HandleError"); // remove selection
                if (value == null)
                    m_HandleError = null;
                else
                {
                    SetElementName(value, "HandleError");
                    m_HandleError = value; 
                }
            }
        }
        protected Mx.Xml.tns.HandleErrorType m_HandleError;
        
        #endregion

        #region Attribute - Junction
        /// <summary>
        /// Represents an optional Element in the XML document
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is optional, initially it is null.
        /// Only one Element within this class may be set at a time, setting this property when another element is already set will raise an exception. setting this property to null will allow another element to be selected
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoChoiceClsOpt("Junction", "http://difi.no/xsd/certvalidator/1.0", LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.Element, typeof(Mx.Xml.tns.JunctionType))]
        public Mx.Xml.tns.JunctionType Junction
        {
            get
            { 
                return m_Junction;
            }
            set
            { 
                // The class represents a choice, so prevent more than one element from being selected
                ClearChoice((value == null)?"":"Junction"); // remove selection
                if (value == null)
                    m_Junction = null;
                else
                {
                    SetElementName(value, "Junction");
                    m_Junction = value; 
                }
            }
        }
        protected Mx.Xml.tns.JunctionType m_Junction;
        
        #endregion

        #region Attribute - KeyUsage
        /// <summary>
        /// Represents an optional Element in the XML document
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is optional, initially it is null.
        /// Only one Element within this class may be set at a time, setting this property when another element is already set will raise an exception. setting this property to null will allow another element to be selected
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoChoiceClsOpt("KeyUsage", "http://difi.no/xsd/certvalidator/1.0", LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.Element, typeof(Mx.Xml.tns.KeyUsageType))]
        public Mx.Xml.tns.KeyUsageType KeyUsage
        {
            get
            { 
                return m_KeyUsage;
            }
            set
            { 
                // The class represents a choice, so prevent more than one element from being selected
                ClearChoice((value == null)?"":"KeyUsage"); // remove selection
                if (value == null)
                    m_KeyUsage = null;
                else
                {
                    SetElementName(value, "KeyUsage");
                    m_KeyUsage = value; 
                }
            }
        }
        protected Mx.Xml.tns.KeyUsageType m_KeyUsage;
        
        #endregion

        #region Attribute - OCSP
        /// <summary>
        /// Represents an optional Element in the XML document
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is optional, initially it is null.
        /// Only one Element within this class may be set at a time, setting this property when another element is already set will raise an exception. setting this property to null will allow another element to be selected
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoChoiceClsOpt("OCSP", "http://difi.no/xsd/certvalidator/1.0", LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.Element, typeof(Mx.Xml.tns.OCSPType))]
        public Mx.Xml.tns.OCSPType OCSP
        {
            get
            { 
                return m_OCSP;
            }
            set
            { 
                // The class represents a choice, so prevent more than one element from being selected
                ClearChoice((value == null)?"":"OCSP"); // remove selection
                if (value == null)
                    m_OCSP = null;
                else
                {
                    SetElementName(value, "OCSP");
                    m_OCSP = value; 
                }
            }
        }
        protected Mx.Xml.tns.OCSPType m_OCSP;
        
        #endregion

        #region Attribute - PrincipleName
        /// <summary>
        /// Represents an optional Element in the XML document
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is optional, initially it is null.
        /// Only one Element within this class may be set at a time, setting this property when another element is already set will raise an exception. setting this property to null will allow another element to be selected
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoChoiceClsOpt("PrincipleName", "http://difi.no/xsd/certvalidator/1.0", LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.Element, typeof(Mx.Xml.tns.PrincipleNameType))]
        public Mx.Xml.tns.PrincipleNameType PrincipleName
        {
            get
            { 
                return m_PrincipleName;
            }
            set
            { 
                // The class represents a choice, so prevent more than one element from being selected
                ClearChoice((value == null)?"":"PrincipleName"); // remove selection
                if (value == null)
                    m_PrincipleName = null;
                else
                {
                    SetElementName(value, "PrincipleName");
                    m_PrincipleName = value; 
                }
            }
        }
        protected Mx.Xml.tns.PrincipleNameType m_PrincipleName;
        
        #endregion

        #region Attribute - RuleReference
        /// <summary>
        /// Represents an optional Element in the XML document
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is optional, initially it is null.
        /// Only one Element within this class may be set at a time, setting this property when another element is already set will raise an exception. setting this property to null will allow another element to be selected
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoChoiceClsOpt("RuleReference", "http://difi.no/xsd/certvalidator/1.0", LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.Element, typeof(Mx.Xml.tns.RuleReferenceType))]
        public Mx.Xml.tns.RuleReferenceType RuleReference
        {
            get
            { 
                return m_RuleReference;
            }
            set
            { 
                // The class represents a choice, so prevent more than one element from being selected
                ClearChoice((value == null)?"":"RuleReference"); // remove selection
                if (value == null)
                    m_RuleReference = null;
                else
                {
                    SetElementName(value, "RuleReference");
                    m_RuleReference = value; 
                }
            }
        }
        protected Mx.Xml.tns.RuleReferenceType m_RuleReference;
        
        #endregion

        #region Attribute - Signing
        /// <summary>
        /// Represents an optional Element in the XML document
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is optional, initially it is null.
        /// Only one Element within this class may be set at a time, setting this property when another element is already set will raise an exception. setting this property to null will allow another element to be selected
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoChoiceClsOpt("Signing", "http://difi.no/xsd/certvalidator/1.0", LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.Element, typeof(Mx.Xml.tns.SigningType))]
        public Mx.Xml.tns.SigningType Signing
        {
            get
            { 
                return m_Signing;
            }
            set
            { 
                // The class represents a choice, so prevent more than one element from being selected
                ClearChoice((value == null)?"":"Signing"); // remove selection
                if (value == null)
                    m_Signing = null;
                else
                {
                    SetElementName(value, "Signing");
                    m_Signing = value; 
                }
            }
        }
        protected Mx.Xml.tns.SigningType m_Signing;
        
        #endregion

        #region Attribute - Try
        /// <summary>
        /// Represents an optional Element in the XML document
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is optional, initially it is null.
        /// Only one Element within this class may be set at a time, setting this property when another element is already set will raise an exception. setting this property to null will allow another element to be selected
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoChoiceClsOpt("Try", "http://difi.no/xsd/certvalidator/1.0", LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.Element, typeof(Mx.Xml.tns.TryType))]
        public Mx.Xml.tns.TryType try_
        {
            get
            { 
                return m_Try;
            }
            set
            { 
                // The class represents a choice, so prevent more than one element from being selected
                ClearChoice((value == null)?"":"try_"); // remove selection
                if (value == null)
                    m_Try = null;
                else
                {
                    SetElementName(value, "Try");
                    m_Try = value; 
                }
            }
        }
        protected Mx.Xml.tns.TryType m_Try;
        
        #endregion

        #region Attribute - ValidatorReference
        /// <summary>
        /// Represents an optional Element in the XML document
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is optional, initially it is null.
        /// Only one Element within this class may be set at a time, setting this property when another element is already set will raise an exception. setting this property to null will allow another element to be selected
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoChoiceClsOpt("ValidatorReference", "http://difi.no/xsd/certvalidator/1.0", LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.Element, typeof(Mx.Xml.tns.ValidatorReferenceType))]
        public Mx.Xml.tns.ValidatorReferenceType ValidatorReference
        {
            get
            { 
                return m_ValidatorReference;
            }
            set
            { 
                // The class represents a choice, so prevent more than one element from being selected
                ClearChoice((value == null)?"":"ValidatorReference"); // remove selection
                if (value == null)
                    m_ValidatorReference = null;
                else
                {
                    SetElementName(value, "ValidatorReference");
                    m_ValidatorReference = value; 
                }
            }
        }
        protected Mx.Xml.tns.ValidatorReferenceType m_ValidatorReference;
        
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


