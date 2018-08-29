using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Peppol.Security.Xmldsig
{
    using System.Diagnostics;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Security.Cryptography.Xml;
    using System.Xml;
    using System.Xml.Linq;

    using Mx.Peppol.Security.Lang;

    using Org.BouncyCastle.X509;

    public class XmldsigVerifier
    {

        public static X509Certificate verify(XmlDocument document) // throws PeppolSecurityException
        {
            try
            {
                SignedXml signedXml = new SignedXml(document);
                XmlNodeList nodeList = document.GetElementsByTagName("Signature");
                XmlElement signatureElement = (XmlElement)nodeList[0];
                signedXml.LoadXml(signatureElement);
                bool verified = signedXml.CheckSignature();
                if (!verified)
                {
                    throw new PeppolSecurityException("Signature failed");
                }

                NameTable nt = new NameTable();
                XmlNamespaceManager m = new XmlNamespaceManager(nt);
                m.AddNamespace("ds", "http://www.w3.org/2000/09/xmldsig#");
                var certificateNode = document.SelectSingleNode("//ds:Signature/ds:KeyInfo/ds:X509Data/ds:X509Certificate", m);
                if (certificateNode == null)
                {
                    throw new PeppolSecurityException("Cannot find Signature element");
                }
                var base64Certificate = certificateNode.InnerText;

                X509CertificateParser p = new X509CertificateParser();
                X509Certificate certificate = p.ReadCertificate(Convert.FromBase64String(base64Certificate));
                return certificate;
            }
            catch (Exception e) // (XMLSignatureException | MarshalException e)
            {
                throw new PeppolSecurityException("Unable to verify document signature.", e);
            }
        }

        XmldsigVerifier()
        {

        }
    }
}