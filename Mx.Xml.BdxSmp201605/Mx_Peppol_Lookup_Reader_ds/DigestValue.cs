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

namespace Mx.Peppol.Lookup.Reader.ds
{
    /// <summary>
    /// This class represents the Element DigestValue
    /// </summary>
    [LiquidTechnologies.Runtime.Standard20.XmlObjectInfo(LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementGroupType.Sequence,
                                                    LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.Element,
                                                    "DigestValue", "http://www.w3.org/2000/09/xmldsig#", true, false, false)]
    public partial class DigestValue : Mx.Peppol.Lookup.Reader.BdxSmp201605.XmlCommonBase
    {
        #region Constructors
        /// <summary>
        /// Constructor for DigestValue
        /// </summary>
        /// <remarks>
        /// The class is created with all the mandatory fields populated with the
        /// default data. 
        /// All Collection object are created.
        /// However any 1-n relationships (these are represented as collections) are
        /// empty. To comply with the schema these must be populated before the xml
        /// obtained from ToXml is valid against the schema C:\src\massivex\hyperway\Mx.Peppol.Lookup\Reader\bdx-smp-201605.xsd
        /// </remarks>
        public DigestValue()
        {
            _elementName = "DigestValue";
            Init();
        }
        public DigestValue(string elementName)
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

            _primitiveValue = LiquidTechnologies.Runtime.Standard20.BinaryData.Empty;
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
            Mx.Peppol.Lookup.Reader.ds.DigestValue newObject = new Mx.Peppol.Lookup.Reader.ds.DigestValue(_elementName);

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

        #region Attribute - PrimitiveValue

        /// <summary>
        /// The InnerText for this element
        /// </summary>
        /// <remarks>
        /// This class represents the element DigestValue, this
        /// element is open, and as such we can place data into it.
        /// i.e. <DigestValue>Some Data</DigestValue>
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.PrimitiveValueInfo(LiquidTechnologies.Runtime.Standard20.Conversions.ConversionType.type_base64Binary, null, LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.WhitespaceRule.Collapse, "", -1, -1, "", "", "", "", -1, -1, -1)]
        public LiquidTechnologies.Runtime.Standard20.BinaryData PrimitiveValue
        {
            get
            {
                return _primitiveValue;
            }
            set
            {
                _primitiveValue = value;
            }
        }
        protected LiquidTechnologies.Runtime.Standard20.BinaryData _primitiveValue;
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


