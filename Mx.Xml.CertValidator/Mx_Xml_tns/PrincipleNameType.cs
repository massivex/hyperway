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
    /// This class represents the ComplexType PrincipleNameType
    /// </summary>
    [LiquidTechnologies.Runtime.Standard20.XmlObjectInfo(LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementGroupType.Choice,
                                                    LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.Element,
                                                    "PrincipleNameType", "http://difi.no/xsd/certvalidator/1.0", true, false, false)]
    public partial class PrincipleNameType : Mx.Xml.CertValidator.XmlCommonBase
    {
        #region Constructors
        /// <summary>
        /// Constructor for PrincipleNameType
        /// </summary>
        /// <remarks>
        /// The class is created with all the mandatory fields populated with the
        /// default data. 
        /// All Collection object are created.
        /// However any 1-n relationships (these are represented as collections) are
        /// empty. To comply with the schema these must be populated before the xml
        /// obtained from ToXml is valid against the schema C:\src\massivex\hyperway\Mx.Certificates.Validator\Xsd\certvalidator.xsd
        /// </remarks>
        public PrincipleNameType()
        {
            _elementName = "PrincipleNameType";
            Init();
        }
        public PrincipleNameType(string elementName)
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
            m_Field = null;
            m_Principal = null;
            m_Value = new Mx.Xml.CertValidator.XmlSimpleTypeCollection<string>("Value", "http://difi.no/xsd/certvalidator/1.0", LiquidTechnologies.Runtime.Standard20.Conversions.ConversionType.type_string, 1, -1, null, LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.WhitespaceRule.Preserve, new LiquidTechnologies.Runtime.Standard20.PrimitiveRestrictions("", -1, -1, "", "", "", "", -1, -1, -1));
            m_Value.OnCollectionChange += new Mx.Xml.CertValidator.XmlSimpleTypeCollection<string>.OnCollectionChangeEvent(this.Value_OnChange);
            m_Reference = null;

            _validElement = "";
// ##HAND_CODED_BLOCK_START ID="Additional Inits"## DO NOT MODIFY ANYTHING OUTSIDE OF THESE TAGS

// Add Additional initialization code here...

// ##HAND_CODED_BLOCK_END ID="Additional Inits"## DO NOT MODIFY ANYTHING OUTSIDE OF THESE TAGS
        }
        protected void ClearChoice(string selectedElement)
        {
            if (m_Value != null)
            {
                if ("Value" != selectedElement)
                    m_Value.Clear();
            }
            m_Reference = null;
            _validElement = selectedElement;
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
            Mx.Xml.tns.PrincipleNameType newObject = new Mx.Xml.tns.PrincipleNameType(_elementName);
            newObject.m_Field = m_Field;
            newObject.m_Principal = m_Principal;
            foreach (string o in m_Value)
                newObject.m_Value.Add(o);
            newObject.m_Reference = null;
            if (m_Reference != null)
                newObject.m_Reference = (Mx.Xml.tns.IReferenceType)m_Reference.Clone();

            newObject._validElement = _validElement;
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

        #region Attribute - field
        /// <summary>
        /// Represents an optional Attribute in the XML document
        /// </summary>
        /// <remarks>
        /// This property is represented as an Attribute in the XML.
        /// It is optional, initially it is not valid.
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.AttributeInfoPrimitive("field", "", true, LiquidTechnologies.Runtime.Standard20.Conversions.ConversionType.type_string, null, LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.WhitespaceRule.Preserve, "", -1, -1, "", "", "", "", -1, -1, -1, null)]
        public string Field
        {
            get 
            { 
                return m_Field;  
            }
            set 
            { 
                if (value == null)
                {
                    m_Field = null;
                }
                else
                {
                    // Apply whitespace rules appropriately
                    value = LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.Preserve(value); 
                    m_Field = value;
                }
            }
        }
        protected string m_Field;
        #endregion

        #region Attribute - principal
        /// <summary>
        /// Represents an optional Attribute in the XML document
        /// </summary>
        /// <remarks>
        /// This property is represented as an Attribute in the XML.
        /// It is optional, initially it is not valid.
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.AttributeInfoEnum("principal", "", true, typeof(Mx.Xml.tns.Enumerations), "PrincipalEnumFromString", "PrincipalEnumToString", null)]
        public Mx.Xml.tns.Enumerations.PrincipalEnum? Principal
        {
            get
            {
                return m_Principal;
            }
            set
            {
                m_Principal = value;
            }
        }
        protected Mx.Xml.tns.Enumerations.PrincipalEnum? m_Principal;

        #endregion

        #region Attribute - Value
        /// <summary>
        /// A collection of strings
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// This collection may contain 1 to Many strings.
        /// Only one Element within this class may be set at a time. This collection (as a whole is counted as an element) may contain 0 or 1 to Many entries. Emptying the collection allows a different element to be the selected one. If there is already a selected item, and an item is added to this collection then an exception will be raised
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoChoicePrimCol("Value", "http://difi.no/xsd/certvalidator/1.0")]
        public Mx.Xml.CertValidator.XmlSimpleTypeCollection<string> Value
        {
            get { return m_Value; }
        }
        // gets called when the collection changes (allows us to determine if this choice is selected or not)
        private void Value_OnChange(object o, EventArgs args)
        {
            // The class represents a choice, so prevent more than one element from being selected
            if (_validElement != "Value")
            {
                ClearChoice(m_Value.Count == 0?"":"Value"); // remove selection
            }
        }
        protected Mx.Xml.CertValidator.XmlSimpleTypeCollection<string> m_Value;

        #endregion

        #region Attribute - Reference
        /// <summary>
        /// Represents an optional Element in the XML document
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is optional, initially it is null.
        /// Only one Element within this class may be set at a time, setting this property when another element is already set will raise an exception. setting this property to null will allow another element to be selected
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoChoiceAbsClsOpt("Reference", "http://difi.no/xsd/certvalidator/1.0", LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.Element, typeof(Mx.Xml.tns.ClassFactory), "IReferenceTypeCreateObject")]
        public Mx.Xml.tns.IReferenceType Reference
        {
            get
            {
                return m_Reference;
            }
            set
            {
                // The class represents a choice, so prevent more than one element from being selected
                ClearChoice((value == null)?"":"Reference"); // remove selection
                if (value == null)
                    m_Reference = null;
                else
                {
                    m_Reference = value; 
                    // The object being set needs to take the element name from the class (the type="" attribute will then be set in the XML)
                    SetElementName(value.GetBase(), "Reference");
                }
            }
        }
        protected Mx.Xml.tns.IReferenceType m_Reference;

        #endregion

        public string ChoiceSelectedElement
        {
            get { return _validElement;  }
        }
        protected string _validElement;
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


