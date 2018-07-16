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
    /// This class represents the Element PGPData_SeqA
    /// </summary>
    [LiquidTechnologies.Runtime.Standard20.XmlObjectInfo(LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementGroupType.Sequence,
                                                    LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.PseudoElement,
                                                    "PGPData_SeqA", "http://www.w3.org/2000/09/xmldsig#", false, false, false)]
    public partial class PGPData_SeqA : Mx.Peppol.Lookup.Reader.BdxSmp201605.XmlCommonBase
    {
        #region Constructors
        /// <summary>
        /// Constructor for PGPData_SeqA
        /// </summary>
        /// <remarks>
        /// The class is created with all the mandatory fields populated with the
        /// default data. 
        /// All Collection object are created.
        /// However any 1-n relationships (these are represented as collections) are
        /// empty. To comply with the schema these must be populated before the xml
        /// obtained from ToXml is valid against the schema C:\src\massivex\hyperway\Mx.Peppol.Lookup\Reader\bdx-smp-201605.xsd
        /// </remarks>
        public PGPData_SeqA()
        {
            _elementName = "PGPData_SeqA";
            Init();
        }
        public PGPData_SeqA(string elementName)
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
            m_PGPKeyPacket = LiquidTechnologies.Runtime.Standard20.BinaryData.Empty;
            m_AnyElement = new LiquidTechnologies.Runtime.Standard20.ElementCol("", "##other", "http://www.w3.org/2000/09/xmldsig#", 0, -1);

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
            Mx.Peppol.Lookup.Reader.ds.PGPData_SeqA newObject = new Mx.Peppol.Lookup.Reader.ds.PGPData_SeqA(_elementName);
            newObject.m_PGPKeyPacket = m_PGPKeyPacket;
            foreach (LiquidTechnologies.Runtime.Standard20.Element o in m_AnyElement)
                newObject.m_AnyElement.Add((LiquidTechnologies.Runtime.Standard20.Element)o.Clone());

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

        #region Attribute - PGPKeyPacket
        /// <summary>
        /// Represents a mandatory Element in the XML document
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is mandatory and therefore must be populated within the XML.
        /// It is defaulted to LiquidTechnologies.Runtime.Standard20.BinaryData.Empty.
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoSeqPrimMnd("PGPKeyPacket", "http://www.w3.org/2000/09/xmldsig#", null, LiquidTechnologies.Runtime.Standard20.Conversions.ConversionType.type_base64Binary, null, LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.WhitespaceRule.Collapse, "", -1, -1, "", "", "", "", -1, -1, -1, null)]
        public LiquidTechnologies.Runtime.Standard20.BinaryData PGPKeyPacket
        {
            get
            {
                return m_PGPKeyPacket;
            }
            set 
            {
                m_PGPKeyPacket = value;
            }
        }
        protected LiquidTechnologies.Runtime.Standard20.BinaryData m_PGPKeyPacket;

        #endregion

        #region Attribute - AnyElement
        /// <summary>
        /// A collection of untyped elements
        /// </summary>
        /// <remarks>
        /// This property is represented as a collection of Elements in the XML.
        /// This collection may contain 0 to Many objects.
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoSeqUntpdCol("AnyElement", "")]
        public LiquidTechnologies.Runtime.Standard20.ElementCol AnyElement
        {
            get { return m_AnyElement; }
        }
        protected LiquidTechnologies.Runtime.Standard20.ElementCol m_AnyElement;
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


