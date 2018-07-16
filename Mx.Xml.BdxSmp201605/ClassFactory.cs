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
 * Using Schema: C:\src\massivex\hyperway\Mx.Peppol.Lookup\Reader\bdx-smp-201605.xsd
 **********************************************************************************************/

namespace Mx.Peppol.Lookup.Reader.BdxSmp201605
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
            itemMap.Add("CanonicalizationMethod", typeof(Mx.Peppol.Lookup.Reader.ds.CanonicalizationMethod));
            itemMap.Add("DigestMethod", typeof(Mx.Peppol.Lookup.Reader.ds.DigestMethod));
            itemMap.Add("DigestValue", typeof(Mx.Peppol.Lookup.Reader.ds.DigestValue));
            itemMap.Add("DSAKeyValue", typeof(Mx.Peppol.Lookup.Reader.ds.DSAKeyValue));
            itemMap.Add("DSAKeyValue_Seq", typeof(Mx.Peppol.Lookup.Reader.ds.DSAKeyValue_Seq));
            itemMap.Add("DSAKeyValue_SeqA", typeof(Mx.Peppol.Lookup.Reader.ds.DSAKeyValue_SeqA));
            itemMap.Add("KeyInfo", typeof(Mx.Peppol.Lookup.Reader.ds.KeyInfo));
            itemMap.Add("KeyInfo_Group", typeof(Mx.Peppol.Lookup.Reader.ds.KeyInfo_Group));
            itemMap.Add("KeyName", typeof(Mx.Peppol.Lookup.Reader.ds.KeyName));
            itemMap.Add("KeyValue", typeof(Mx.Peppol.Lookup.Reader.ds.KeyValue));
            itemMap.Add("Manifest", typeof(Mx.Peppol.Lookup.Reader.ds.Manifest));
            itemMap.Add("MgmtData", typeof(Mx.Peppol.Lookup.Reader.ds.MgmtData));
            itemMap.Add("Object", typeof(Mx.Peppol.Lookup.Reader.ds.object_));
            itemMap.Add("PGPData", typeof(Mx.Peppol.Lookup.Reader.ds.PGPData));
            itemMap.Add("PGPData_Seq", typeof(Mx.Peppol.Lookup.Reader.ds.PGPData_Seq));
            itemMap.Add("PGPData_SeqA", typeof(Mx.Peppol.Lookup.Reader.ds.PGPData_SeqA));
            itemMap.Add("Reference", typeof(Mx.Peppol.Lookup.Reader.ds.Reference));
            itemMap.Add("RetrievalMethod", typeof(Mx.Peppol.Lookup.Reader.ds.RetrievalMethod));
            itemMap.Add("RSAKeyValue", typeof(Mx.Peppol.Lookup.Reader.ds.RSAKeyValue));
            itemMap.Add("Signature", typeof(Mx.Peppol.Lookup.Reader.ds.Signature));
            itemMap.Add("SignatureMethod", typeof(Mx.Peppol.Lookup.Reader.ds.SignatureMethod));
            itemMap.Add("SignatureProperties", typeof(Mx.Peppol.Lookup.Reader.ds.SignatureProperties));
            itemMap.Add("SignatureProperty", typeof(Mx.Peppol.Lookup.Reader.ds.SignatureProperty));
            itemMap.Add("SignatureValue", typeof(Mx.Peppol.Lookup.Reader.ds.SignatureValue));
            itemMap.Add("SignedInfo", typeof(Mx.Peppol.Lookup.Reader.ds.SignedInfo));
            itemMap.Add("SPKIData", typeof(Mx.Peppol.Lookup.Reader.ds.SPKIData));
            itemMap.Add("SPKIData_Group", typeof(Mx.Peppol.Lookup.Reader.ds.SPKIData_Group));
            itemMap.Add("Transform", typeof(Mx.Peppol.Lookup.Reader.ds.Transform));
            itemMap.Add("Transform_Group", typeof(Mx.Peppol.Lookup.Reader.ds.Transform_Group));
            itemMap.Add("Transforms", typeof(Mx.Peppol.Lookup.Reader.ds.Transforms));
            itemMap.Add("X509Data", typeof(Mx.Peppol.Lookup.Reader.ds.X509Data));
            itemMap.Add("X509Data_Group", typeof(Mx.Peppol.Lookup.Reader.ds.X509Data_Group));
            itemMap.Add("X509IssuerSerialType", typeof(Mx.Peppol.Lookup.Reader.ds.X509IssuerSerialType));
            _nsMap.Add("http://www.w3.org/2000/09/xmldsig#", itemMap);
            itemMap = new System.Collections.Generic.Dictionary<string, System.Type>();
            itemMap.Add("DocumentIdentifier", typeof(Mx.Peppol.Lookup.Reader.tns.DocumentIdentifier));
            itemMap.Add("EndpointType", typeof(Mx.Peppol.Lookup.Reader.tns.EndpointType));
            itemMap.Add("ExtensionType", typeof(Mx.Peppol.Lookup.Reader.tns.ExtensionType));
            itemMap.Add("ParticipantIdentifier", typeof(Mx.Peppol.Lookup.Reader.tns.ParticipantIdentifier));
            itemMap.Add("ProcessIdentifier", typeof(Mx.Peppol.Lookup.Reader.tns.ProcessIdentifier));
            itemMap.Add("ProcessListType", typeof(Mx.Peppol.Lookup.Reader.tns.ProcessListType));
            itemMap.Add("ProcessType", typeof(Mx.Peppol.Lookup.Reader.tns.ProcessType));
            itemMap.Add("RecipientIdentifier", typeof(Mx.Peppol.Lookup.Reader.tns.RecipientIdentifier));
            itemMap.Add("RedirectType", typeof(Mx.Peppol.Lookup.Reader.tns.RedirectType));
            itemMap.Add("SenderIdentifier", typeof(Mx.Peppol.Lookup.Reader.tns.SenderIdentifier));
            itemMap.Add("ServiceEndpointList", typeof(Mx.Peppol.Lookup.Reader.tns.ServiceEndpointList));
            itemMap.Add("ServiceGroup", typeof(Mx.Peppol.Lookup.Reader.tns.ServiceGroup));
            itemMap.Add("ServiceInformationType", typeof(Mx.Peppol.Lookup.Reader.tns.ServiceInformationType));
            itemMap.Add("ServiceMetadata", typeof(Mx.Peppol.Lookup.Reader.tns.ServiceMetadata));
            itemMap.Add("ServiceMetadataReferenceCollectionType", typeof(Mx.Peppol.Lookup.Reader.tns.ServiceMetadataReferenceCollectionType));
            itemMap.Add("ServiceMetadataReferenceType", typeof(Mx.Peppol.Lookup.Reader.tns.ServiceMetadataReferenceType));
            itemMap.Add("SignedServiceMetadata", typeof(Mx.Peppol.Lookup.Reader.tns.SignedServiceMetadata));
            _nsMap.Add("http://docs.oasis-open.org/bdxr/ns/SMP/2016/05", itemMap);
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

