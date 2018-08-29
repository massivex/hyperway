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
    /// This class represents the Element SPKIData_Group
    /// </summary>
    [LiquidTechnologies.Runtime.Standard20.XmlObjectInfo(LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementGroupType.Sequence,
                                                    LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.PseudoElement,
                                                    "SPKIData_Group", "http://www.w3.org/2000/09/xmldsig#", false, false, false)]
    public partial class SPKIData_Group : Mx.Xml.Busdox.Smp.XmlCommonBase
    {
        #region Constructors
        /// <summary>
        /// Constructor for SPKIData_Group
        /// </summary>
        /// <remarks>
        /// The class is created with all the mandatory fields populated with the
        /// default data. 
        /// All Collection object are created.
        /// However any 1-n relationships (these are represented as collections) are
        /// empty. To comply with the schema these must be populated before the xml
        /// obtained from ToXml is valid against the schema C:\src\massivex\hyperway\Mx.Xml.Busdox.Smp\ServiceMetadataPublishingTypes-1.0.xsd
        /// </remarks>
        public SPKIData_Group()
        {
            _elementName = "SPKIData_Group";
            Init();
        }
        public SPKIData_Group(string elementName)
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
            m_SPKISexp = LiquidTechnologies.Runtime.Standard20.BinaryData.Empty;
            m_AnyElement = null;

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
            Mx.Xml.Busdox.ds.SPKIData_Group newObject = new Mx.Xml.Busdox.ds.SPKIData_Group(_elementName);
            newObject.m_SPKISexp = m_SPKISexp;
            newObject.m_AnyElement = null;
            if (m_AnyElement != null)
                newObject.m_AnyElement = (LiquidTechnologies.Runtime.Standard20.Element)m_AnyElement.Clone();

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

        #region Attribute - SPKISexp
        /// <summary>
        /// Represents a mandatory Element in the XML document
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is mandatory and therefore must be populated within the XML.
        /// It is defaulted to LiquidTechnologies.Runtime.Standard20.BinaryData.Empty.
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoSeqPrimMnd("SPKISexp", "http://www.w3.org/2000/09/xmldsig#", null, LiquidTechnologies.Runtime.Standard20.Conversions.ConversionType.type_base64Binary, null, LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.WhitespaceRule.Collapse, "", -1, -1, "", "", "", "", -1, -1, -1, null)]
        public LiquidTechnologies.Runtime.Standard20.BinaryData SPKISexp
        {
            get
            {
                return m_SPKISexp;
            }
            set 
            {
                m_SPKISexp = value;
            }
        }
        protected LiquidTechnologies.Runtime.Standard20.BinaryData m_SPKISexp;

        #endregion

        #region Attribute - AnyElement
        /// <summary>
        /// Represents an optional untyped element in the XML document
        /// </summary>
        /// <remarks>
        /// It is optional, initially it is null.
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoSeqUntpdOpt("AnyElement", "", "##other", "http://busdox.org/serviceMetadata/publishing/1.0/")]
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
                m_AnyElement = value; 
            }
        }
        protected LiquidTechnologies.Runtime.Standard20.Element m_AnyElement;
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


