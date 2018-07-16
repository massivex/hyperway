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
 * Using Schema: C:\src\massivex\hyperway\Mx.Certificates.Validator\Xsd\certvalidator.xsd
 **********************************************************************************************/

namespace Mx.Xml.tns
{
    /// <summary>
    /// This class represents the Element ExtensibleType_Type
    /// </summary>
    [LiquidTechnologies.Runtime.Standard20.XmlObjectInfo(LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementGroupType.Sequence,
                                                    LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.PseudoElement,
                                                    "ExtensibleType_Type", "http://difi.no/xsd/certvalidator/1.0", false, false, false)]
    public partial class ExtensibleType_Type : Mx.Xml.CertValidator.XmlCommonBase
    {
        #region Constructors
        /// <summary>
        /// Constructor for ExtensibleType_Type
        /// </summary>
        /// <remarks>
        /// The class is created with all the mandatory fields populated with the
        /// default data. 
        /// All Collection object are created.
        /// However any 1-n relationships (these are represented as collections) are
        /// empty. To comply with the schema these must be populated before the xml
        /// obtained from ToXml is valid against the schema C:\src\massivex\hyperway\Mx.Certificates.Validator\Xsd\certvalidator.xsd
        /// </remarks>
        public ExtensibleType_Type()
        {
            _elementName = "ExtensibleType_Type";
            Init();
        }
        public ExtensibleType_Type(string elementName)
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
            m_ExtensibleType_Type_Group = new Mx.Xml.CertValidator.XmlObjectCollection<Mx.Xml.tns.ExtensibleType_Type_Group>("ExtensibleType_Type_Group", "http://difi.no/xsd/certvalidator/1.0", 0, -1, true);

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
            Mx.Xml.tns.ExtensibleType_Type newObject = new Mx.Xml.tns.ExtensibleType_Type(_elementName);
            foreach (Mx.Xml.tns.ExtensibleType_Type_Group o in m_ExtensibleType_Type_Group)
                newObject.m_ExtensibleType_Type_Group.Add((Mx.Xml.tns.ExtensibleType_Type_Group)o.Clone());

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

        #region Attribute - ExtensibleType_Type_Group
        /// <summary>
        /// A collection of ExtensibleType_Type_Groups
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// This collection may contain 0 to Many objects.
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoSeqClsCol("ExtensibleType_Type_Group", "http://difi.no/xsd/certvalidator/1.0", LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.PseudoElement)]
        public Mx.Xml.CertValidator.XmlObjectCollection<Mx.Xml.tns.ExtensibleType_Type_Group> ExtensibleType_Type_Group
        {
            get { return m_ExtensibleType_Type_Group; }
        }
        protected Mx.Xml.CertValidator.XmlObjectCollection<Mx.Xml.tns.ExtensibleType_Type_Group> m_ExtensibleType_Type_Group;

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


