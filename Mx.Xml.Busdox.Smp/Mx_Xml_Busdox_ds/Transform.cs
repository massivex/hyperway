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
    /// This class represents the Element Transform
    /// </summary>
    [LiquidTechnologies.Runtime.Standard20.XmlObjectInfo(LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementGroupType.Sequence,
                                                    LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.Element,
                                                    "Transform", "http://www.w3.org/2000/09/xmldsig#", true, false, false)]
    public partial class Transform : Mx.Xml.Busdox.Smp.XmlCommonBase
    {
        #region Constructors
        /// <summary>
        /// Constructor for Transform
        /// </summary>
        /// <remarks>
        /// The class is created with all the mandatory fields populated with the
        /// default data. 
        /// All Collection object are created.
        /// However any 1-n relationships (these are represented as collections) are
        /// empty. To comply with the schema these must be populated before the xml
        /// obtained from ToXml is valid against the schema C:\src\massivex\hyperway\Mx.Xml.Busdox.Smp\ServiceMetadataPublishingTypes-1.0.xsd
        /// </remarks>
        public Transform()
        {
            _elementName = "Transform";
            Init();
        }
        public Transform(string elementName)
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
            m_Algorithm = "";
            m_Transform_Group = new Mx.Xml.Busdox.Smp.XmlObjectCollection<Mx.Xml.Busdox.ds.Transform_Group>("Transform_Group", "http://www.w3.org/2000/09/xmldsig#", 0, -1, true);

            _primitiveValue = "";
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
            Mx.Xml.Busdox.ds.Transform newObject = new Mx.Xml.Busdox.ds.Transform(_elementName);
            newObject.m_Algorithm = m_Algorithm;
            foreach (Mx.Xml.Busdox.ds.Transform_Group o in m_Transform_Group)
                newObject.m_Transform_Group.Add((Mx.Xml.Busdox.ds.Transform_Group)o.Clone());

            newObject._primitiveValue = _primitiveValue;
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

        #region Attribute - Algorithm
        /// <summary>
        /// Represents a mandatory Attribute in the XML document
        /// </summary>
        /// <remarks>
        /// This property is represented as an Attribute in the XML.
        /// It is mandatory and therefore must be populated within the XML.
        /// It is defaulted to "".
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.AttributeInfoPrimitive("Algorithm", "", LiquidTechnologies.Runtime.Standard20.Conversions.ConversionType.type_string, null, LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.WhitespaceRule.Collapse, "", -1, -1, "", "", "", "", -1, -1, -1, null)]
        public string Algorithm
        {
            get
            {
                return m_Algorithm;
            }
            set 
            {
                // Apply whitespace rules appropriately
                value = LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.Collapse(value);
                m_Algorithm = value;
            }
        }
        protected string m_Algorithm;

        #endregion

        #region Attribute - Transform_Group
        /// <summary>
        /// A collection of Transform_Groups
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// This collection may contain 0 to Many objects.
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoSeqClsCol("Transform_Group", "http://www.w3.org/2000/09/xmldsig#", LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.PseudoElement)]
        public Mx.Xml.Busdox.Smp.XmlObjectCollection<Mx.Xml.Busdox.ds.Transform_Group> Transform_Group
        {
            get { return m_Transform_Group; }
        }
        protected Mx.Xml.Busdox.Smp.XmlObjectCollection<Mx.Xml.Busdox.ds.Transform_Group> m_Transform_Group;

        #endregion

        #region Attribute - PrimitiveValue

        /// <summary>
        /// The InnerText for this element
        /// </summary>
        /// <remarks>
        /// This class represents the element Transform, this
        /// element is open, and as such we can place data into it.
        /// i.e. <Transform>Some Data</Transform>
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.PrimitiveValueInfo(LiquidTechnologies.Runtime.Standard20.Conversions.ConversionType.type_string, null, LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.WhitespaceRule.Preserve, "", -1, -1, "", "", "", "", -1, -1, -1)]
        public string PrimitiveValue
        {
            get
            {
                return _primitiveValue;
            }
            set
            {
                // Apply whitespace rules appropriately
                value = LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.Preserve(value);
                _primitiveValue = value;
            }
        }
        protected string _primitiveValue;
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


