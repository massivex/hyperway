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
 * Generation  :  by Liquid XML Data Binder 16.1.11.8608
 * Using Schema: C:\src\massivex\hyperway\Mx.Xml.Busdox.Smp\ServiceMetadataPublishingTypes-1.0.xsd
 **********************************************************************************************/

namespace Mx.Xml.Busdox.ds
{
    /// <summary>
    /// This class represents the Element SPKIData
    /// </summary>
    [LiquidTechnologies.Runtime.Standard20.XmlObjectInfo(LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementGroupType.Sequence,
                                                    LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.Element,
                                                    "SPKIData", "http://www.w3.org/2000/09/xmldsig#", true, false, false)]
    public partial class SPKIData : Mx.Xml.Busdox.Smp.XmlCommonBase
    {
        #region Constructors
        /// <summary>
        /// Constructor for SPKIData
        /// </summary>
        /// <remarks>
        /// The class is created with all the mandatory fields populated with the
        /// default data. 
        /// All Collection object are created.
        /// However any 1-n relationships (these are represented as collections) are
        /// empty. To comply with the schema these must be populated before the xml
        /// obtained from ToXml is valid against the schema C:\src\massivex\hyperway\Mx.Xml.Busdox.Smp\ServiceMetadataPublishingTypes-1.0.xsd
        /// </remarks>
        public SPKIData()
        {
            _elementName = "SPKIData";
            Init();
        }
        public SPKIData(string elementName)
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
            m_SPKIData_Group = new Mx.Xml.Busdox.Smp.XmlObjectCollection<Mx.Xml.Busdox.ds.SPKIData_Group>("SPKIData_Group", "http://www.w3.org/2000/09/xmldsig#", 1, -1, true);

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
            Mx.Xml.Busdox.ds.SPKIData newObject = new Mx.Xml.Busdox.ds.SPKIData(_elementName);
            foreach (Mx.Xml.Busdox.ds.SPKIData_Group o in m_SPKIData_Group)
                newObject.m_SPKIData_Group.Add((Mx.Xml.Busdox.ds.SPKIData_Group)o.Clone());

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

        #region Attribute - SPKIData_Group
        /// <summary>
        /// A collection of SPKIData_Groups
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// This collection may contain 1 to Many objects.
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoSeqClsCol("SPKIData_Group", "http://www.w3.org/2000/09/xmldsig#", LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.PseudoElement)]
        public Mx.Xml.Busdox.Smp.XmlObjectCollection<Mx.Xml.Busdox.ds.SPKIData_Group> SPKIData_Group
        {
            get { return m_SPKIData_Group; }
        }
        protected Mx.Xml.Busdox.Smp.XmlObjectCollection<Mx.Xml.Busdox.ds.SPKIData_Group> m_SPKIData_Group;

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

