﻿using System;
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
    /// This class represents the Element SignedServiceMetadata
    /// </summary>
    [LiquidTechnologies.Runtime.Standard20.XmlObjectInfo(LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementGroupType.Sequence,
                                                    LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.Element,
                                                    "SignedServiceMetadata", "http://docs.oasis-open.org/bdxr/ns/SMP/2016/05", true, false, false)]
    public partial class SignedServiceMetadata : Mx.Peppol.Lookup.Reader.BdxSmp201605.XmlCommonBase
    {
        #region Constructors
        /// <summary>
        /// Constructor for SignedServiceMetadata
        /// </summary>
        /// <remarks>
        /// The class is created with all the mandatory fields populated with the
        /// default data. 
        /// All Collection object are created.
        /// However any 1-n relationships (these are represented as collections) are
        /// empty. To comply with the schema these must be populated before the xml
        /// obtained from ToXml is valid against the schema C:\src\massivex\hyperway\Mx.Peppol.Lookup\Reader\bdx-smp-201605.xsd
        /// </remarks>
        public SignedServiceMetadata()
        {
            _elementName = "SignedServiceMetadata";
            Init();
        }
        public SignedServiceMetadata(string elementName)
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
            m_ServiceMetadata = new Mx.Peppol.Lookup.Reader.tns.ServiceMetadata("ServiceMetadata");
            m_Signature = new Mx.Peppol.Lookup.Reader.ds.Signature("Signature");

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
            Mx.Peppol.Lookup.Reader.tns.SignedServiceMetadata newObject = new Mx.Peppol.Lookup.Reader.tns.SignedServiceMetadata(_elementName);
            newObject.m_ServiceMetadata = null;
            if (m_ServiceMetadata != null)
                newObject.m_ServiceMetadata = (Mx.Peppol.Lookup.Reader.tns.ServiceMetadata)m_ServiceMetadata.Clone();
            newObject.m_Signature = null;
            if (m_Signature != null)
                newObject.m_Signature = (Mx.Peppol.Lookup.Reader.ds.Signature)m_Signature.Clone();

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

        #region Attribute - ServiceMetadata
        /// <summary>
        /// Represents a mandatory Element in the XML document
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is mandatory and therefore must be populated within the XML.
        /// If this property is set, then the object will be COPIED. If the property is set to null an exception is raised.
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoSeqClsMnd("ServiceMetadata", "http://docs.oasis-open.org/bdxr/ns/SMP/2016/05", LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.Element, typeof(Mx.Peppol.Lookup.Reader.tns.ServiceMetadata), false)]
        public Mx.Peppol.Lookup.Reader.tns.ServiceMetadata ServiceMetadata
        {
            get 
            { 
                return m_ServiceMetadata;  
            }
            set 
            { 
                Throw_IfPropertyIsNull(value, "ServiceMetadata");
                m_ServiceMetadata = value;
            }
        }
        protected Mx.Peppol.Lookup.Reader.tns.ServiceMetadata m_ServiceMetadata;
        
        #endregion

        #region Attribute - Signature
        /// <summary>
        /// Represents a mandatory Element in the XML document
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is mandatory and therefore must be populated within the XML.
        /// If this property is set, then the object will be COPIED. If the property is set to null an exception is raised.
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoSeqClsMnd("Signature", "http://www.w3.org/2000/09/xmldsig#", LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.Element, typeof(Mx.Peppol.Lookup.Reader.ds.Signature), true)]
        public Mx.Peppol.Lookup.Reader.ds.Signature Signature
        {
            get 
            { 
                return m_Signature;  
            }
            set 
            { 
                Throw_IfPropertyIsNull(value, "Signature");
                m_Signature = value;
            }
        }
        protected Mx.Peppol.Lookup.Reader.ds.Signature m_Signature;
        
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


