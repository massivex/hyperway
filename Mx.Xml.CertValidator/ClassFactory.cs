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

namespace Mx.Xml.CertValidator
{
    public static class ClassFactory
    {
        #region Static Constructor
        private static System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, System.Type>> _nsMap = null;
        static ClassFactory()
        {
            _nsMap = new System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, System.Type>>();
            System.Collections.Generic.Dictionary<string, System.Type> itemMap = null;
            itemMap = new System.Collections.Generic.Dictionary<string, System.Type>();
            itemMap.Add("CachedType", typeof(Mx.Xml.tns.CachedType));
            itemMap.Add("CachedType_Type", typeof(Mx.Xml.tns.CachedType_Type));
            itemMap.Add("CertificateBucketType", typeof(Mx.Xml.tns.CertificateBucketType));
            itemMap.Add("CertificateBucketType_Group", typeof(Mx.Xml.tns.CertificateBucketType_Group));
            itemMap.Add("CertificateReferenceType", typeof(Mx.Xml.tns.CertificateReferenceType));
            itemMap.Add("CertificateStartsWithType", typeof(Mx.Xml.tns.CertificateStartsWithType));
            itemMap.Add("ChainType", typeof(Mx.Xml.tns.ChainType));
            itemMap.Add("CriticalExtensionRecognizedType", typeof(Mx.Xml.tns.CriticalExtensionRecognizedType));
            itemMap.Add("CriticalExtensionRequiredType", typeof(Mx.Xml.tns.CriticalExtensionRequiredType));
            itemMap.Add("CRLType", typeof(Mx.Xml.tns.CRLType));
            itemMap.Add("ExpirationType", typeof(Mx.Xml.tns.ExpirationType));
            itemMap.Add("ExtensibleType", typeof(Mx.Xml.tns.ExtensibleType));
            itemMap.Add("ExtensibleType_Type", typeof(Mx.Xml.tns.ExtensibleType_Type));
            itemMap.Add("ExtensibleType_Type_Group", typeof(Mx.Xml.tns.ExtensibleType_Type_Group));
            itemMap.Add("HandleErrorType", typeof(Mx.Xml.tns.HandleErrorType));
            itemMap.Add("HandleErrorType_Type", typeof(Mx.Xml.tns.HandleErrorType_Type));
            itemMap.Add("JunctionType", typeof(Mx.Xml.tns.JunctionType));
            itemMap.Add("JunctionType_Type", typeof(Mx.Xml.tns.JunctionType_Type));
            itemMap.Add("KeyStoreType", typeof(Mx.Xml.tns.KeyStoreType));
            itemMap.Add("KeyUsageType", typeof(Mx.Xml.tns.KeyUsageType));
            itemMap.Add("OCSPType", typeof(Mx.Xml.tns.OCSPType));
            itemMap.Add("PrincipleNameType", typeof(Mx.Xml.tns.PrincipleNameType));
            itemMap.Add("ReferenceType", typeof(Mx.Xml.tns.ReferenceType));
            itemMap.Add("RuleReferenceType", typeof(Mx.Xml.tns.RuleReferenceType));
            itemMap.Add("SigningType", typeof(Mx.Xml.tns.SigningType));
            itemMap.Add("TryType", typeof(Mx.Xml.tns.TryType));
            itemMap.Add("TryType_Type", typeof(Mx.Xml.tns.TryType_Type));
            itemMap.Add("ValidatorRecipe", typeof(Mx.Xml.tns.ValidatorRecipe));
            itemMap.Add("ValidatorReferenceType", typeof(Mx.Xml.tns.ValidatorReferenceType));
            itemMap.Add("ValidatorType", typeof(Mx.Xml.tns.ValidatorType));
            itemMap.Add("ValidatorType_Type", typeof(Mx.Xml.tns.ValidatorType_Type));
            _nsMap.Add("http://difi.no/xsd/certvalidator/1.0", itemMap);
        }
        #endregion

        #region FromXml
        /// <summary>
        /// Creates an object from XML data held in a string.
        /// </summary>
        /// <param name="xmlIn">The data to be loaded</param>
        /// <returns>The wrapper object, loaded with the supplied data</returns>
        /// <remarks>Throws an exception if the XML data is not compatible with the schema</remarks>
        public static LiquidTechnologies.Runtime.Standard20.XmlObjectBase FromXml(String xmlIn)
        {
            return FromXml(xmlIn, LiquidTechnologies.Runtime.Standard20.SerializationContext.Default);
        }
        /// <summary>
        /// Creates an object from XML data held in a string.
        /// </summary>
        /// <param name="xmlIn">The data to be loaded</param>
        /// <param name="context">The context to use when loading the data</param>
        /// <returns>The wrapper object, loaded with the supplied data</returns>
        /// <remarks>Throws an exception if the XML data is not compatible with the schema</remarks>
        public static LiquidTechnologies.Runtime.Standard20.XmlObjectBase FromXml(String xmlIn, LiquidTechnologies.Runtime.Standard20.SerializationContext context)
        {
            XmlDocument xmlDoc = LiquidTechnologies.Runtime.Standard20.XmlObjectBase.LoadXmlDocument(xmlIn, context);
            return FromXmlElement(xmlDoc.DocumentElement, context);
        }
        #endregion

        #region FromXmlFile
        /// <summary>
        /// Creates an object from XML data held in a File
        /// </summary>
        /// <param name="FileName">The file to be loaded</param>
        /// <returns>The wrapper object, loaded with the supplied data</returns>
        /// <remarks>Throws an exception if the XML data is not compatible with the schema</remarks>
        public static LiquidTechnologies.Runtime.Standard20.XmlObjectBase FromXmlFile(String FileName)
        {
            return FromXmlFile(FileName, LiquidTechnologies.Runtime.Standard20.SerializationContext.Default);
        }
        /// <summary>
        /// Creates an object from XML data held in a File
        /// </summary>
        /// <param name="FileName">The file to be loaded</param>
        /// <param name="context">The context to use when loading the data</param>
        /// <returns>The wrapper object, loaded with the supplied data</returns>
        /// <remarks>Throws an exception if the XML data is not compatible with the schema</remarks>
        public static LiquidTechnologies.Runtime.Standard20.XmlObjectBase FromXmlFile(String FileName, LiquidTechnologies.Runtime.Standard20.SerializationContext context)
        {
            using (System.IO.Stream stream = new System.IO.FileStream(FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read,System.IO.FileShare.Read))
            {
                return FromXmlStream(stream, context);
            }
        }
        #endregion

        #region FromXmlStream
        /// <summary>
        /// Creates an object from XML data held in a stream.
        /// </summary>
        /// <param name="stream">The data to be loaded</param>
        /// <returns>The wrapper object, loaded with the supplied data</returns>
        /// <remarks>Throws an exception if the XML data is not compatible with the schema</remarks>
        public static LiquidTechnologies.Runtime.Standard20.XmlObjectBase FromXmlStream(System.IO.Stream stream)
        {
            return FromXmlStream(stream, LiquidTechnologies.Runtime.Standard20.SerializationContext.Default);
        }
        /// <summary>
        /// Creates an object from XML data held in a stream.
        /// </summary>
        /// <param name="stream">The data to be loaded</param>
        /// <returns>The wrapper object, loaded with the supplied data</returns>
        /// <remarks>Throws an exception if the XML data is not compatible with the schema</remarks>
        public static LiquidTechnologies.Runtime.Standard20.XmlObjectBase FromXmlStream(System.IO.Stream stream, LiquidTechnologies.Runtime.Standard20.SerializationContext context)
        {
            XmlDocument xmlDoc = LiquidTechnologies.Runtime.Standard20.XmlObjectBase.LoadXmlDocument(stream, context);
            return FromXmlElement(xmlDoc.DocumentElement, context);
        }
        #endregion

        #region FromXmlElement
        /// <summary>
        /// Creates an object from an XML Element.
        /// </summary>
        /// <param name="xmlParent">The data that needs loading</param>
        /// <returns>The wrapper object, loaded with the supplied data</returns>
        /// <remarks>Throws an exception if the XML data is not compatible with the schema</remarks>
        public static LiquidTechnologies.Runtime.Standard20.XmlObjectBase FromXmlElement(XmlElement xmlParent)
        {
            return FromXmlElement(xmlParent, LiquidTechnologies.Runtime.Standard20.SerializationContext.Default);
        }
        /// <summary>
        /// Creates an object from an XML Element.
        /// </summary>
        /// <param name="xmlParent">The data that needs loading</param>
        /// <param name="context">The context to use when loading the data</param>
        /// <returns>The wrapper object, loaded with the supplied data</returns>
        /// <remarks>Throws an exception if the XML data is not compatible with the schema</remarks>
        public static LiquidTechnologies.Runtime.Standard20.XmlObjectBase FromXmlElement(XmlElement xmlParent, LiquidTechnologies.Runtime.Standard20.SerializationContext context)
        {
            LiquidTechnologies.Runtime.Standard20.XmlObjectBase retVal = null;
            String elementName;
            String elementNamespaceUri;

            // Get the type name this is either 
            // from the element i.e. <Parent>... = Parent
            // or from the type i.e. <Parent xsi:type="someNS:SomeElement">... = SomeElement
            if (LiquidTechnologies.Runtime.Standard20.ClassFactoryHelper.GetElementType(xmlParent) == "")
            {
                elementName = xmlParent.LocalName;
                elementNamespaceUri = xmlParent.NamespaceURI;
            }
            else
            {
                elementName = LiquidTechnologies.Runtime.Standard20.ClassFactoryHelper.GetElementType(xmlParent);
                elementNamespaceUri = LiquidTechnologies.Runtime.Standard20.ClassFactoryHelper.GetElementTypeNamespaceUri(xmlParent);
            }

            // create the appropriate object
            retVal = LiquidTechnologies.Runtime.Standard20.ClassFactoryHelper.CreateObject(_nsMap, elementName, elementNamespaceUri, context);
            if (retVal == null)
                throw new LiquidTechnologies.Runtime.Standard20.LtException(string.Format("Failed load the element {0}:{1}. No appropriate class exists to load the data into. Ensure that the XML document complies with the schema.", elementName, elementNamespaceUri));
            
            // load the data into the object
            retVal.FromXmlElement(xmlParent, context);

            return retVal;
        }
        #endregion
    }
}
namespace Mx.Xml.tns
{
    public static class ClassFactory
    {
        #region Static Constructor
        private static System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, System.Type>> _IExtensibleTypeMap = null;
        private static System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, System.Type>> _IReferenceTypeMap = null;
        static ClassFactory()
        {
            System.Collections.Generic.Dictionary<string, System.Type> itemMap = null;
            _IExtensibleTypeMap = new System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, System.Type>>();
            itemMap = new System.Collections.Generic.Dictionary<string, System.Type>();
            itemMap.Add("ExtensibleType", typeof(Mx.Xml.tns.ExtensibleType));
            itemMap.Add("HandleErrorType", typeof(Mx.Xml.tns.HandleErrorType));
            itemMap.Add("CachedType", typeof(Mx.Xml.tns.CachedType));
            itemMap.Add("TryType", typeof(Mx.Xml.tns.TryType));
            itemMap.Add("JunctionType", typeof(Mx.Xml.tns.JunctionType));
            itemMap.Add("ValidatorType", typeof(Mx.Xml.tns.ValidatorType));
            _IExtensibleTypeMap.Add("http://difi.no/xsd/certvalidator/1.0", itemMap);
            _IReferenceTypeMap = new System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, System.Type>>();
            itemMap = new System.Collections.Generic.Dictionary<string, System.Type>();
            itemMap.Add("ReferenceType", typeof(Mx.Xml.tns.ReferenceType));
            itemMap.Add("ValidatorReferenceType", typeof(Mx.Xml.tns.ValidatorReferenceType));
            itemMap.Add("RuleReferenceType", typeof(Mx.Xml.tns.RuleReferenceType));
            _IReferenceTypeMap.Add("http://difi.no/xsd/certvalidator/1.0", itemMap);
        }
        #endregion

        // We are trying to create a class, however it may be any one of the derived 
        // classes that we want, so we need to try to create them, if they fail move on to
        // the next, until we have one that fits.
        public static IExtensibleType IExtensibleTypeCreateObject(XmlElement xmlParent, LiquidTechnologies.Runtime.Standard20.SerializationContext context)
        {
            Mx.Xml.tns.IExtensibleType retVal = null;

            // Get the type name
            String typeName = LiquidTechnologies.Runtime.Standard20.ClassFactoryHelper.GetElementType(xmlParent);

            if (typeName == "")
                return new Mx.Xml.tns.ExtensibleType();

            if (retVal == null)
                retVal = LiquidTechnologies.Runtime.Standard20.ClassFactoryHelper.CreateObject(_IExtensibleTypeMap, typeName, LiquidTechnologies.Runtime.Standard20.ClassFactoryHelper.GetElementTypeNamespaceUri(xmlParent), context) as Mx.Xml.tns.IExtensibleType;

            return retVal;
        }

        // We are trying to create a class, however it may be any one of the derived 
        // classes that we want, so we need to try to create them, if they fail move on to
        // the next, until we have one that fits.
        public static IReferenceType IReferenceTypeCreateObject(XmlElement xmlParent, LiquidTechnologies.Runtime.Standard20.SerializationContext context)
        {
            Mx.Xml.tns.IReferenceType retVal = null;

            // Get the type name
            String typeName = LiquidTechnologies.Runtime.Standard20.ClassFactoryHelper.GetElementType(xmlParent);

            if (typeName == "")
                return new Mx.Xml.tns.ReferenceType();

            if (retVal == null)
                retVal = LiquidTechnologies.Runtime.Standard20.ClassFactoryHelper.CreateObject(_IReferenceTypeMap, typeName, LiquidTechnologies.Runtime.Standard20.ClassFactoryHelper.GetElementTypeNamespaceUri(xmlParent), context) as Mx.Xml.tns.IReferenceType;

            return retVal;
        }
    }
}

