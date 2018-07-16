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
 * Using Schema: C:\src\massivex\hyperway\Mx.Certificates.Validator\Xsd\certvalidator.xsd
 **********************************************************************************************/

namespace Mx.Xml.tns
{
    /// <summary>
    /// This class represents the ComplexType ExpirationType
    /// </summary>
    [LiquidTechnologies.Runtime.Standard20.XmlObjectInfo(LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementGroupType.Sequence,
                                                    LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.Element,
                                                    "ExpirationType", "http://difi.no/xsd/certvalidator/1.0", true, false, false)]
    public partial class ExpirationType : Mx.Xml.CertValidator.XmlCommonBase
    {
        #region Constructors
        /// <summary>
        /// Constructor for ExpirationType
        /// </summary>
        /// <remarks>
        /// The class is created with all the mandatory fields populated with the
        /// default data. 
        /// All Collection object are created.
        /// However any 1-n relationships (these are represented as collections) are
        /// empty. To comply with the schema these must be populated before the xml
        /// obtained from ToXml is valid against the schema C:\src\massivex\hyperway\Mx.Certificates.Validator\Xsd\certvalidator.xsd
        /// </remarks>
        public ExpirationType()
        {
            _elementName = "ExpirationType";
            Init();
        }
        public ExpirationType(string elementName)
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
        /// obtained from ToXml is valid against the schema C:\src\massivex\hyperway\Mx.Certificates.Validator\Xsd\certvalidator.xsd.
        /// </remarks>
        protected override void Init()
        {
            Mx.Xml.CertValidator.Registration.iRegistrationIndicator = 0; // causes registration to take place
            m_Millis = null;

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
            Mx.Xml.tns.ExpirationType newObject = new Mx.Xml.tns.ExpirationType(_elementName);
            newObject.m_Millis = m_Millis;

// ##HAND_CODED_BLOCK_START ID="Additional clone"## DO NOT MODIFY ANYTHING OUTSIDE OF THESE TAGS

// Add Additional clone code here...

// ##HAND_CODED_BLOCK_END ID="Additional clone"## DO NOT MODIFY ANYTHING OUTSIDE OF THESE TAGS

            return newObject;
        }
        #endregion

        #region Member variables

        protected override string TargetNamespace
        {
            get { return "http://difi.no/xsd/certvalidator/1.0"; }
        }

        #region Attribute - millis
        /// <summary>
        /// Represents an optional Attribute in the XML document
        /// </summary>
        /// <remarks>
        /// This property is represented as an Attribute in the XML.
        /// It is optional, initially it is not valid.
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.AttributeInfoPrimitive("millis", "", true, LiquidTechnologies.Runtime.Standard20.Conversions.ConversionType.type_i8, null, LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.WhitespaceRule.Collapse, "", -1, -1, "", "", "", "", -1, -1, -1, null)]
        public long? Millis
        {
            get 
            { 
                return m_Millis;  
            }
            set 
            { 
                if (value == null)
                {
                    m_Millis = null;
                }
                else
                {
                    m_Millis = value;
                }
            }
        }
        protected long? m_Millis;
        #endregion

        #region Attribute - Namespace
        public override string Namespace
        {
            get { return "http://difi.no/xsd/certvalidator/1.0"; }
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


