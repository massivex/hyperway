using System;
using System.Collections.Generic;
using System.Text;

using Mx.Oxalis.DocumentSniffer;
using Mx.Peppol.Common.Model;

namespace Mx.Oxalis.DocumentSniffer.Document
{
    using System.IO;
    using System.Xml.Linq;
    using System.Xml.XPath;

    using Mx.Oxalis.Api.Lang;
    using Mx.Oxalis.Api.Transformer;
    using Mx.Oxalis.DocumentSniffer.Parsers;

    /**
     * Parses UBL based documents, which are not wrapped within an SBDH, extracting data and
     * creating a PeppolStandardBusinessHeader.
     *
     * @author steinar
     * @author thore
     */
    // TODO: Check IoC Registration
    // @Singleton
    // @Type("legacy")
    public class NoSbdhParser : ContentDetector
    {

        // private static readonly DocumentBuilderFactory documentBuilderFactory;

        //static {
        //    documentBuilderFactory = DocumentBuilderFactory.newInstance();
        //    documentBuilderFactory.setNamespaceAware(true);

        //    try {
        //        documentBuilderFactory.setFeature(XMLConstants.FEATURE_SECURE_PROCESSING, true);
        //    } catch (ParserConfigurationException e) {
        //        throw new IllegalStateException("Unable to configure DOM parser for secure processing.", e);
        //    }
        //}

        /**
         * Parses and extracts the data needed to create a PeppolStandardBusinessHeader object. The inputstream supplied
         * should not be wrapped in an SBDH.
         *
         * @param inputStream UBL XML data without an SBDH.
         * @return an instance of Header populated with data from the UBL XML document.
         */
        // @Override
        public Header parse(Stream inputStream) // throws OxalisContentException
        {
            return originalParse(inputStream).toVefa();
        }

        /**
         * Parses and extracts the data needed to create a PeppolStandardBusinessHeader object. The inputstream supplied
         * should not be wrapped in an SBDH.
         *
         * @param inputStream UBL XML data without an SBDH.
         * @return an instance of PeppolStandardBusinessHeader populated with data from the UBL XML document.
         */
        public PeppolStandardBusinessHeader originalParse(Stream inputStream) // throws OxalisContentException
        {
            try
            {
                // DocumentBuilder documentBuilder = documentBuilderFactory.newDocumentBuilder();
                
                XDocument document = XDocument.Load(inputStream);
                var nsResolver = new HardCodedNamespaceResolver();

                PeppolStandardBusinessHeader sbdh =
                    PeppolStandardBusinessHeader.createPeppolStandardBusinessHeaderWithNewDate();

                // use the plain UBL header parser to decode format and create correct document parser
                PlainUBLHeaderParser headerParser = new PlainUBLHeaderParser(document, nsResolver);

                // make sure we actually have a UBL type document
                if (headerParser.canParse())
                {

                    sbdh.setDocumentTypeIdentifier(headerParser.fetchDocumentTypeId().toVefa());
                    sbdh.setProfileTypeIdentifier(headerParser.fetchProcessTypeId());

                    // try to use a specialized document parser to fetch more document details
                    PEPPOLDocumentParser documentParser = null;
                    try
                    {
                        documentParser = headerParser.createDocumentParser();
                    }
                    catch (Exception ex)
                    {
                        /*
                            allow this to happen so that "unknown" PEPPOL documents still
                            can be used by explicitly setting sender and receiver thru API
                        */
                    }

                    /* However, if we found an eligible parser, we should be able to determine the sender and receiver */
                    if (documentParser != null)
                    {
                        try
                        {
                            sbdh.setSenderId(documentParser.getSender());
                        }
                        catch (Exception e)
                        {
                            // Continue with recipient
                        }

                        try
                        {
                            sbdh.setRecipientId(documentParser.getReceiver());
                        }
                        catch (Exception e)
                        {
                            // Just continue
                        }
                    }
                }

                return sbdh;
            }
            catch (Exception e)
            {
                throw new OxalisContentException("Unable to parse document " + e.Message, e);
            }
        }
    }

}
