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

namespace Mx.Xml.Busdox.ds
{
    /// <summary>
    /// This class represents the Element PGPData
    /// </summary>
    [LiquidTechnologies.Runtime.Standard20.XmlObjectInfo(LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementGroupType.Choice,
                                                    LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.Element,
                                                    "PGPData", "http://www.w3.org/2000/09/xmldsig#", true, false, false)]
    public partial class PGPData : Mx.Xml.Busdox.Smp.XmlCommonBase
    {
        #region Constructors
        /// <summary>
        /// Constructor for PGPData
        /// </summary>
        /// <remarks>
        /// The class is created with all the mandatory fields populated with the
        /// default data. 
        /// All Collection object are created.
        /// However any 1-n relationships (these are represented as collections) are
        /// empty. To comply with the schema these must be populated before the xml
        /// obtained from ToXml is valid against the schema C:\src\massivex\hyperway\Mx.Xml.Busdox.Smp\ServiceMetadataPublishingTypes-1.0.xsd
        /// </remarks>
        public PGPData()
        {
            _elementName = "PGPData";
            Init();
        }
        public PGPData(string elementName)
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
            m_PGPData_Seq = null;
            m_PGPData_SeqA = null;

            _validElement = "";
// ##HAND_CODED_BLOCK_START ID="Additional Inits"## DO NOT MODIFY ANYTHING OUTSIDE OF THESE TAGS

// Add Additional initialization code here...

// ##HAND_CODED_BLOCK_END ID="Additional Inits"## DO NOT MODIFY ANYTHING OUTSIDE OF THESE TAGS
        }
        protected void ClearChoice(string selectedElement)
        {
            m_PGPData_Seq = null;
            m_PGPData_SeqA = null;
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
            Mx.Xml.Busdox.ds.PGPData newObject = new Mx.Xml.Busdox.ds.PGPData(_elementName);
            newObject.m_PGPData_Seq = null;
            if (m_PGPData_Seq != null)
                newObject.m_PGPData_Seq = (Mx.Xml.Busdox.ds.PGPData_Seq)m_PGPData_Seq.Clone();
            newObject.m_PGPData_SeqA = null;
            if (m_PGPData_SeqA != null)
                newObject.m_PGPData_SeqA = (Mx.Xml.Busdox.ds.PGPData_SeqA)m_PGPData_SeqA.Clone();

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
            get { return "http://www.w3.org/2000/09/xmldsig#"; }
        }

        #region Attribute - PGPData_Seq
        /// <summary>
        /// Represents an optional Element in the XML document
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is optional, initially it is null.
        /// Only one Element within this class may be set at a time, setting this property when another element is already set will raise an exception. setting this property to null will allow another element to be selected
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoChoiceClsOpt("PGPData_Seq", "", LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.PseudoElement, typeof(Mx.Xml.Busdox.ds.PGPData_Seq))]
        public Mx.Xml.Busdox.ds.PGPData_Seq PGPData_Seq
        {
            get
            { 
                return m_PGPData_Seq;
            }
            set
            { 
                // The class represents a choice, so prevent more than one element from being selected
                ClearChoice((value == null)?"":"PGPData_Seq"); // remove selection
                if (value == null)
                    m_PGPData_Seq = null;
                else
                {
                    m_PGPData_Seq = value; 
                }
            }
        }
        protected Mx.Xml.Busdox.ds.PGPData_Seq m_PGPData_Seq;
        
        #endregion

        #region Attribute - PGPData_SeqA
        /// <summary>
        /// Represents an optional Element in the XML document
        /// </summary>
        /// <remarks>
        /// This property is represented as an Element in the XML.
        /// It is optional, initially it is null.
        /// Only one Element within this class may be set at a time, setting this property when another element is already set will raise an exception. setting this property to null will allow another element to be selected
        /// </remarks>
        [LiquidTechnologies.Runtime.Standard20.ElementInfoChoiceClsOpt("PGPData_SeqA", "", LiquidTechnologies.Runtime.Standard20.XmlObjectBase.XmlElementType.PseudoElement, typeof(Mx.Xml.Busdox.ds.PGPData_SeqA))]
        public Mx.Xml.Busdox.ds.PGPData_SeqA PGPData_SeqA
        {
            get
            { 
                return m_PGPData_SeqA;
            }
            set
            { 
                // The class represents a choice, so prevent more than one element from being selected
                ClearChoice((value == null)?"":"PGPData_SeqA"); // remove selection
                if (value == null)
                    m_PGPData_SeqA = null;
                else
                {
                    m_PGPData_SeqA = value; 
                }
            }
        }
        protected Mx.Xml.Busdox.ds.PGPData_SeqA m_PGPData_SeqA;
        
        #endregion

        public string ChoiceSelectedElement
        {
            get { return _validElement;  }
        }
        protected string _validElement;
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


