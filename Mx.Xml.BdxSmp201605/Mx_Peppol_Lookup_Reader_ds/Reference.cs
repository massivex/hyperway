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
    /// This class represents the Element Reference
    /// </summary>
    [LiquidTechnologies.Runtime.Standard20.XmlObjectInfo(LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementGroupType.Sequence,
                                                    LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.Element,
                                                    "Reference", "http://www.w3.org/2000/09/xmldsig#", true, false, false)]
    public partial class Reference : Mx.Peppol.Lookup.Reader.BdxSmp201605.XmlCommonBase
    {
        #region Constructors
        /// <summary>
        /// Constructor for Reference
        /// </summary>
        /// <remarks>
        /// The class is created with all the mandatory fields populated with the
        /// default data. 
        /// All Collection object are created.
        /// However any 1-n relationships (these are represented as collections) are
        /// empty. To comply with the schema these must be populated before the xml
        /// obtained from ToXml is valid against the schema C:\src\massivex\hyperway\Mx.Peppol.Lookup\Reader\bdx-smp-201605.xsd
        /// </remarks>
        public Reference()
        {
            _elementName = "Reference";
            Init();
        }
        public Reference(string elementName)
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
            m_Id = null;
            m_URI = null;
            m_Type = null;
            m_Transforms = null;
            m_DigestMethod = new Mx.Peppol.Lookup.Reader.ds.DigestMethod("DigestMethod");
            m_DigestValue = LiquidTechnologies.Runtime.Standard20.BinaryData.Empty;

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
            Mx.Peppol.Lookup.Reader.ds.Reference newObject = new Mx.Peppol.Lookup.Reader.ds.Reference(_elementName);
            newObject.m_Id = m_Id;
            newObject.m_URI = m_URI;
            newObject.m_Type = m_Type;
            newObject.m_Transforms = null;
            if (m_Transforms != null)
                newObject.m_Transforms = (Mx.Peppol.Lookup.Reader.ds.Transforms)m_Transforms.Clone();
            newObject.m_DigestMethod = null;
            if (m_DigestMethod != null)
                newObject.m_DigestMethod = (Mx.Peppol.Lookup.Reader.ds.DigestMethod)m_DigestMethod.Clone();
            newObject.m_DigestValue = m_DigestValue;

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

        #region Attribute - URI
        /// <summary>
        /// Represents an optional Attribute in the XML document
        /// </summary>
        /// <remarks>
        /// This property is represented as an Attribute in the XML.
        /// It is optional, initially it is not valid.
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.AttributeInfoPrimitive("URI", "", true, LiquidTechnologies.Runtime.Standard20.Conversions.ConversionType.type_string, null, LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.WhitespaceRule.Collapse, "", -1, -1, "", "", "", "", -1, -1, -1, null)]
        public string URI
        {
            get 
            { 
                return m_URI;  
            }
            set 
            { 
                if (value == null)
                {
                    m_URI = null;
                }
                else
                {
                    // Apply whitespace rules appropriately
                    value = LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.Collapse(value); 
                    m_URI = value;
                }
            }
        }
        protected string m_URI;
        #endregion

        #region Attribute - Type
        /// <summary>
        /// Represents an optional Attribute in the XML document
        /// </summary>
        /// <remarks>
        /// This property is represented as an Attribute in the XML.
        /// It is optional, initially it is not valid.
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.AttributeInfoPrimitive("Type", "", true, LiquidTechnologies.Runtime.Standard20.Conversions.ConversionType.type_string, null, LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.WhitespaceRule.Collapse, "", -1, -1, "", "", "", "", -1, -1, -1, null)]
        public string Type
        {
            get 
            { 
                return m_Type;  
            }
            set 
            { 
                if (value == null)
                {
                    m_Type = null;
                }
                else
                {
                    // Apply whitespace rules appropriately
                    value = LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.Collapse(value); 
                    m_Type = value;
                }
            }
        }
        protected string m_Type;
        #endregion

        #region Attribute - Transforms
        /// <summary>
        /// Represents an optional Element in the XML document
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is optional, initially it is null.
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoSeqClsOpt("Transforms", "http://www.w3.org/2000/09/xmldsig#", LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.Element, typeof(Mx.Peppol.Lookup.Reader.ds.Transforms))]
        public Mx.Peppol.Lookup.Reader.ds.Transforms Transforms
        {
            get
            { 
                return m_Transforms;
            }
            set
            { 
                if (value == null)
                    m_Transforms = null;
                else
                {
                    m_Transforms = value; 
                }
            }
        }
        protected Mx.Peppol.Lookup.Reader.ds.Transforms m_Transforms;
        
        #endregion

        #region Attribute - DigestMethod
        /// <summary>
        /// Represents a mandatory Element in the XML document
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is mandatory and therefore must be populated within the XML.
        /// If this property is set, then the object will be COPIED. If the property is set to null an exception is raised.
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoSeqClsMnd("DigestMethod", "http://www.w3.org/2000/09/xmldsig#", LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.Element, typeof(Mx.Peppol.Lookup.Reader.ds.DigestMethod), false)]
        public Mx.Peppol.Lookup.Reader.ds.DigestMethod DigestMethod
        {
            get 
            { 
                return m_DigestMethod;  
            }
            set 
            { 
                Throw_IfPropertyIsNull(value, "DigestMethod");
                m_DigestMethod = value;
            }
        }
        protected Mx.Peppol.Lookup.Reader.ds.DigestMethod m_DigestMethod;
        
        #endregion

        #region Attribute - DigestValue
        /// <summary>
        /// Represents a mandatory Element in the XML document
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is mandatory and therefore must be populated within the XML.
        /// It is defaulted to LiquidTechnologies.Runtime.Standard20.BinaryData.Empty.
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoSeqPrimMnd("DigestValue", "http://www.w3.org/2000/09/xmldsig#", null, LiquidTechnologies.Runtime.Standard20.Conversions.ConversionType.type_base64Binary, null, LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.WhitespaceRule.Collapse, "", -1, -1, "", "", "", "", -1, -1, -1, null)]
        public LiquidTechnologies.Runtime.Standard20.BinaryData DigestValue
        {
            get
            {
                return m_DigestValue;
            }
            set 
            {
                m_DigestValue = value;
            }
        }
        protected LiquidTechnologies.Runtime.Standard20.BinaryData m_DigestValue;

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


