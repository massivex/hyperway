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
    /// This class represents the Element Signature
    /// </summary>
    [LiquidTechnologies.Runtime.Standard20.XmlObjectInfo(LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementGroupType.Sequence,
                                                    LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.Element,
                                                    "Signature", "http://www.w3.org/2000/09/xmldsig#", true, false, false)]
    public partial class Signature : Mx.Peppol.Lookup.Reader.BdxSmp201605.XmlCommonBase
    {
        #region Constructors
        /// <summary>
        /// Constructor for Signature
        /// </summary>
        /// <remarks>
        /// The class is created with all the mandatory fields populated with the
        /// default data. 
        /// All Collection object are created.
        /// However any 1-n relationships (these are represented as collections) are
        /// empty. To comply with the schema these must be populated before the xml
        /// obtained from ToXml is valid against the schema C:\src\massivex\hyperway\Mx.Peppol.Lookup\Reader\bdx-smp-201605.xsd
        /// </remarks>
        public Signature()
        {
            _elementName = "Signature";
            Init();
        }
        public Signature(string elementName)
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
            m_SignedInfo = new Mx.Peppol.Lookup.Reader.ds.SignedInfo("SignedInfo");
            m_SignatureValue = new Mx.Peppol.Lookup.Reader.ds.SignatureValue("SignatureValue");
            m_KeyInfo = null;
            m_Object = new Mx.Peppol.Lookup.Reader.BdxSmp201605.XmlObjectCollection<Mx.Peppol.Lookup.Reader.ds.object_>("Object", "http://www.w3.org/2000/09/xmldsig#", 0, -1, false);

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
            Mx.Peppol.Lookup.Reader.ds.Signature newObject = new Mx.Peppol.Lookup.Reader.ds.Signature(_elementName);
            newObject.m_Id = m_Id;
            newObject.m_SignedInfo = null;
            if (m_SignedInfo != null)
                newObject.m_SignedInfo = (Mx.Peppol.Lookup.Reader.ds.SignedInfo)m_SignedInfo.Clone();
            newObject.m_SignatureValue = null;
            if (m_SignatureValue != null)
                newObject.m_SignatureValue = (Mx.Peppol.Lookup.Reader.ds.SignatureValue)m_SignatureValue.Clone();
            newObject.m_KeyInfo = null;
            if (m_KeyInfo != null)
                newObject.m_KeyInfo = (Mx.Peppol.Lookup.Reader.ds.KeyInfo)m_KeyInfo.Clone();
            foreach (Mx.Peppol.Lookup.Reader.ds.object_ o in m_Object)
                newObject.m_Object.Add((Mx.Peppol.Lookup.Reader.ds.object_)o.Clone());

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

        #region Attribute - SignedInfo
        /// <summary>
        /// Represents a mandatory Element in the XML document
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is mandatory and therefore must be populated within the XML.
        /// If this property is set, then the object will be COPIED. If the property is set to null an exception is raised.
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoSeqClsMnd("SignedInfo", "http://www.w3.org/2000/09/xmldsig#", LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.Element, typeof(Mx.Peppol.Lookup.Reader.ds.SignedInfo), true)]
        public Mx.Peppol.Lookup.Reader.ds.SignedInfo SignedInfo
        {
            get 
            { 
                return m_SignedInfo;  
            }
            set 
            { 
                Throw_IfPropertyIsNull(value, "SignedInfo");
                m_SignedInfo = value;
            }
        }
        protected Mx.Peppol.Lookup.Reader.ds.SignedInfo m_SignedInfo;
        
        #endregion

        #region Attribute - SignatureValue
        /// <summary>
        /// Represents a mandatory Element in the XML document
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is mandatory and therefore must be populated within the XML.
        /// If this property is set, then the object will be COPIED. If the property is set to null an exception is raised.
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoSeqClsMnd("SignatureValue", "http://www.w3.org/2000/09/xmldsig#", LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.Element, typeof(Mx.Peppol.Lookup.Reader.ds.SignatureValue), false)]
        public Mx.Peppol.Lookup.Reader.ds.SignatureValue SignatureValue
        {
            get 
            { 
                return m_SignatureValue;  
            }
            set 
            { 
                Throw_IfPropertyIsNull(value, "SignatureValue");
                m_SignatureValue = value;
            }
        }
        protected Mx.Peppol.Lookup.Reader.ds.SignatureValue m_SignatureValue;
        
        #endregion

        #region Attribute - KeyInfo
        /// <summary>
        /// Represents an optional Element in the XML document
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is optional, initially it is null.
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoSeqClsOpt("KeyInfo", "http://www.w3.org/2000/09/xmldsig#", LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.Element, typeof(Mx.Peppol.Lookup.Reader.ds.KeyInfo))]
        public Mx.Peppol.Lookup.Reader.ds.KeyInfo KeyInfo
        {
            get
            { 
                return m_KeyInfo;
            }
            set
            { 
                if (value == null)
                    m_KeyInfo = null;
                else
                {
                    m_KeyInfo = value; 
                }
            }
        }
        protected Mx.Peppol.Lookup.Reader.ds.KeyInfo m_KeyInfo;
        
        #endregion

        #region Attribute - Object
        /// <summary>
        /// A collection of object_s
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// This collection may contain 0 to Many objects.
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoSeqClsCol("Object", "http://www.w3.org/2000/09/xmldsig#", LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.Element)]
        public Mx.Peppol.Lookup.Reader.BdxSmp201605.XmlObjectCollection<Mx.Peppol.Lookup.Reader.ds.object_> object_
        {
            get { return m_Object; }
        }
        protected Mx.Peppol.Lookup.Reader.BdxSmp201605.XmlObjectCollection<Mx.Peppol.Lookup.Reader.ds.object_> m_Object;

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


