using System;
using System.Collections.Generic;

namespace Mx.Peppol.Lookup.Reader
{
    using System.IO;
    using System.Xml;

    using Mx.Peppol.Common.Api;
    using Mx.Peppol.Common.Model;
    using Mx.Peppol.Lookup.Api;
    using Mx.Peppol.Security.Xmldsig;
    using Mx.Xml.Busdox.Smp;

    using Org.BouncyCastle.Security.Certificates;
    using Org.BouncyCastle.X509;

    using EndpointType = Mx.Xml.Busdox.tns.EndpointType;
    using ParticipantIdentifier = Mx.Peppol.Common.Model.ParticipantIdentifier;
    using ProcessIdentifier = Mx.Peppol.Common.Model.ProcessIdentifier;
    using ProcessType = Mx.Xml.Busdox.tns.ProcessType;
    using ServiceInformationType = Mx.Xml.Busdox.tns.ServiceInformationType;
    using ServiceMetadata = Mx.Peppol.Common.Model.ServiceMetadata;
    using SignedServiceMetadata = Mx.Xml.Busdox.tns.SignedServiceMetadata;

    [Namespace("http://busdox.org/serviceMetadata/publishing/1.0/")]
    public class BusdoxReader : IMetadataReader
    {
        private static readonly X509CertificateParser CertificateFactory = new X509CertificateParser();

        [Obsolete]
        public List<ServiceReference> ParseServiceGroup(FetcherResponse fetcherResponse) // throws LookupException
        {
            throw new NotSupportedException("service group not managed for BUSDOX");
        }

        public IPotentiallySigned<ServiceMetadata> ParseServiceMetadata(FetcherResponse fetcherResponse)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.PreserveWhitespace = true;
                doc.Load(fetcherResponse.InputStream);
                var o = ClassFactory.FromXmlElement(doc.DocumentElement);

                Xml.Busdox.tns.ServiceMetadata serviceMetadata = o as Xml.Busdox.tns.ServiceMetadata;
                X509Certificate signer = null;
                if (o is SignedServiceMetadata)
                {
                    signer = XmldsigVerifier.Verify(doc);
                    serviceMetadata = ((SignedServiceMetadata)o).ServiceMetadata;
                }

                if (serviceMetadata == null)
                {
                    throw new LookupException("ServiceMetadata element not found");
                }

                
                ServiceInformationType serviceInformation = serviceMetadata.ServiceInformation;

                List<ProcessMetadata<Endpoint>> processMetadatas = new List<ProcessMetadata<Endpoint>>();
                foreach (ProcessType processType in serviceInformation.ProcessList.Process)
                {
                    List<Endpoint> endpoints = new List<Endpoint>();
                    foreach (EndpointType endpointType in processType.ServiceEndpointList.Endpoint)
                    {
                        var certificate = this.CertificateInstance(Convert.FromBase64String(endpointType.Certificate));
                        var endpointUri = new Uri(endpointType.EndpointReference.Address.PrimitiveValue);
                        var profile = TransportProfile.Of(endpointType.TransportProfile);
                        endpoints.Add(Endpoint.Of(profile, endpointUri, certificate));
                    }

                    processMetadatas.Add(
                        ProcessMetadata<Endpoint>.Of(
                            ProcessIdentifier.Of(
                                processType.ProcessIdentifier.PrimitiveValue,
                                Scheme.Of(processType.ProcessIdentifier.Scheme)),
                            endpoints));
                }

                return Signed<ServiceMetadata>.Of(
                    ServiceMetadata.Of(

                        ParticipantIdentifier.Of(
                            serviceInformation.ParticipantIdentifier.PrimitiveValue,
                            Scheme.Of(serviceInformation.ParticipantIdentifier.Scheme)),

                        DocumentTypeIdentifier.Of(
                            serviceInformation.DocumentIdentifier.PrimitiveValue,
                            Scheme.Of(serviceInformation.DocumentIdentifier.Scheme)),

                        processMetadatas),
                    signer);
            }
            catch (Exception e) when (e is CertificateException | e is IOException)
            {
                throw new Exception(e.Message, e);
            }
        }

        private X509Certificate CertificateInstance(byte[] content) // throws CertificateException
        {
            return CertificateFactory.ReadCertificate(content);
        }
    }
}
