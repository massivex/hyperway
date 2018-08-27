namespace Mx.Hyperway.DocumentSniffer.Document
{
    using System;
    using System.IO;
    using System.Xml.Linq;

    using Mx.Hyperway.Api.Lang;
    using Mx.Hyperway.Api.Transformer;
    using Mx.Hyperway.DocumentSniffer.Document.Parsers;
    using Mx.Peppol.Common.Model;

    /**
     * Parses UBL based documents, which are not wrapped within an SBDH, extracting data and
     * creating a PeppolStandardBusinessHeader.
     */
    public class NoSbdhParser : IContentDetector
    {

        /**
         * Parses and extracts the data needed to create a PeppolStandardBusinessHeader object. The inputstream supplied
         * should not be wrapped in an SBDH.
         */
        public Header Parse(Stream inputStream)
        {
            return this.originalParse(inputStream).toVefa();
        }

        /**
         * Parses and extracts the data needed to create a PeppolStandardBusinessHeader object. The inputstream supplied
         * should not be wrapped in an SBDH.
         */
        public PeppolStandardBusinessHeader originalParse(Stream inputStream)
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
                throw new HyperwayContentException("Unable to parse document " + e.Message, e);
            }
        }
    }

}
