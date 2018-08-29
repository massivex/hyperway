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
    /// This class represents the Element SignedInfo
    /// </summary>
    [LiquidTechnologies.Runtime.Standard20.XmlObjectInfo(LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementGroupType.Sequence,
                                                    LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.Element,
                                                    "SignedInfo", "http://www.w3.org/2000/09/xmldsig#", true, false, false)]
    public partial class SignedInfo : Mx.Xml.Busdox.Smp.XmlCommonBase
    {
        #region Constructors
        /// <summary>
        /// Constructor for SignedInfo
        /// </summary>
        /// <remarks>
        /// The class is created with all the mandatory fields populated with the
        /// default data. 
        /// All Collection object are created.
        /// However any 1-n relationships (these are represented as collections) are
        /// empty. To comply with the schema these must be populated before the xml
        /// obtained from ToXml is valid against the schema C:\src\massivex\hyperway\Mx.Xml.Busdox.Smp\ServiceMetadataPublishingTypes-1.0.xsd
        /// </remarks>
        public SignedInfo()
        {
            _elementName = "SignedInfo";
            Init();
        }
        public SignedInfo(string elementName)
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
            m_Id = null;
            m_CanonicalizationMethod = new Mx.Xml.Busdox.ds.CanonicalizationMethod("CanonicalizationMethod");
            m_SignatureMethod = new Mx.Xml.Busdox.ds.SignatureMethod("SignatureMethod");
            m_Reference = new Mx.Xml.Busdox.Smp.XmlObjectCollection<Mx.Xml.Busdox.ds.Reference>("Reference", "http://www.w3.org/2000/09/xmldsig#", 1, -1, false);

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
            Mx.Xml.Busdox.ds.SignedInfo newObject = new Mx.Xml.Busdox.ds.SignedInfo(_elementName);
            newObject.m_Id = m_Id;
            newObject.m_CanonicalizationMethod = null;
            if (m_CanonicalizationMethod != null)
                newObject.m_CanonicalizationMethod = (Mx.Xml.Busdox.ds.CanonicalizationMethod)m_CanonicalizationMethod.Clone();
            newObject.m_SignatureMethod = null;
            if (m_SignatureMethod != null)
                newObject.m_SignatureMethod = (Mx.Xml.Busdox.ds.SignatureMethod)m_SignatureMethod.Clone();
            foreach (Mx.Xml.Busdox.ds.Reference o in m_Reference)
                newObject.m_Reference.Add((Mx.Xml.Busdox.ds.Reference)o.Clone());

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

        #region Attribute - Id
        /// <summary>
        /// Represents an optional Attribute in the XML document
        /// </summary>
        /// <remarks>
        /// This property is represented as an Attribute in the XML.
        /// It is optional, initially it is not valid.
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.AttributeInfoPrimitive("Id", "", true, LiquidTechnologies.Runtime.Standard20.Conversions.ConversionType.type_string, null, LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.WhitespaceRule.Collapse, "", -1, -1, "", "", "", "", -1, -1, -1, null)]
        public string Id
        {
            get 
            { 
                return m_Id;  
            }
            set 
            { 
                if (value == null)
                {
                    m_Id = null;
                }
                else
                {
                    // Apply whitespace rules appropriately
                    value = LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.Collapse(value); 
                    m_Id = value;
                }
            }
        }
        protected string m_Id;
        #endregion

        #region Attribute - CanonicalizationMethod
        /// <summary>
        /// Represents a mandatory Element in the XML document
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is mandatory and therefore must be populated within the XML.
        /// If this property is set, then the object will be COPIED. If the property is set to null an exception is raised.
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoSeqClsMnd("CanonicalizationMethod", "http://www.w3.org/2000/09/xmldsig#", LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.Element, typeof(Mx.Xml.Busdox.ds.CanonicalizationMethod), false)]
        public Mx.Xml.Busdox.ds.CanonicalizationMethod CanonicalizationMethod
        {
            get 
            { 
                return m_CanonicalizationMethod;  
            }
            set 
            { 
                Throw_IfPropertyIsNull(value, "CanonicalizationMethod");
                m_CanonicalizationMethod = value;
            }
        }
        protected Mx.Xml.Busdox.ds.CanonicalizationMethod m_CanonicalizationMethod;
        
        #endregion

        #region Attribute - SignatureMethod
        /// <summary>
        /// Represents a mandatory Element in the XML document
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is mandatory and therefore must be populated within the XML.
        /// If this property is set, then the object will be COPIED. If the property is set to null an exception is raised.
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoSeqClsMnd("SignatureMethod", "http://www.w3.org/2000/09/xmldsig#", LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.Element, typeof(Mx.Xml.Busdox.ds.SignatureMethod), false)]
        public Mx.Xml.Busdox.ds.SignatureMethod SignatureMethod
        {
            get 
            { 
                return m_SignatureMethod;  
            }
            set 
            { 
                Throw_IfPropertyIsNull(value, "SignatureMethod");
                m_SignatureMethod = value;
            }
        }
        protected Mx.Xml.Busdox.ds.SignatureMethod m_SignatureMethod;
        
        #endregion

        #region Attribute - Reference
        /// <summary>
        /// A collection of References
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// This collection may contain 1 to Many objects.
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoSeqClsCol("Reference", "http://www.w3.org/2000/09/xmldsig#", LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.Element)]
        public Mx.Xml.Busdox.Smp.XmlObjectCollection<Mx.Xml.Busdox.ds.Reference> Reference
        {
            get { return m_Reference; }
        }
        protected Mx.Xml.Busdox.Smp.XmlObjectCollection<Mx.Xml.Busdox.ds.Reference> m_Reference;

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


