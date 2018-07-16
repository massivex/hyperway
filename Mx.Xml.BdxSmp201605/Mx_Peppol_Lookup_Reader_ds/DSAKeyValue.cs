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
    /// This class represents the Element DSAKeyValue
    /// </summary>
    [LiquidTechnologies.Runtime.Standard20.XmlObjectInfo(LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementGroupType.Sequence,
                                                    LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.Element,
                                                    "DSAKeyValue", "http://www.w3.org/2000/09/xmldsig#", true, false, false)]
    public partial class DSAKeyValue : Mx.Peppol.Lookup.Reader.BdxSmp201605.XmlCommonBase
    {
        #region Constructors
        /// <summary>
        /// Constructor for DSAKeyValue
        /// </summary>
        /// <remarks>
        /// The class is created with all the mandatory fields populated with the
        /// default data. 
        /// All Collection object are created.
        /// However any 1-n relationships (these are represented as collections) are
        /// empty. To comply with the schema these must be populated before the xml
        /// obtained from ToXml is valid against the schema C:\src\massivex\hyperway\Mx.Peppol.Lookup\Reader\bdx-smp-201605.xsd
        /// </remarks>
        public DSAKeyValue()
        {
            _elementName = "DSAKeyValue";
            Init();
        }
        public DSAKeyValue(string elementName)
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
            m_DSAKeyValue_Seq = null;
            m_G = null;
            m_Y = LiquidTechnologies.Runtime.Standard20.BinaryData.Empty;
            m_J = null;
            m_DSAKeyValue_SeqA = null;

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
            Mx.Peppol.Lookup.Reader.ds.DSAKeyValue newObject = new Mx.Peppol.Lookup.Reader.ds.DSAKeyValue(_elementName);
            newObject.m_DSAKeyValue_Seq = null;
            if (m_DSAKeyValue_Seq != null)
                newObject.m_DSAKeyValue_Seq = (Mx.Peppol.Lookup.Reader.ds.DSAKeyValue_Seq)m_DSAKeyValue_Seq.Clone();
            newObject.m_G = m_G;
            newObject.m_Y = m_Y;
            newObject.m_J = m_J;
            newObject.m_DSAKeyValue_SeqA = null;
            if (m_DSAKeyValue_SeqA != null)
                newObject.m_DSAKeyValue_SeqA = (Mx.Peppol.Lookup.Reader.ds.DSAKeyValue_SeqA)m_DSAKeyValue_SeqA.Clone();

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

        #region Attribute - DSAKeyValue_Seq
        /// <summary>
        /// Represents an optional Element in the XML document
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is optional, initially it is null.
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoSeqClsOpt("DSAKeyValue_Seq", "", LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.PseudoElement, typeof(Mx.Peppol.Lookup.Reader.ds.DSAKeyValue_Seq))]
        public Mx.Peppol.Lookup.Reader.ds.DSAKeyValue_Seq DSAKeyValue_Seq
        {
            get
            { 
                return m_DSAKeyValue_Seq;
            }
            set
            { 
                if (value == null)
                    m_DSAKeyValue_Seq = null;
                else
                {
                    m_DSAKeyValue_Seq = value; 
                }
            }
        }
        protected Mx.Peppol.Lookup.Reader.ds.DSAKeyValue_Seq m_DSAKeyValue_Seq;
        
        #endregion

        #region Attribute - G
        /// <summary>
        /// Represents an optional Element in the XML document
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is optional, initially it is not valid.
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoSeqPrimOpt("G", "http://www.w3.org/2000/09/xmldsig#", true, null, LiquidTechnologies.Runtime.Standard20.Conversions.ConversionType.type_base64Binary, null, LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.WhitespaceRule.Collapse, "", -1, -1, "", "", "", "", -1, -1, -1, null)]
        public LiquidTechnologies.Runtime.Standard20.BinaryData G
        {
            get 
            { 
                return m_G;  
            }
            set 
            { 
                if (value == null)
                {
                    m_G = null;
                }
                else
                {
                    m_G = value;
                }
            }
        }
        protected LiquidTechnologies.Runtime.Standard20.BinaryData m_G;
        #endregion

        #region Attribute - Y
        /// <summary>
        /// Represents a mandatory Element in the XML document
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is mandatory and therefore must be populated within the XML.
        /// It is defaulted to LiquidTechnologies.Runtime.Standard20.BinaryData.Empty.
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoSeqPrimMnd("Y", "http://www.w3.org/2000/09/xmldsig#", null, LiquidTechnologies.Runtime.Standard20.Conversions.ConversionType.type_base64Binary, null, LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.WhitespaceRule.Collapse, "", -1, -1, "", "", "", "", -1, -1, -1, null)]
        public LiquidTechnologies.Runtime.Standard20.BinaryData Y
        {
            get
            {
                return m_Y;
            }
            set 
            {
                m_Y = value;
            }
        }
        protected LiquidTechnologies.Runtime.Standard20.BinaryData m_Y;

        #endregion

        #region Attribute - J
        /// <summary>
        /// Represents an optional Element in the XML document
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is optional, initially it is not valid.
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoSeqPrimOpt("J", "http://www.w3.org/2000/09/xmldsig#", true, null, LiquidTechnologies.Runtime.Standard20.Conversions.ConversionType.type_base64Binary, null, LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.WhitespaceRule.Collapse, "", -1, -1, "", "", "", "", -1, -1, -1, null)]
        public LiquidTechnologies.Runtime.Standard20.BinaryData J
        {
            get 
            { 
                return m_J;  
            }
            set 
            { 
                if (value == null)
                {
                    m_J = null;
                }
                else
                {
                    m_J = value;
                }
            }
        }
        protected LiquidTechnologies.Runtime.Standard20.BinaryData m_J;
        #endregion

        #region Attribute - DSAKeyValue_SeqA
        /// <summary>
        /// Represents an optional Element in the XML document
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is optional, initially it is null.
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoSeqClsOpt("DSAKeyValue_SeqA", "", LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.PseudoElement, typeof(Mx.Peppol.Lookup.Reader.ds.DSAKeyValue_SeqA))]
        public Mx.Peppol.Lookup.Reader.ds.DSAKeyValue_SeqA DSAKeyValue_SeqA
        {
            get
            { 
                return m_DSAKeyValue_SeqA;
            }
            set
            { 
                if (value == null)
                    m_DSAKeyValue_SeqA = null;
                else
                {
                    m_DSAKeyValue_SeqA = value; 
                }
            }
        }
        protected Mx.Peppol.Lookup.Reader.ds.DSAKeyValue_SeqA m_DSAKeyValue_SeqA;
        
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


