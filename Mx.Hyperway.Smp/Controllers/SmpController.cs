using System;
using System.Linq;

using Microsoft.AspNetCore.Mvc;

namespace Mx.Hyperway.Smp.Controllers
{
    using System.Collections.Generic;
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
    using Mx.Xml.Busdox.ds;
    using Mx.Xml.Busdox.ids;
    using Mx.Xml.Busdox.tns;
    using Mx.Xml.Busdox.wsa;

    using ParticipantIdentifier = Mx.Xml.Busdox.ids.ParticipantIdentifier;
    using ProcessIdentifier = Mx.Xml.Busdox.ids.ProcessIdentifier;
    using Reference = System.Security.Cryptography.Xml.Reference;
    using ServiceMetadata = Mx.Xml.Busdox.tns.ServiceMetadata;
    using XmlTextWriter = System.Xml.XmlTextWriter;
    using XmlWriter = LiquidTechnologies.Runtime.Standard20.XmlWriter;

    [Route("{*smpUrl}")]
    public class SmpController : Controller
    {
        private readonly SmpContext context;

        public SmpController(SmpContext context)
        {
            this.context = context;
        }

        // GET api/values
        [HttpGet()]
        public void Get()
        {
            var uri = this.HttpContext.Request.GetUri();
            var url = uri.PathAndQuery + uri.Fragment;
            if (url.Contains("/services/"))
            {
                TestData.SetupDevDatabase(this.context);
                var test = this.context.SmpHosts.ToList();

                var values = url.Split('/');
                var participantIdentifier = ModelUtils.Urldecode(values[1]);
                var documentIdentifier = ModelUtils.Urldecode(values[3]);
                var services =
                    (from x in this.context.SmpServices
                         .Include(x => x.PeppolParticipant)
                         .Include(x => x.PeppolProcess)
                         .Include(x => x.Endpoints)
                         .Include(x => x.PeppolDocument)
                     where x.PeppolParticipant.Identifier == participantIdentifier
                           && x.PeppolDocument.Identifier == documentIdentifier
                     select x).ToList();
                if (services.Count == 0)
                {
                    return;
                }

                var doc = this.CreateXml(services);
                this.HttpContext.Response.Headers.Add("Content-Type", "text/xml");
                var xmlWriter = new XmlTextWriter(this.HttpContext.Response.Body, Encoding.UTF8);
                doc.WriteTo(xmlWriter);
                xmlWriter.Flush();
            }
            return;
        }

        public XmlDocument CreateXml(IList<SmpService> services)
        {
            var firstService = services.First();
            Mx.Peppol.Common.Model.ParticipantIdentifier participant = Peppol.Common.Model.ParticipantIdentifier.Parse(firstService.PeppolParticipant.Identifier);
            Mx.Peppol.Common.Model.DocumentTypeIdentifier documentType = DocumentTypeIdentifier.Parse(firstService.PeppolDocument.Identifier);
            
            
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
                Mx.Peppol.Common.Model.ProcessIdentifier processIdentifier =
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
                    xEndpoint.MinimumAuthenticationLevel = endpoint.MinimumAuthenticationLevel.ToString();
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
            X509Certificate2Collection collection = new X509Certificate2Collection();
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

            SignedXml signedXml = new SignedXml(xmlDoc);
            signedXml.SigningKey = key;

            Reference reference = new Reference();
            reference.Uri = "";
            XmlDsigEnvelopedSignatureTransform env = new XmlDsigEnvelopedSignatureTransform();
            reference.AddTransform(env);
            signedXml.AddReference(reference);
            signedXml.ComputeSignature();

            signedXml.KeyInfo = new System.Security.Cryptography.Xml.KeyInfo();
            var keyClause = new KeyInfoX509Data(cert);
            keyClause.AddSubjectName(cert.Subject);
            signedXml.KeyInfo.AddClause(keyClause);

            XmlElement xmlDigitalSignature = signedXml.GetXml();
            xmlDoc.DocumentElement.AppendChild(xmlDoc.ImportNode(xmlDigitalSignature, true));

            return xmlDoc;
        }
    }
}