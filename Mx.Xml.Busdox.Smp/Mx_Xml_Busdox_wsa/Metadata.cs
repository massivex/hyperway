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

namespace Mx.Xml.Busdox.wsa
{
    /// <summary>
    /// This class represents the Element Metadata
    /// </summary>
    [LiquidTechnologies.Runtime.Standard20.XmlObjectInfo(LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementGroupType.Sequence,
                                                    LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.Element,
                                                    "Metadata", "http://www.w3.org/2005/08/addressing", true, false, false)]
    public partial class Metadata : Mx.Xml.Busdox.Smp.XmlCommonBase
    {
        #region Constructors
        /// <summary>
        /// Constructor for Metadata
        /// </summary>
        /// <remarks>
        /// The class is created with all the mandatory fields populated with the
        /// default data. 
        /// All Collection object are created.
        /// However any 1-n relationships (these are represented as collections) are
        /// empty. To comply with the schema these must be populated before the xml
        /// obtained from ToXml is valid against the schema C:\src\massivex\hyperway\Mx.Xml.Busdox.Smp\ServiceMetadataPublishingTypes-1.0.xsd
        /// </remarks>
        public Metadata()
        {
            _elementName = "Metadata";
            Init();
        }
        public Metadata(string elementName)
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
            m_AnyElement = new LiquidTechnologies.Runtime.Standard20.ElementCol("http://www.w3.org/2005/08/addressing", "##any", "http://www.w3.org/2005/08/addressing", 0, -1);

            _anyAttributes = new LiquidTechnologies.Runtime.Standard20.AttributeCol("##other", "##other", "http://busdox.org/serviceMetadata/publishing/1.0/");
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
            Mx.Xml.Busdox.wsa.Metadata newObject = new Mx.Xml.Busdox.wsa.Metadata(_elementName);
            foreach (LiquidTechnologies.Runtime.Standard20.Element o in m_AnyElement)
                newObject.m_AnyElement.Add((LiquidTechnologies.Runtime.Standard20.Element)o.Clone());

            newObject._anyAttributes = (LiquidTechnologies.Runtime.Standard20.AttributeCol)_anyAttributes.Clone();
// ##HAND_CODED_BLOCK_START ID="Additional clone"## DO NOT MODIFY ANYTHING OUTSIDE OF THESE TAGS

// Add Additional clone code here...

// ##HAND_CODED_BLOCK_END ID="Additional clone"## DO NOT MODIFY ANYTHING OUTSIDE OF THESE TAGS

            return newObject;
        }
        #endregion

        #region Member variables

        protected override string TargetNamespace
        {
            get { return "http://www.w3.org/2005/08/addressing"; }
        }

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

        #region Attribute - AnyAttribute
        
        /// <summary>
        /// Additional attributes
        /// </summary>
        [LiquidTechnologies.Runtime.Standard20.AttributeInfoAny("Any", "http://www.w3.org/2005/08/addressing", "##other", "http://busdox.org/serviceMetadata/publishing/1.0/", null)]
        public LiquidTechnologies.Runtime.Standard20.AttributeCol AnyAttributes
        {
            get { return _anyAttributes;  }
        }
        protected LiquidTechnologies.Runtime.Standard20.AttributeCol _anyAttributes;
        #endregion
        #region Attribute - Namespace
        public override string Namespace
        {
            get { return "http://www.w3.org/2005/08/addressing"; }
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


