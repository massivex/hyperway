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
    /// This class represents the ComplexType RedirectType
    /// </summary>
    [LiquidTechnologies.Runtime.Standard20.XmlObjectInfo(LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementGroupType.Sequence,
                                                    LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.Element,
                                                    "RedirectType", "http://busdox.org/serviceMetadata/publishing/1.0/", true, false, false)]
    public partial class RedirectType : Mx.Xml.Busdox.Smp.XmlCommonBase
    {
        #region Constructors
        /// <summary>
        /// Constructor for RedirectType
        /// </summary>
        /// <remarks>
        /// The class is created with all the mandatory fields populated with the
        /// default data. 
        /// All Collection object are created.
        /// However any 1-n relationships (these are represented as collections) are
        /// empty. To comply with the schema these must be populated before the xml
        /// obtained from ToXml is valid against the schema C:\src\massivex\hyperway\Mx.Xml.Busdox.Smp\ServiceMetadataPublishingTypes-1.0.xsd
        /// </remarks>
        public RedirectType()
        {
            _elementName = "RedirectType";
            Init();
        }
        public RedirectType(string elementName)
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
            m_Href = null;
            m_CertificateUID = "";
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
            Mx.Xml.Busdox.tns.RedirectType newObject = new Mx.Xml.Busdox.tns.RedirectType(_elementName);
            newObject.m_Href = m_Href;
            newObject.m_CertificateUID = m_CertificateUID;
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

        #region Attribute - href
        /// <summary>
		/// The destination URL of the redirect.
        /// </summary>
        /// <remarks>
        /// This property is represented as an Attribute in the XML.
        /// It is optional, initially it is not valid.
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.AttributeInfoPrimitive("href", "", true, LiquidTechnologies.Runtime.Standard20.Conversions.ConversionType.type_string, null, LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.WhitespaceRule.Collapse, "", -1, -1, "", "", "", "", -1, -1, -1, null)]
        public string Href
        {
            get 
            { 
                return m_Href;  
            }
            set 
            { 
                if (value == null)
                {
                    m_Href = null;
                }
                else
                {
                    // Apply whitespace rules appropriately
                    value = LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.Collapse(value); 
                    m_Href = value;
                }
            }
        }
        protected string m_Href;
        #endregion

        #region Attribute - CertificateUID
        /// <summary>
		/// Holds the Subject Unique Identifier of the certificate of the destination SMP. A client SHOULD validate that the Subject Unique Identifier of the certificate used to sign the resource at the destination SMP matches the Subject Unique Identifier published in the redirecting SMP.
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is mandatory and therefore must be populated within the XML.
        /// It is defaulted to "".
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoSeqPrimMnd("CertificateUID", "http://busdox.org/serviceMetadata/publishing/1.0/", null, LiquidTechnologies.Runtime.Standard20.Conversions.ConversionType.type_string, null, LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.WhitespaceRule.Preserve, "", -1, -1, "", "", "", "", -1, -1, -1, null)]
        public string CertificateUID
        {
            get
            {
                return m_CertificateUID;
            }
            set 
            {
                // Apply whitespace rules appropriately
                value = LiquidTechnologies.Runtime.Standard20.WhitespaceUtils.Preserve(value);
                m_CertificateUID = value;
            }
        }
        protected string m_CertificateUID;

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


