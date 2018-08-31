using System;
using System.Linq;

using Microsoft.AspNetCore.Mvc;

namespace Mx.Hyperway.Smp.Controllers
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Security.Cryptography;
    using System.Security.Cryptography.X509Certificates;
    using System.Security.Cryptography.Xml;
    using System.Text;
    using System.Xml;

    using LiquidTechnologies.Runtime.Standard20;

    using Microsoft.ApplicationInsights.AspNetCore.Extensions;
    using Microsoft.EntityFrameworkCore;

    using Mx.Hyperway.Smp.Data;
    using Mx.Peppol.Common.Model;
    using Mx.Peppol.Common.Util;
    using Mx.Xml.Busdox.ids;
    using Mx.Xml.Busdox.tns;
    using Mx.Xml.Busdox.wsa;

    using ParticipantIdentifier = Mx.Xml.Busdox.ids.ParticipantIdentifier;
    using ProcessIdentifier = Mx.Xml.Busdox.ids.ProcessIdentifier;
    using Reference = System.Security.Cryptography.Xml.Reference;
    using ServiceMetadata = Mx.Xml.Busdox.tns.ServiceMetadata;

    public class SmpController : Controller
    {
        private readonly SmpContext context;

        public SmpController(SmpContext context)
        {
            this.context = context;
        }

        public ContentResult GetServiceMetadata()
        {
            var uri = this.HttpContext.Request.GetUri();
            var url = uri.PathAndQuery + uri.Fragment;
            if (url.Contains("/services/"))
            {
                var xml = this.GetServiceMetadataXml(url);
                if (xml != null)
                {
                    var x = new ContentResult();
                    x.ContentType = "text/xml";
                    x.Content = xml.OuterXml;
                    return x;
                }

            }
            return new ContentResult()
                       {
                           Content = "<html><body><h1>SMP SEDIVA (TEST)</h1></body></html>",
                           ContentType = "text/html"
                       };
        }

        private XmlDocument GetServiceMetadataXml(string url)
        {
            XmlDocument doc;
            try
            {
                TestData.SetupDevDatabase(this.context);

                var values = url.Split('/');
                var participantIdentifier = ModelUtils.Urldecode(values[1]);
                var documentIdentifier = ModelUtils.Urldecode(values[3]);
                var services =
                    (from x in this.context.SmpServices.Include(x => x.PeppolParticipant).Include(x => x.PeppolProcess)
                         .Include(x => x.Endpoints).Include(x => x.PeppolDocument)
                     where x.PeppolParticipant.Identifier == participantIdentifier
                           && x.PeppolDocument.Identifier == documentIdentifier
                     select x).ToList();
                if (services.Count == 0)
                {
                    return null;
                }

                doc = this.CreateXml(services);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message + " " + ex.StackTrace);
                throw;
            }
            return doc;
        }

        public XmlDocument CreateXml(IList<SmpService> services)
        {
            var firstService = services.First();
            var participant = Peppol.Common.Model.ParticipantIdentifier.Parse(firstService.PeppolParticipant.Identifier);
            var documentType = DocumentTypeIdentifier.Parse(firstService.PeppolDocument.Identifier);
            
            
            var signedServiceMetadata = new SignedServiceMetadata();
            var serviceMetadata = new ServiceMetadata();
            signedServiceMetadata.ServiceMetadata = serviceMetadata;
            var serviceInfo = new ServiceInformationType();
            serviceMetadata.ServiceInformation = serviceInfo;

            serviceInfo.ParticipantIdentifier = new ParticipantIdentifier();
            serviceInfo.ParticipantIdentifier.Scheme = participant.Scheme.Identifier;
            serviceInfo.ParticipantIdentifier.PrimitiveValue = participant.Identifier;

            serviceInfo.DocumentIdentifier = new DocumentIdentifier();
            serviceInfo.DocumentIdentifier.Scheme = documentType.Scheme.Identifier;
            serviceInfo.DocumentIdentifier.PrimitiveValue = documentType.Identifier;
            serviceInfo.ProcessList = new ProcessListType();

            foreach (var service in services)
            {
                var process = new ProcessType();
                serviceInfo.ProcessList.Process.Add(process);
                var processIdentifier =
                    Peppol.Common.Model.ProcessIdentifier.Parse(service.PeppolProcess.Identifier);
                process.ProcessIdentifier = new ProcessIdentifier();
                process.ProcessIdentifier.Scheme = processIdentifier.Scheme.Identifier;
                process.ProcessIdentifier.PrimitiveValue = processIdentifier.Identifier;
                process.ServiceEndpointList = new ServiceEndpointList();

                foreach (var endpoint in service.Endpoints)
                {
                    var xEndpoint = new EndpointType();
                    xEndpoint.EndpointReference = new EndpointReference();
                    xEndpoint.EndpointReference.Address.PrimitiveValue = endpoint.Endpoint;
                    xEndpoint.EndpointReference.ReferenceParameters = new ReferenceParameters();
                    xEndpoint.EndpointReference.Metadata = new Metadata();
                    xEndpoint.TransportProfile = "busdox-transport-as2-ver1p0";
                    xEndpoint.Certificate = endpoint.Certificate;
                    xEndpoint.MinimumAuthenticationLevel = endpoint.MinimumAuthenticationLevel;
                    xEndpoint.RequireBusinessLevelSignature = endpoint.RequireBusinessLevelSignature;
                    xEndpoint.ServiceExpirationDate = new XmlDateTime(endpoint.ServiceExpirationDate);
                    xEndpoint.ServiceActivationDate = new XmlDateTime(endpoint.ServiceActivationDate);
                    xEndpoint.ServiceDescription = endpoint.ServiceDescription;
                    xEndpoint.TechnicalContactUrl = endpoint.TechnicalContactUrl;
                    process.ServiceEndpointList.Endpoint.Add(xEndpoint);
                }
            }


            var certPath = "conf\\test-keystore.pfx";
            var certPass = "test";
            var collection = new X509Certificate2Collection();
            collection.Import(certPath, certPass, X509KeyStorageFlags.PersistKeySet);
            AsymmetricAlgorithm key = null;
            X509Certificate cert = null;
            foreach (var certificate in collection)
            {
                if (certificate.HasPrivateKey)
                {
                    key = certificate.PrivateKey;
                    cert = certificate;
                }
            }
            signedServiceMetadata.Signature = new Xml.Busdox.ds.Signature();
            signedServiceMetadata.Signature.SignedInfo = new Xml.Busdox.ds.SignedInfo();
            signedServiceMetadata.Signature.SignedInfo.Reference.Add(new Xml.Busdox.ds.Reference());
            var xmlRoot = signedServiceMetadata.ToXmlElement(true, Encoding.UTF8, EOLType.CRLF);
            xmlRoot.RemoveChild(xmlRoot.LastChild);

            var xmlDoc = new XmlDocument();
            xmlDoc.PreserveWhitespace = true;
            xmlDoc.LoadXml(xmlRoot.OuterXml);

            var signedXml = new SignedXml(xmlDoc);
            signedXml.SigningKey = key;

            var reference = new Reference();
            reference.Uri = "";
            var env = new XmlDsigEnvelopedSignatureTransform();
            reference.AddTransform(env);
            signedXml.AddReference(reference);
            signedXml.ComputeSignature();

            signedXml.KeyInfo = new KeyInfo();
            var keyClause = new KeyInfoX509Data(cert);
            Debug.Assert(cert != null, nameof(cert) + " != null");
            keyClause.AddSubjectName(cert.Subject);
            signedXml.KeyInfo.AddClause(keyClause);

            var xmlDigitalSignature = signedXml.GetXml();
            xmlDoc.DocumentElement.AppendChild(xmlDoc.ImportNode(xmlDigitalSignature, true));

            return xmlDoc;
        }
    }
}