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
    /// This class represents the Element ServiceMetadata
    /// </summary>
    [LiquidTechnologies.Runtime.Standard20.XmlObjectInfo(LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementGroupType.Choice,
                                                    LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.Element,
                                                    "ServiceMetadata", "http://busdox.org/serviceMetadata/publishing/1.0/", true, false, false)]
    public partial class ServiceMetadata : Mx.Xml.Busdox.Smp.XmlCommonBase
    {
        #region Constructors
        /// <summary>
        /// Constructor for ServiceMetadata
        /// </summary>
        /// <remarks>
        /// The class is created with all the mandatory fields populated with the
        /// default data. 
        /// All Collection object are created.
        /// However any 1-n relationships (these are represented as collections) are
        /// empty. To comply with the schema these must be populated before the xml
        /// obtained from ToXml is valid against the schema C:\src\massivex\hyperway\Mx.Xml.Busdox.Smp\ServiceMetadataPublishingTypes-1.0.xsd
        /// </remarks>
        public ServiceMetadata()
        {
            _elementName = "ServiceMetadata";
            Init();
        }
        public ServiceMetadata(string elementName)
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
            m_ServiceInformation = null;
            m_Redirect = null;

            _validElement = "";
// ##HAND_CODED_BLOCK_START ID="Additional Inits"## DO NOT MODIFY ANYTHING OUTSIDE OF THESE TAGS

// Add Additional initialization code here...

// ##HAND_CODED_BLOCK_END ID="Additional Inits"## DO NOT MODIFY ANYTHING OUTSIDE OF THESE TAGS
        }
        protected void ClearChoice(string selectedElement)
        {
            m_ServiceInformation = null;
            m_Redirect = null;
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
            Mx.Xml.Busdox.tns.ServiceMetadata newObject = new Mx.Xml.Busdox.tns.ServiceMetadata(_elementName);
            newObject.m_ServiceInformation = null;
            if (m_ServiceInformation != null)
                newObject.m_ServiceInformation = (Mx.Xml.Busdox.tns.ServiceInformationType)m_ServiceInformation.Clone();
            newObject.m_Redirect = null;
            if (m_Redirect != null)
                newObject.m_Redirect = (Mx.Xml.Busdox.tns.RedirectType)m_Redirect.Clone();

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
            get { return "http://busdox.org/serviceMetadata/publishing/1.0/"; }
        }

        #region Attribute - ServiceInformation
        /// <summary>
		/// Contains service information for an actual service registration, rather than a redirect to another SMP
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is optional, initially it is null.
        /// Only one Element within this class may be set at a time, setting this property when another element is already set will raise an exception. setting this property to null will allow another element to be selected
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoChoiceClsOpt("ServiceInformation", "http://busdox.org/serviceMetadata/publishing/1.0/", LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.Element, typeof(Mx.Xml.Busdox.tns.ServiceInformationType))]
        public Mx.Xml.Busdox.tns.ServiceInformationType ServiceInformation
        {
            get
            { 
                return m_ServiceInformation;
            }
            set
            { 
                // The class represents a choice, so prevent more than one element from being selected
                ClearChoice((value == null)?"":"ServiceInformation"); // remove selection
                if (value == null)
                    m_ServiceInformation = null;
                else
                {
                    SetElementName(value, "ServiceInformation");
                    m_ServiceInformation = value; 
                }
            }
        }
        protected Mx.Xml.Busdox.tns.ServiceInformationType m_ServiceInformation;
        
        #endregion

        #region Attribute - Redirect
        /// <summary>
		/// For recipients that want to associate more than one SMP with their participant identifier, 
		/// they may redirect senders to an alternative SMP for specific document types. To achieve 
		/// this, the ServiceMetadata element defines the optional element ‘Redirect’. This element 
		/// holds the URL of the alternative SMP, as well as the Subject Unique Identifier of the 
		/// destination SMPs certificate used to sign its resources.
		/// In the case where a client encounters such a redirection element, the client MUST follow 
		/// the first redirect reference to the alternative SMP. If the SignedServiceMetadata resource 
		/// at the alternative SMP also contains a redirection element, the client SHOULD NOT follow 
		/// that redirect. It is the responsibility of the client to enforce this constraint.
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is optional, initially it is null.
        /// Only one Element within this class may be set at a time, setting this property when another element is already set will raise an exception. setting this property to null will allow another element to be selected
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoChoiceClsOpt("Redirect", "http://busdox.org/serviceMetadata/publishing/1.0/", LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.Element, typeof(Mx.Xml.Busdox.tns.RedirectType))]
        public Mx.Xml.Busdox.tns.RedirectType Redirect
        {
            get
            { 
                return m_Redirect;
            }
            set
            { 
                // The class represents a choice, so prevent more than one element from being selected
                ClearChoice((value == null)?"":"Redirect"); // remove selection
                if (value == null)
                    m_Redirect = null;
                else
                {
                    SetElementName(value, "Redirect");
                    m_Redirect = value; 
                }
            }
        }
        protected Mx.Xml.Busdox.tns.RedirectType m_Redirect;
        
        #endregion

        public string ChoiceSelectedElement
        {
            get { return _validElement;  }
        }
        protected string _validElement;
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


