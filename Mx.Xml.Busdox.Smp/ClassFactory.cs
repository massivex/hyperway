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

namespace Mx.Xml.Busdox.Smp
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
            itemMap.Add("AttributedURIType", typeof(Mx.Xml.Busdox.wsa.AttributedURIType));
            itemMap.Add("EndpointReference", typeof(Mx.Xml.Busdox.wsa.EndpointReference));
            itemMap.Add("Metadata", typeof(Mx.Xml.Busdox.wsa.Metadata));
            itemMap.Add("ReferenceParameters", typeof(Mx.Xml.Busdox.wsa.ReferenceParameters));
            _nsMap.Add("http://www.w3.org/2005/08/addressing", itemMap);
            itemMap = new System.Collections.Generic.Dictionary<string, System.Type>();
            itemMap.Add("CanonicalizationMethod", typeof(Mx.Xml.Busdox.ds.CanonicalizationMethod));
            itemMap.Add("DigestMethod", typeof(Mx.Xml.Busdox.ds.DigestMethod));
            itemMap.Add("DSAKeyValue", typeof(Mx.Xml.Busdox.ds.DSAKeyValue));
            itemMap.Add("DSAKeyValue_Seq", typeof(Mx.Xml.Busdox.ds.DSAKeyValue_Seq));
            itemMap.Add("DSAKeyValue_SeqA", typeof(Mx.Xml.Busdox.ds.DSAKeyValue_SeqA));
            itemMap.Add("KeyInfo", typeof(Mx.Xml.Busdox.ds.KeyInfo));
            itemMap.Add("KeyInfo_Group", typeof(Mx.Xml.Busdox.ds.KeyInfo_Group));
            itemMap.Add("KeyValue", typeof(Mx.Xml.Busdox.ds.KeyValue));
            itemMap.Add("Object", typeof(Mx.Xml.Busdox.ds.object_));
            itemMap.Add("PGPData", typeof(Mx.Xml.Busdox.ds.PGPData));
            itemMap.Add("PGPData_Seq", typeof(Mx.Xml.Busdox.ds.PGPData_Seq));
            itemMap.Add("PGPData_SeqA", typeof(Mx.Xml.Busdox.ds.PGPData_SeqA));
            itemMap.Add("Reference", typeof(Mx.Xml.Busdox.ds.Reference));
            itemMap.Add("RetrievalMethod", typeof(Mx.Xml.Busdox.ds.RetrievalMethod));
            itemMap.Add("RSAKeyValue", typeof(Mx.Xml.Busdox.ds.RSAKeyValue));
            itemMap.Add("Signature", typeof(Mx.Xml.Busdox.ds.Signature));
            itemMap.Add("SignatureMethod", typeof(Mx.Xml.Busdox.ds.SignatureMethod));
            itemMap.Add("SignatureValue", typeof(Mx.Xml.Busdox.ds.SignatureValue));
            itemMap.Add("SignedInfo", typeof(Mx.Xml.Busdox.ds.SignedInfo));
            itemMap.Add("SPKIData", typeof(Mx.Xml.Busdox.ds.SPKIData));
            itemMap.Add("SPKIData_Group", typeof(Mx.Xml.Busdox.ds.SPKIData_Group));
            itemMap.Add("Transform", typeof(Mx.Xml.Busdox.ds.Transform));
            itemMap.Add("Transform_Group", typeof(Mx.Xml.Busdox.ds.Transform_Group));
            itemMap.Add("Transforms", typeof(Mx.Xml.Busdox.ds.Transforms));
            itemMap.Add("X509Data", typeof(Mx.Xml.Busdox.ds.X509Data));
            itemMap.Add("X509Data_Group", typeof(Mx.Xml.Busdox.ds.X509Data_Group));
            itemMap.Add("X509IssuerSerialType", typeof(Mx.Xml.Busdox.ds.X509IssuerSerialType));
            _nsMap.Add("http://www.w3.org/2000/09/xmldsig#", itemMap);
            itemMap = new System.Collections.Generic.Dictionary<string, System.Type>();
            itemMap.Add("DocumentIdentifier", typeof(Mx.Xml.Busdox.ids.DocumentIdentifier));
            itemMap.Add("ParticipantIdentifier", typeof(Mx.Xml.Busdox.ids.ParticipantIdentifier));
            itemMap.Add("ProcessIdentifier", typeof(Mx.Xml.Busdox.ids.ProcessIdentifier));
            _nsMap.Add("http://busdox.org/transport/identifiers/1.0/", itemMap);
            itemMap = new System.Collections.Generic.Dictionary<string, System.Type>();
            itemMap.Add("EndpointType", typeof(Mx.Xml.Busdox.tns.EndpointType));
            itemMap.Add("ExtensionType", typeof(Mx.Xml.Busdox.tns.ExtensionType));
            itemMap.Add("ProcessListType", typeof(Mx.Xml.Busdox.tns.ProcessListType));
            itemMap.Add("ProcessType", typeof(Mx.Xml.Busdox.tns.ProcessType));
            itemMap.Add("RedirectType", typeof(Mx.Xml.Busdox.tns.RedirectType));
            itemMap.Add("ServiceEndpointList", typeof(Mx.Xml.Busdox.tns.ServiceEndpointList));
            itemMap.Add("ServiceInformationType", typeof(Mx.Xml.Busdox.tns.ServiceInformationType));
            itemMap.Add("ServiceMetadata", typeof(Mx.Xml.Busdox.tns.ServiceMetadata));
            itemMap.Add("SignedServiceMetadata", typeof(Mx.Xml.Busdox.tns.SignedServiceMetadata));
            _nsMap.Add("http://busdox.org/serviceMetadata/publishing/1.0/", itemMap);
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

