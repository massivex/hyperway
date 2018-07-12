using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Oxalis.DocumentSniffer.Parsers
{
    using Mx.Oxalis.DocumentSniffer.Document;
    using Mx.Oxalis.DocumentSniffer.Identifier;
    using Mx.Peppol.Common.Model;

    using System.Xml.Linq;

    /**
     * Abstract implementation based on the PlainUBLParser to retrieve information from PEPPOL documents.
     * Contains common functionality to be used as a base for decoding types.
     *
     * @author thore
     */
    public abstract class AbstractDocumentParser : PEPPOLDocumentParser
    {

        protected PlainUBLParser parser;

        public AbstractDocumentParser(PlainUBLParser parser)
        {
            this.parser = parser;
        }

        /**
         * Retrieves the ParticipantId which is retrieved using the supplied XPath.
         */
        protected ParticipantIdentifier participantId(String xPathExpr)
        {
            ParticipantId ret;

            // first we retrieve the correct participant element
            XElement element;
            try
            {
                element = parser.retrieveElementForXpath(xPathExpr);
            }
            catch (Exception ex)
            {
                // DOM parser throws "java.lang.IllegalStateException: No element in XPath: ..." if no Element is found
                throw new InvalidOperationException(String.Format("No ParticipantId found at '{0}'.", xPathExpr));
            }

            // get value and any schemeId given

            String companyId = (element.FirstNode as XText).Value.Trim();
            String schemeIdTextValue = element.Attribute("schemeID").Value.Trim();

            // check if we already have a valid participant 9908:987654321
            if (ParticipantId.isValidParticipantIdentifierPattern(companyId))
            {
                if (schemeIdTextValue.Length == 0)
                {
                    // we accept participants with icd prefix if schemeId is missing ...
                    ret = new ParticipantId(companyId);
                }
                else
                {
                    // ... or when given schemeId matches the icd code stat eg NO:VAT matches 9908 from 9908:987654321
                    if (companyId.StartsWith(SchemeId.parse(schemeIdTextValue).getCode() + ":"))
                    {
                        ret = new ParticipantId(companyId);
                    }
                    else
                    {
                        throw new InvalidOperationException(
                            String.Format(
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
                companyId = String.Format("{0}:{1}", SchemeId.parse(schemeIdTextValue).getCode(), companyId);
                if (!ParticipantId.isValidParticipantIdentifierPattern(companyId))
                {
                    throw new InvalidOperationException(
                        $"ParticipantId syntax at '{xPathExpr}' evaluates to '{companyId}' and is invalid");
                }

                ret = new ParticipantId(companyId);
            }

            return ret.toVefa();
        }

        public abstract ParticipantIdentifier getSender();

        public abstract ParticipantIdentifier getReceiver();
    }
}

