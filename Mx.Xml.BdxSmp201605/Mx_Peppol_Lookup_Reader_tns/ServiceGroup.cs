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

namespace Mx.Peppol.Lookup.Reader.tns
{
    /// <summary>
    /// This class represents the Element ServiceGroup
    /// </summary>
    [LiquidTechnologies.Runtime.Standard20.XmlObjectInfo(LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementGroupType.Sequence,
                                                    LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.Element,
                                                    "ServiceGroup", "http://docs.oasis-open.org/bdxr/ns/SMP/2016/05", true, false, false)]
    public partial class ServiceGroup : Mx.Peppol.Lookup.Reader.BdxSmp201605.XmlCommonBase
    {
        #region Constructors
        /// <summary>
        /// Constructor for ServiceGroup
        /// </summary>
        /// <remarks>
        /// The class is created with all the mandatory fields populated with the
        /// default data. 
        /// All Collection object are created.
        /// However any 1-n relationships (these are represented as collections) are
        /// empty. To comply with the schema these must be populated before the xml
        /// obtained from ToXml is valid against the schema C:\src\massivex\hyperway\Mx.Peppol.Lookup\Reader\bdx-smp-201605.xsd
        /// </remarks>
        public ServiceGroup()
        {
            _elementName = "ServiceGroup";
            Init();
        }
        public ServiceGroup(string elementName)
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
            m_ParticipantIdentifier = new Mx.Peppol.Lookup.Reader.tns.ParticipantIdentifier("ParticipantIdentifier");
            m_ServiceMetadataReferenceCollection = new Mx.Peppol.Lookup.Reader.tns.ServiceMetadataReferenceCollectionType("ServiceMetadataReferenceCollection");
            m_Extension = new Mx.Peppol.Lookup.Reader.BdxSmp201605.XmlObjectCollection<Mx.Peppol.Lookup.Reader.tns.ExtensionType>("Extension", "http://docs.oasis-open.org/bdxr/ns/SMP/2016/05", 0, -1, false);

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
            Mx.Peppol.Lookup.Reader.tns.ServiceGroup newObject = new Mx.Peppol.Lookup.Reader.tns.ServiceGroup(_elementName);
            newObject.m_ParticipantIdentifier = null;
            if (m_ParticipantIdentifier != null)
                newObject.m_ParticipantIdentifier = (Mx.Peppol.Lookup.Reader.tns.ParticipantIdentifier)m_ParticipantIdentifier.Clone();
            newObject.m_ServiceMetadataReferenceCollection = null;
            if (m_ServiceMetadataReferenceCollection != null)
                newObject.m_ServiceMetadataReferenceCollection = (Mx.Peppol.Lookup.Reader.tns.ServiceMetadataReferenceCollectionType)m_ServiceMetadataReferenceCollection.Clone();
            foreach (Mx.Peppol.Lookup.Reader.tns.ExtensionType o in m_Extension)
                newObject.m_Extension.Add((Mx.Peppol.Lookup.Reader.tns.ExtensionType)o.Clone());

// ##HAND_CODED_BLOCK_START ID="Additional clone"## DO NOT MODIFY ANYTHING OUTSIDE OF THESE TAGS

// Add Additional clone code here...

// ##HAND_CODED_BLOCK_END ID="Additional clone"## DO NOT MODIFY ANYTHING OUTSIDE OF THESE TAGS

            return newObject;
        }
        #endregion

        #region Member variables

        protected override string TargetNamespace
        {
            get { return "http://docs.oasis-open.org/bdxr/ns/SMP/2016/05"; }
        }

        #region Attribute - ParticipantIdentifier
        /// <summary>
        /// Represents a mandatory Element in the XML document
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is mandatory and therefore must be populated within the XML.
        /// If this property is set, then the object will be COPIED. If the property is set to null an exception is raised.
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoSeqClsMnd("ParticipantIdentifier", "http://docs.oasis-open.org/bdxr/ns/SMP/2016/05", LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.Element, typeof(Mx.Peppol.Lookup.Reader.tns.ParticipantIdentifier), false)]
        public Mx.Peppol.Lookup.Reader.tns.ParticipantIdentifier ParticipantIdentifier
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
        protected Mx.Peppol.Lookup.Reader.tns.ParticipantIdentifier m_ParticipantIdentifier;
        
        #endregion

        #region Attribute - ServiceMetadataReferenceCollection
        /// <summary>
        /// Represents a mandatory Element in the XML document
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is mandatory and therefore must be populated within the XML.
        /// If this property is set, then the object will be COPIED. If the property is set to null an exception is raised.
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoSeqClsMnd("ServiceMetadataReferenceCollection", "http://docs.oasis-open.org/bdxr/ns/SMP/2016/05", LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.Element, typeof(Mx.Peppol.Lookup.Reader.tns.ServiceMetadataReferenceCollectionType), false)]
        public Mx.Peppol.Lookup.Reader.tns.ServiceMetadataReferenceCollectionType ServiceMetadataReferenceCollection
        {
            get 
            { 
                return m_ServiceMetadataReferenceCollection;  
            }
            set 
            { 
                Throw_IfPropertyIsNull(value, "ServiceMetadataReferenceCollection");
                if (value != null)
                    SetElementName(value, "ServiceMetadataReferenceCollection");
                m_ServiceMetadataReferenceCollection = value;
            }
        }
        protected Mx.Peppol.Lookup.Reader.tns.ServiceMetadataReferenceCollectionType m_ServiceMetadataReferenceCollection;
        
        #endregion

        #region Attribute - Extension
        /// <summary>
        /// A collection of Extensions
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// This collection may contain 0 to Many objects.
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoSeqClsCol("Extension", "http://docs.oasis-open.org/bdxr/ns/SMP/2016/05", LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.Element)]
        public Mx.Peppol.Lookup.Reader.BdxSmp201605.XmlObjectCollection<Mx.Peppol.Lookup.Reader.tns.ExtensionType> Extension
        {
            get { return m_Extension; }
        }
        protected Mx.Peppol.Lookup.Reader.BdxSmp201605.XmlObjectCollection<Mx.Peppol.Lookup.Reader.tns.ExtensionType> m_Extension;

        #endregion

        #region Attribute - Namespace
        public override string Namespace
        {
            get { return "http://docs.oasis-open.org/bdxr/ns/SMP/2016/05"; }
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


