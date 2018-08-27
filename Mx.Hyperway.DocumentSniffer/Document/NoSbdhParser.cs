namespace Mx.Hyperway.DocumentSniffer.Document
{
    using System;
    using System.IO;
    using System.Xml.Linq;

    using Mx.Hyperway.Api.Lang;
    using Mx.Hyperway.Api.Transformer;
    using Mx.Hyperway.DocumentSniffer.Document.Parsers;
    using Mx.Peppol.Common.Model;

    /// <summary>
    /// Parses UBL based documents, which are not wrapped within an SBDH, extracting data and
    /// creating a PeppolStandardBusinessHeader.
    /// </summary>
    public class NoSbdhParser : IContentDetector
    {
        /// <summary>
        /// Parses and extracts the data needed to create a PeppolStandardBusinessHeader object. The inputstream supplied
        /// should not be wrapped in an SBDH.
        /// </summary>
        /// <param name="inputStream"></param>
        /// <returns></returns>
        public Header Parse(Stream inputStream)
        {
            return this.OriginalParse(inputStream).ToVefa();
        }

        /// <summary>
        /// Parses and extracts the data needed to create a PeppolStandardBusinessHeader object. The inputstream supplied
        /// should not be wrapped in an SBDH.
        /// </summary>
        /// <param name="inputStream"></param>
        /// <returns></returns>
        public PeppolStandardBusinessHeader OriginalParse(Stream inputStream)
        {
            try
            {
                XDocument document = XDocument.Load(inputStream);
                var nsResolver = new HardCodedNamespaceResolver();

                PeppolStandardBusinessHeader sbdh =
                    PeppolStandardBusinessHeader.CreatePeppolStandardBusinessHeaderWithNewDate();

                // use the plain UBL header parser to decode format and create correct document parser
                PlainUblHeaderParser headerParser = new PlainUblHeaderParser(document, nsResolver);

                // make sure we actually have a UBL type document
                if (headerParser.CanParse())
                {

                    sbdh.DocumentTypeIdentifier = headerParser.FetchDocumentTypeId().ToVefa();
                    sbdh.ProfileTypeIdentifier = headerParser.FetchProcessTypeId();

                    // try to use a specialized document parser to fetch more document details
                    IPeppolDocumentParser documentParser = null;
                    try
                    {
                        documentParser = headerParser.CreateDocumentParser();
                    }
                    catch (Exception)
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
                            sbdh.SenderId = documentParser.Sender;
                        }
                        catch (Exception)
                        {
                            // Continue with recipient
                        }

                        try
                        {
                            sbdh.RecipientId = documentParser.Receiver;
                        }
                        catch (Exception)
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
