using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Peppol.Lookup.Reader
{
    using System.IO;
    using System.Net;
    using System.Xml;

    using log4net;

    using Mx.Peppol.Common.Api;
    using Mx.Peppol.Common.Model;
    using Mx.Peppol.Lookup.Api;
    using Mx.Peppol.Lookup.Model;
    using Mx.Peppol.Lookup.Reader.BdxSmp201605;
    using Mx.Peppol.Lookup.Reader.tns;
    using Mx.Peppol.Security.Xmldsig;
    using Mx.Tools;

    using Org.BouncyCastle.Security.Certificates;
    using Org.BouncyCastle.X509;

    using ParticipantIdentifier = Mx.Peppol.Common.Model.ParticipantIdentifier;
    using ProcessIdentifier = Mx.Peppol.Common.Model.ProcessIdentifier;
    using ServiceMetadata = Mx.Peppol.Common.Model.ServiceMetadata;

    [Namespace("http://docs.oasis-open.org/bdxr/ns/SMP/2016/05")]
    public class Bdxr201605Reader : MetadataReader
    {
        private static readonly ILog LOGGER = LogManager.GetLogger(typeof(Bdxr201605Reader));

        //    private static JAXBContext jaxbContext;

        private static readonly X509CertificateParser certificateFactory = new X509CertificateParser();

        //        static {
        //        ExceptionUtil.perform(PeppolRuntimeException.class, new PerformAction()
        //        {
        //            @Override
        //            public void action() throws Exception {
        //                jaxbContext = JAXBContext.newInstance(ServiceGroupType.class, SignedServiceMetadataType.class,
        //                        ServiceMetadataType.class);
        //                certificateFactory = CertificateFactory.getInstance("X.509");
        //            }
        //});
        //    }

        public List<ServiceReference> parseServiceGroup(FetcherResponse fetcherResponse) // throws LookupException
        {
            try
            {
                ServiceGroup serviceGroup = new ServiceGroup();
                serviceGroup.FromXmlStream(fetcherResponse.InputStream);

                List<ServiceReference> serviceReferences = new List<ServiceReference>();

                foreach (ServiceMetadataReferenceType reference in serviceGroup.ServiceMetadataReferenceCollection.ServiceMetadataReference) {
                    
                    String hrefDocumentTypeIdentifier = WebUtility.UrlDecode(reference.Href).Split(new string[] { "/services/" }, StringSplitOptions.None)[1];
                    String[] parts = hrefDocumentTypeIdentifier.Split(new string[] { "::" }, 2, StringSplitOptions.None);

                    try
                    {
                        serviceReferences.Add(
                            ServiceReference.Of(
                                DocumentTypeIdentifierWithUri.of(
                                    parts[1],
                                    Scheme.Of(parts[0]),
                                    new Uri(reference.Href))));
                    }
                    catch (Exception e)
                    {
                        LOGGER.WarnFormat("Unable to parse '{0}'.", hrefDocumentTypeIdentifier);
                    }
                }

                return serviceReferences;
            }
            catch (Exception e) {
                throw new LookupException(e.Message, e);
            }
        }

        public IPotentiallySigned<ServiceMetadata> parseServiceMetadata(FetcherResponse fetcherResponse)
            //  throws LookupException, PeppolSecurityException
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(fetcherResponse.InputStream);
                    // Document doc = DomUtils.parse(fetcherResponse.getInputStream());

                // Unmarshaller unmarshaller = jaxbContext.createUnmarshaller();

                // Object o = ((JAXBElement < ?>) unmarshaller.unmarshal(new DOMSource(doc))).getValue();
                var o = ClassFactory.FromXmlStream(fetcherResponse.InputStream);

                Mx.Peppol.Lookup.Reader.tns.ServiceMetadata serviceMatadata;
                X509Certificate signer = null;
                if (o is SignedServiceMetadata) {
                    signer = XmldsigVerifier.verify(doc);
                    serviceMatadata = ((SignedServiceMetadata) o).ServiceMetadata;
                }

                ServiceInformationType serviceInformation = ((tns.ServiceMetadata) o).ServiceInformation;

                List<ProcessMetadata<Endpoint>> processMetadatas = new List<ProcessMetadata<Endpoint>>();
                foreach (ProcessType processType in serviceInformation.ProcessList.Process) {
                    List<Endpoint> endpoints = new List<Endpoint>();
                    foreach (EndpointType endpointType in processType.ServiceEndpointList.Endpoint) {
                        endpoints.Add(
                            Endpoint.Of(
                                TransportProfile.Of(endpointType.TransportProfile),
                                new Uri(endpointType.EndpointURI),
                                certificateInstance(endpointType.Certificate.Data)));
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
            catch (Exception e) when (e is CertificateException | e is IOException ) {
                throw new Exception(e.Message, e);
            }
        }

        private X509Certificate certificateInstance(byte[] content) // throws CertificateException
        {
            return certificateFactory.ReadCertificate(content);
        }
    }
}
