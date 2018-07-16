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
    /// This class represents the Element ValidatorRecipe
    /// </summary>
    [LiquidTechnologies.Runtime.Standard20.XmlObjectInfo(LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementGroupType.Sequence,
                                                    LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.Element,
                                                    "ValidatorRecipe", "http://difi.no/xsd/certvalidator/1.0", true, false, false)]
    public partial class ValidatorRecipe : Mx.Xml.CertValidator.XmlCommonBase
    {
        #region Constructors
        /// <summary>
        /// Constructor for ValidatorRecipe
        /// </summary>
        /// <remarks>
        /// The class is created with all the mandatory fields populated with the
        /// default data. 
        /// All Collection object are created.
        /// However any 1-n relationships (these are represented as collections) are
        /// empty. To comply with the schema these must be populated before the xml
        /// obtained from ToXml is valid against the schema C:\src\massivex\hyperway\Mx.Certificates.Validator\Xsd\certvalidator.xsd
        /// </remarks>
        public ValidatorRecipe()
        {
            _elementName = "ValidatorRecipe";
            Init();
        }
        public ValidatorRecipe(string elementName)
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
            m_Name = null;
            m_Version = null;
            m_Validator = new Mx.Xml.CertValidator.XmlObjectCollection<Mx.Xml.tns.ValidatorType>("Validator", "http://difi.no/xsd/certvalidator/1.0", 1, -1, false);
            m_CertificateBucket = new Mx.Xml.CertValidator.XmlObjectCollection<Mx.Xml.tns.CertificateBucketType>("CertificateBucket", "http://difi.no/xsd/certvalidator/1.0", 0, -1, false);
            m_KeyStore = new Mx.Xml.CertValidator.XmlObjectCollection<Mx.Xml.tns.KeyStoreType>("KeyStore", "http://difi.no/xsd/certvalidator/1.0", 0, -1, false);

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
            Mx.Xml.tns.ValidatorRecipe newObject = new Mx.Xml.tns.ValidatorRecipe(_elementName);
            newObject.m_Name = m_Name;
            newObject.m_Version = m_Version;
            foreach (Mx.Xml.tns.ValidatorType o in m_Validator)
                newObject.m_Validator.Add((Mx.Xml.tns.ValidatorType)o.Clone());
            foreach (Mx.Xml.tns.CertificateBucketType o in m_CertificateBucket)
                newObject.m_CertificateBucket.Add((Mx.Xml.tns.CertificateBucketType)o.Clone());
            foreach (Mx.Xml.tns.KeyStoreType o in m_KeyStore)
                newObject.m_KeyStore.Add((Mx.Xml.tns.KeyStoreType)o.Clone());

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

        #region Attribute - name
        /// <summary>
        /// Represents an optional Attribute in the XML document
        /// </summary>
        /// <remarks>
        /// This property is represented as an Attribute in the XML.
        /// It is optional, initially it is not valid.
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.AttributeInfoPrimitive("name", "", true, LiquidTechnologies.Runtime.Standard20.Conversions.ConversionType.type_string, null, LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.WhitespaceRule.Preserve, "", -1, -1, "", "", "", "", -1, -1, -1, null)]
        public string Name
        {
            get 
            { 
                return m_Name;  
            }
            set 
            { 
                if (value == null)
                {
                    m_Name = null;
                }
                else
                {
                    // Apply whitespace rules appropriately
                    value = LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.Preserve(value); 
                    m_Name = value;
                }
            }
        }
        protected string m_Name;
        #endregion

        #region Attribute - version
        /// <summary>
        /// Represents an optional Attribute in the XML document
        /// </summary>
        /// <remarks>
        /// This property is represented as an Attribute in the XML.
        /// It is optional, initially it is not valid.
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.AttributeInfoPrimitive("version", "", true, LiquidTechnologies.Runtime.Standard20.Conversions.ConversionType.type_string, null, LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.WhitespaceRule.Preserve, "", -1, -1, "", "", "", "", -1, -1, -1, null)]
        public string Version
        {
            get 
            { 
                return m_Version;  
            }
            set 
            { 
                if (value == null)
                {
                    m_Version = null;
                }
                else
                {
                    // Apply whitespace rules appropriately
                    value = LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.Preserve(value); 
                    m_Version = value;
                }
            }
        }
        protected string m_Version;
        #endregion

        #region Attribute - Validator
        /// <summary>
        /// A collection of Validators
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// This collection may contain 1 to Many objects.
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoSeqClsCol("Validator", "http://difi.no/xsd/certvalidator/1.0", LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.Element)]
        public Mx.Xml.CertValidator.XmlObjectCollection<Mx.Xml.tns.ValidatorType> Validator
        {
            get { return m_Validator; }
        }
        protected Mx.Xml.CertValidator.XmlObjectCollection<Mx.Xml.tns.ValidatorType> m_Validator;

        #endregion

        #region Attribute - CertificateBucket
        /// <summary>
        /// A collection of CertificateBuckets
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// This collection may contain 0 to Many objects.
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoSeqClsCol("CertificateBucket", "http://difi.no/xsd/certvalidator/1.0", LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.Element)]
        public Mx.Xml.CertValidator.XmlObjectCollection<Mx.Xml.tns.CertificateBucketType> CertificateBucket
        {
            get { return m_CertificateBucket; }
        }
        protected Mx.Xml.CertValidator.XmlObjectCollection<Mx.Xml.tns.CertificateBucketType> m_CertificateBucket;

        #endregion

        #region Attribute - KeyStore
        /// <summary>
        /// A collection of KeyStores
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// This collection may contain 0 to Many objects.
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoSeqClsCol("KeyStore", "http://difi.no/xsd/certvalidator/1.0", LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.Element)]
        public Mx.Xml.CertValidator.XmlObjectCollection<Mx.Xml.tns.KeyStoreType> KeyStore
        {
            get { return m_KeyStore; }
        }
        protected Mx.Xml.CertValidator.XmlObjectCollection<Mx.Xml.tns.KeyStoreType> m_KeyStore;

        #endregion

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


