namespace Mx.Hyperway.DocumentSniffer.Document.Parsers
{
    using System;
    using System.Diagnostics;
    using System.Xml.Linq;

    using Mx.Hyperway.DocumentSniffer.Identifier;
    using Mx.Peppol.Common.Model;

    /// <summary>
    /// Abstract implementation based on the PlainUBLParser to retrieve information from PEPPOL documents.
    /// Contains common functionality to be used as a base for decoding types.
    /// </summary>
    public abstract class AbstractDocumentParser : IPeppolDocumentParser
    {
        protected PlainUblParser Parser;

        public AbstractDocumentParser(PlainUblParser parser)
        {
            this.Parser = parser;
        }

        /// <summary>
        /// Retrieves the ParticipantId which is retrieved using the supplied XPath. 
        /// </summary>
        /// <param name="xPathExpr"></param>
        /// <returns></returns>
        protected ParticipantIdentifier ParticipantId(String xPathExpr)
        {
            ParticipantId ret;

            // first we retrieve the correct participant element
            XElement element;
            try
            {
                element = this.Parser.RetrieveElementForXpath(xPathExpr);
            }
            catch (Exception)
            {
                // DOM parser throws "java.lang.IllegalStateException: No element in XPath: ..." if no Element is found
                throw new InvalidOperationException(String.Format("No ParticipantId found at '{0}'.", xPathExpr));
            }

            // get value and any schemeId given

            String companyId = (element.FirstNode as XText)?.Value.Trim();
            String schemeIdTextValue = element.Attribute("schemeID")?.Value.Trim();

            // check if we already have a valid participant 9908:987654321
            if (Identifier.ParticipantId.IsValidParticipantIdentifierPattern(companyId))
            {
                Debug.Assert(schemeIdTextValue != null, nameof(schemeIdTextValue) + " != null");
                if (schemeIdTextValue.Length == 0)
                {
                    // we accept participants with icd prefix if schemeId is missing ...
                    ret = new ParticipantId(companyId);
                }
                else
                {
                    // ... or when given schemeId matches the icd code stat eg NO:VAT matches 9908 from 9908:987654321
                    Debug.Assert(companyId != null, nameof(companyId) + " != null");
                    if (companyId.StartsWith(SchemeId.Parse(schemeIdTextValue).Code+ ":"))
                    {
                        ret = new ParticipantId(companyId);
                    }
                    else
                    {
                        throw new InvalidOperationException(
                            string.Format(
                                "ParticipantId at '{0}' is illegal, schemeId '{1}' and icd code prefix of '{2}' does not match",
                                xPathExpr,
                                schemeIdTextValue,
                                companyId));
                    }
                }
            }
            else
            {
                // try to add the given icd prefix to the participant id
                companyId = String.Format("{0}:{1}", SchemeId.Parse(schemeIdTextValue).Code, companyId);
                if (!Identifier.ParticipantId.IsValidParticipantIdentifierPattern(companyId))
                {
                    throw new InvalidOperationException(
                        $"ParticipantId syntax at '{xPathExpr}' evaluates to '{companyId}' and is invalid");
                }

                ret = new ParticipantId(companyId);
            }

            return ret.ToVefa();
        }

        public abstract ParticipantIdentifier Sender { get; }

        public abstract ParticipantIdentifier Receiver { get; }
    }
}

