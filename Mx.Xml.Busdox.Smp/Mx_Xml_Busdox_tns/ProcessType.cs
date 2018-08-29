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
    /// This class represents the ComplexType ProcessType
    /// </summary>
    [LiquidTechnologies.Runtime.Standard20.XmlObjectInfo(LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementGroupType.Sequence,
                                                    LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.Element,
                                                    "ProcessType", "http://busdox.org/serviceMetadata/publishing/1.0/", true, false, false)]
    public partial class ProcessType : Mx.Xml.Busdox.Smp.XmlCommonBase
    {
        #region Constructors
        /// <summary>
        /// Constructor for ProcessType
        /// </summary>
        /// <remarks>
        /// The class is created with all the mandatory fields populated with the
        /// default data. 
        /// All Collection object are created.
        /// However any 1-n relationships (these are represented as collections) are
        /// empty. To comply with the schema these must be populated before the xml
        /// obtained from ToXml is valid against the schema C:\src\massivex\hyperway\Mx.Xml.Busdox.Smp\ServiceMetadataPublishingTypes-1.0.xsd
        /// </remarks>
        public ProcessType()
        {
            _elementName = "ProcessType";
            Init();
        }
        public ProcessType(string elementName)
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
            m_ProcessIdentifier = new Mx.Xml.Busdox.ids.ProcessIdentifier("ProcessIdentifier");
            m_ServiceEndpointList = new Mx.Xml.Busdox.tns.ServiceEndpointList("ServiceEndpointList");
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
            Mx.Xml.Busdox.tns.ProcessType newObject = new Mx.Xml.Busdox.tns.ProcessType(_elementName);
            newObject.m_ProcessIdentifier = null;
            if (m_ProcessIdentifier != null)
                newObject.m_ProcessIdentifier = (Mx.Xml.Busdox.ids.ProcessIdentifier)m_ProcessIdentifier.Clone();
            newObject.m_ServiceEndpointList = null;
            if (m_ServiceEndpointList != null)
                newObject.m_ServiceEndpointList = (Mx.Xml.Busdox.tns.ServiceEndpointList)m_ServiceEndpointList.Clone();
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

        #region Attribute - ProcessIdentifier
        /// <summary>
		/// The identifier of the process.
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is mandatory and therefore must be populated within the XML.
        /// If this property is set, then the object will be COPIED. If the property is set to null an exception is raised.
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoSeqClsMnd("ProcessIdentifier", "http://busdox.org/transport/identifiers/1.0/", LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.Element, typeof(Mx.Xml.Busdox.ids.ProcessIdentifier), false)]
        public Mx.Xml.Busdox.ids.ProcessIdentifier ProcessIdentifier
        {
            get 
            { 
                return m_ProcessIdentifier;  
            }
            set 
            { 
                Throw_IfPropertyIsNull(value, "ProcessIdentifier");
                m_ProcessIdentifier = value;
            }
        }
        protected Mx.Xml.Busdox.ids.ProcessIdentifier m_ProcessIdentifier;
        
        #endregion

        #region Attribute - ServiceEndpointList
        /// <summary>
		/// List of one or more endpoints that support this process.
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is mandatory and therefore must be populated within the XML.
        /// If this property is set, then the object will be COPIED. If the property is set to null an exception is raised.
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoSeqClsMnd("ServiceEndpointList", "http://busdox.org/serviceMetadata/publishing/1.0/", LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.Element, typeof(Mx.Xml.Busdox.tns.ServiceEndpointList), true)]
        public Mx.Xml.Busdox.tns.ServiceEndpointList ServiceEndpointList
        {
            get 
            { 
                return m_ServiceEndpointList;  
            }
            set 
            { 
                Throw_IfPropertyIsNull(value, "ServiceEndpointList");
                if (value != null)
                    SetElementName(value, "ServiceEndpointList");
                m_ServiceEndpointList = value;
            }
        }
        protected Mx.Xml.Busdox.tns.ServiceEndpointList m_ServiceEndpointList;
        
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


