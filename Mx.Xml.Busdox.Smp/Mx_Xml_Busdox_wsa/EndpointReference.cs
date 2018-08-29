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
    /// This class represents the Element EndpointReference
    /// </summary>
    [LiquidTechnologies.Runtime.Standard20.XmlObjectInfo(LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementGroupType.Sequence,
                                                    LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.Element,
                                                    "EndpointReference", "http://www.w3.org/2005/08/addressing", true, false, false)]
    public partial class EndpointReference : Mx.Xml.Busdox.Smp.XmlCommonBase
    {
        #region Constructors
        /// <summary>
        /// Constructor for EndpointReference
        /// </summary>
        /// <remarks>
        /// The class is created with all the mandatory fields populated with the
        /// default data. 
        /// All Collection object are created.
        /// However any 1-n relationships (these are represented as collections) are
        /// empty. To comply with the schema these must be populated before the xml
        /// obtained from ToXml is valid against the schema C:\src\massivex\hyperway\Mx.Xml.Busdox.Smp\ServiceMetadataPublishingTypes-1.0.xsd
        /// </remarks>
        public EndpointReference()
        {
            _elementName = "EndpointReference";
            Init();
        }
        public EndpointReference(string elementName)
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
            m_Address = new Mx.Xml.Busdox.wsa.AttributedURIType("Address");
            m_ReferenceParameters = null;
            m_Metadata = null;
            m_AnyElement = new LiquidTechnologies.Runtime.Standard20.ElementCol("", "##other", "http://www.w3.org/2005/08/addressing", 0, -1);

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
            Mx.Xml.Busdox.wsa.EndpointReference newObject = new Mx.Xml.Busdox.wsa.EndpointReference(_elementName);
            newObject.m_Address = null;
            if (m_Address != null)
                newObject.m_Address = (Mx.Xml.Busdox.wsa.AttributedURIType)m_Address.Clone();
            newObject.m_ReferenceParameters = null;
            if (m_ReferenceParameters != null)
                newObject.m_ReferenceParameters = (Mx.Xml.Busdox.wsa.ReferenceParameters)m_ReferenceParameters.Clone();
            newObject.m_Metadata = null;
            if (m_Metadata != null)
                newObject.m_Metadata = (Mx.Xml.Busdox.wsa.Metadata)m_Metadata.Clone();
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

        #region Attribute - Address
        /// <summary>
        /// Represents a mandatory Element in the XML document
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is mandatory and therefore must be populated within the XML.
        /// If this property is set, then the object will be COPIED. If the property is set to null an exception is raised.
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoSeqClsMnd("Address", "http://www.w3.org/2005/08/addressing", LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.Element, typeof(Mx.Xml.Busdox.wsa.AttributedURIType), false)]
        public Mx.Xml.Busdox.wsa.AttributedURIType Address
        {
            get 
            { 
                return m_Address;  
            }
            set 
            { 
                Throw_IfPropertyIsNull(value, "Address");
                if (value != null)
                    SetElementName(value, "Address");
                m_Address = value;
            }
        }
        protected Mx.Xml.Busdox.wsa.AttributedURIType m_Address;
        
        #endregion

        #region Attribute - ReferenceParameters
        /// <summary>
        /// Represents an optional Element in the XML document
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is optional, initially it is null.
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoSeqClsOpt("ReferenceParameters", "http://www.w3.org/2005/08/addressing", LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.Element, typeof(Mx.Xml.Busdox.wsa.ReferenceParameters))]
        public Mx.Xml.Busdox.wsa.ReferenceParameters ReferenceParameters
        {
            get
            { 
                return m_ReferenceParameters;
            }
            set
            { 
                if (value == null)
                    m_ReferenceParameters = null;
                else
                {
                    m_ReferenceParameters = value; 
                }
            }
        }
        protected Mx.Xml.Busdox.wsa.ReferenceParameters m_ReferenceParameters;
        
        #endregion

        #region Attribute - Metadata
        /// <summary>
        /// Represents an optional Element in the XML document
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is optional, initially it is null.
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoSeqClsOpt("Metadata", "http://www.w3.org/2005/08/addressing", LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.Element, typeof(Mx.Xml.Busdox.wsa.Metadata))]
        public Mx.Xml.Busdox.wsa.Metadata Metadata
        {
            get
            { 
                return m_Metadata;
            }
            set
            { 
                if (value == null)
                    m_Metadata = null;
                else
                {
                    m_Metadata = value; 
                }
            }
        }
        protected Mx.Xml.Busdox.wsa.Metadata m_Metadata;
        
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


