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

namespace Mx.Xml.Busdox.tns
{
    /// <summary>
    /// This class represents the ComplexType ServiceInformationType
    /// </summary>
    [LiquidTechnologies.Runtime.Standard20.XmlObjectInfo(LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementGroupType.Sequence,
                                                    LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.Element,
                                                    "ServiceInformationType", "http://busdox.org/serviceMetadata/publishing/1.0/", true, false, false)]
    public partial class ServiceInformationType : Mx.Xml.Busdox.Smp.XmlCommonBase
    {
        #region Constructors
        /// <summary>
        /// Constructor for ServiceInformationType
        /// </summary>
        /// <remarks>
        /// The class is created with all the mandatory fields populated with the
        /// default data. 
        /// All Collection object are created.
        /// However any 1-n relationships (these are represented as collections) are
        /// empty. To comply with the schema these must be populated before the xml
        /// obtained from ToXml is valid against the schema C:\src\massivex\hyperway\Mx.Xml.Busdox.Smp\ServiceMetadataPublishingTypes-1.0.xsd
        /// </remarks>
        public ServiceInformationType()
        {
            _elementName = "ServiceInformationType";
            Init();
        }
        public ServiceInformationType(string elementName)
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
            m_ParticipantIdentifier = new Mx.Xml.Busdox.ids.ParticipantIdentifier("ParticipantIdentifier");
            m_DocumentIdentifier = new Mx.Xml.Busdox.ids.DocumentIdentifier("DocumentIdentifier");
            m_ProcessList = new Mx.Xml.Busdox.tns.ProcessListType("ProcessList");
            m_Extension = null;

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
            Mx.Xml.Busdox.tns.ServiceInformationType newObject = new Mx.Xml.Busdox.tns.ServiceInformationType(_elementName);
            newObject.m_ParticipantIdentifier = null;
            if (m_ParticipantIdentifier != null)
                newObject.m_ParticipantIdentifier = (Mx.Xml.Busdox.ids.ParticipantIdentifier)m_ParticipantIdentifier.Clone();
            newObject.m_DocumentIdentifier = null;
            if (m_DocumentIdentifier != null)
                newObject.m_DocumentIdentifier = (Mx.Xml.Busdox.ids.DocumentIdentifier)m_DocumentIdentifier.Clone();
            newObject.m_ProcessList = null;
            if (m_ProcessList != null)
                newObject.m_ProcessList = (Mx.Xml.Busdox.tns.ProcessListType)m_ProcessList.Clone();
            newObject.m_Extension = null;
            if (m_Extension != null)
                newObject.m_Extension = (Mx.Xml.Busdox.tns.ExtensionType)m_Extension.Clone();

// ##HAND_CODED_BLOCK_START ID="Additional clone"## DO NOT MODIFY ANYTHING OUTSIDE OF THESE TAGS

// Add Additional clone code here...

// ##HAND_CODED_BLOCK_END ID="Additional clone"## DO NOT MODIFY ANYTHING OUTSIDE OF THESE TAGS

            return newObject;
        }
        #endregion

        #region Member variables

        protected override string TargetNamespace
        {
            get { return "http://busdox.org/serviceMetadata/publishing/1.0/"; }
        }

        #region Attribute - ParticipantIdentifier
        /// <summary>
		/// The participant identifier. Comprises the identifier, and an identifier scheme. This identifier MUST have the same value of the {id} part of the URI of the enclosing ServiceMetadata resource.
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is mandatory and therefore must be populated within the XML.
        /// If this property is set, then the object will be COPIED. If the property is set to null an exception is raised.
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoSeqClsMnd("ParticipantIdentifier", "http://busdox.org/transport/identifiers/1.0/", LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.Element, typeof(Mx.Xml.Busdox.ids.ParticipantIdentifier), false)]
        public Mx.Xml.Busdox.ids.ParticipantIdentifier ParticipantIdentifier
        {
            get 
            { 
                return m_ParticipantIdentifier;  
            }
            set 
            { 
                Throw_IfPropertyIsNull(value, "ParticipantIdentifier");
                m_ParticipantIdentifier = value;
            }
        }
        protected Mx.Xml.Busdox.ids.ParticipantIdentifier m_ParticipantIdentifier;
        
        #endregion

        #region Attribute - DocumentIdentifier
        /// <summary>
		/// Represents the type of document that the recipient is able to handle.
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is mandatory and therefore must be populated within the XML.
        /// If this property is set, then the object will be COPIED. If the property is set to null an exception is raised.
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoSeqClsMnd("DocumentIdentifier", "http://busdox.org/transport/identifiers/1.0/", LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.Element, typeof(Mx.Xml.Busdox.ids.DocumentIdentifier), false)]
        public Mx.Xml.Busdox.ids.DocumentIdentifier DocumentIdentifier
        {
            get 
            { 
                return m_DocumentIdentifier;  
            }
            set 
            { 
                Throw_IfPropertyIsNull(value, "DocumentIdentifier");
                m_DocumentIdentifier = value;
            }
        }
        protected Mx.Xml.Busdox.ids.DocumentIdentifier m_DocumentIdentifier;
        
        #endregion

        #region Attribute - ProcessList
        /// <summary>
		/// Represents the processes that a specific document type can participate in, and endpoint address and binding information. Each process element describes a specific business process that accepts this type of document as input and holds a list of endpoint addresses (in the case that the service supports multiple transports) of services that implement the business process, plus information about the transport used for each endpoint.
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is mandatory and therefore must be populated within the XML.
        /// If this property is set, then the object will be COPIED. If the property is set to null an exception is raised.
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoSeqClsMnd("ProcessList", "http://busdox.org/serviceMetadata/publishing/1.0/", LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.Element, typeof(Mx.Xml.Busdox.tns.ProcessListType), true)]
        public Mx.Xml.Busdox.tns.ProcessListType ProcessList
        {
            get 
            { 
                return m_ProcessList;  
            }
            set 
            { 
                Throw_IfPropertyIsNull(value, "ProcessList");
                if (value != null)
                    SetElementName(value, "ProcessList");
                m_ProcessList = value;
            }
        }
        protected Mx.Xml.Busdox.tns.ProcessListType m_ProcessList;
        
        #endregion

        #region Attribute - Extension
        /// <summary>
		/// The extension element may contain any XML element. Clients MAY ignore this element.
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is optional, initially it is null.
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoSeqClsOpt("Extension", "http://busdox.org/serviceMetadata/publishing/1.0/", LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.Element, typeof(Mx.Xml.Busdox.tns.ExtensionType))]
        public Mx.Xml.Busdox.tns.ExtensionType Extension
        {
            get
            { 
                return m_Extension;
            }
            set
            { 
                if (value == null)
                    m_Extension = null;
                else
                {
                    SetElementName(value, "Extension");
                    m_Extension = value; 
                }
            }
        }
        protected Mx.Xml.Busdox.tns.ExtensionType m_Extension;
        
        #endregion

        #region Attribute - Namespace
        public override string Namespace
        {
            get { return "http://busdox.org/serviceMetadata/publishing/1.0/"; }
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


