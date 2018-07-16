using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Peppol.Security.Xmldsig
{
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
                signedXml.LoadXml((XmlElement)nodeList[0]);
                AsymmetricAlgorithm algorithm;
                bool verified = signedXml.CheckSignatureReturningKey(out algorithm);
                

                //NodeList nl = document.getElementsByTagNameNS(XMLSignature.XMLNS, "Signature");
                //if (nl.getLength() == 0)
                //{
                //    throw new PeppolSecurityException("Cannot find Signature element");
                //}

                //X509KeySelector keySelector = new X509KeySelector();
                //DOMValidateContext valContext = new DOMValidateContext(keySelector, nl.item(0));

                //XMLSignatureFactory xmlSignatureFactory = XMLSignatureFactory.getInstance("DOM");
                //XMLSignature signature = xmlSignatureFactory.unmarshalXMLSignature(valContext);

                //if (!signature.validate(valContext))
                //    throw new PeppolSecurityException("Signature failed.");
                throw new NotSupportedException();
                return null; // ' keySelector.getCertificate();
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