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
 * Generation  :  by Liquid XML Data Binder 16.1.9.8572
 * Using Schema: C:\src\massivex\hyperway\Mx.Certificates.Validator\Xsd\certvalidator.xsd
 **********************************************************************************************/

namespace Mx.Xml.tns
{
    /// <summary>
    /// This class represents the ComplexType CachedType
    /// </summary>
    [LiquidTechnologies.Runtime.Standard20.XmlObjectInfo(LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementGroupType.Sequence,
                                                    LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.Element,
                                                    "CachedType", "http://difi.no/xsd/certvalidator/1.0", false, true, false)]
    public partial class CachedType : Mx.Xml.CertValidator.XmlCommonBase
                    , Mx.Xml.tns.IExtensibleType
    {
        #region Constructors
        /// <summary>
        /// Constructor for CachedType
        /// </summary>
        /// <remarks>
        /// The class is created with all the mandatory fields populated with the
        /// default data. 
        /// All Collection object are created.
        /// However any 1-n relationships (these are represented as collections) are
        /// empty. To comply with the schema these must be populated before the xml
        /// obtained from ToXml is valid against the schema C:\src\massivex\hyperway\Mx.Certificates.Validator\Xsd\certvalidator.xsd
        /// </remarks>
        public CachedType()
        {
            _elementName = "CachedType";
            Init();
        }
        public CachedType(string elementName)
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
            m_ExtensibleTypeData = new Mx.Xml.CertValidator.XmlObjectCollection<Mx.Xml.tns.ExtensibleType_Type>("ExtensibleTypeData", "http://difi.no/xsd/certvalidator/1.0", 0, -1, true);
            m_CachedTypeData = new Mx.Xml.tns.CachedType_Type("CachedTypeData");

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
            Mx.Xml.tns.CachedType newObject = new Mx.Xml.tns.CachedType(_elementName);
            foreach (Mx.Xml.tns.ExtensibleType_Type o in m_ExtensibleTypeData)
                newObject.m_ExtensibleTypeData.Add((Mx.Xml.tns.ExtensibleType_Type)o.Clone());
            newObject.m_CachedTypeData = null;
            if (m_CachedTypeData != null)
                newObject.m_CachedTypeData = (Mx.Xml.tns.CachedType_Type)m_CachedTypeData.Clone();

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

        #region Attribute - ExtensibleTypeData
        /// <summary>
        /// Holds all the information contained within the element
        /// </summary>
        /// <remarks>
        /// Because this is a base type, all the objects in it must be contained within a second class. This means all classes supporting this interface can expose use the same interface regardless of how they are extended.
        /// This property is represented as an Element in the XML.
        /// This collection may contain 0 to Many objects.
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoSeqClsCol("ExtensibleTypeData", "http://difi.no/xsd/certvalidator/1.0", LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.PseudoElement)]
        public Mx.Xml.CertValidator.XmlObjectCollection<Mx.Xml.tns.ExtensibleType_Type> ExtensibleTypeData
        {
            get { return m_ExtensibleTypeData; }
        }
        protected Mx.Xml.CertValidator.XmlObjectCollection<Mx.Xml.tns.ExtensibleType_Type> m_ExtensibleTypeData;

        #endregion

        #region Attribute - CachedTypeData
        /// <summary>
        /// Holds all the information contained within the element
        /// </summary>
        /// <remarks>
        /// Because this is a base type, all the objects in it must be contained within a second class. This means all classes supporting this interface can expose use the same interface regardless of how they are extended.
        /// This property is represented as an Element in the XML.
        /// It is mandatory and therefore must be populated within the XML.
        /// If this property is set, then the object will be COPIED. If the property is set to null an exception is raised.
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoSeqClsMnd("CachedTypeData", "http://difi.no/xsd/certvalidator/1.0", LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.PseudoElement, typeof(Mx.Xml.tns.CachedType_Type), false)]
        public Mx.Xml.tns.CachedType_Type CachedTypeData
        {
            get 
            { 
                return m_CachedTypeData;  
            }
            set 
            { 
                Throw_IfPropertyIsNull(value, "CachedTypeData");
                m_CachedTypeData = value;
            }
        }
        protected Mx.Xml.tns.CachedType_Type m_CachedTypeData;
        
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


