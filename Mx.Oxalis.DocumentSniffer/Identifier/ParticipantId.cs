using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Oxalis.DocumentSniffer.Identifier
{
    using System.Text.RegularExpressions;

    using Mx.Oxalis.DocumentSniffer.Lang;
    using Mx.Peppol.Common.Model;
    using Mx.Peppol.Icd.Api;

    /**
     * @author Steinar Overbeck Cook
     * @author Thore Johnsen
     * <p>
     * TODO: introduce the iso6235 ICD as a separate property of the constructor
     * @see SchemeId
     */
    public class ParticipantId
    {

        static readonly Regex ISO6523_PATTERN = new Regex("^(\\d{4}):([^\\s]+)$", RegexOptions.Compiled);

        // max length for international organisation number
        static readonly int INTERNATION_ORG_ID_MAX_LENGTH = 50;

        // Holds the textual representation of this PEPPOL participant id
        private readonly String peppolParticipantIdValue;

        /**
         * Constructs a new instance based upon a match of the following patterns :
         * <ol>
         * <li>{@code xxxx:yyyyyy} - i.e. a 4 digit ICD followed by a ':' followed by the organisationID</li>
         * <li>AB999999999 - i.e. a prefix of at least two characters followed by something</li>
         * </ol>
         *
         * @param participantId participant Id represented as a string
         * @throws InvalidPeppolParticipantException if we are unable to recognize the input as a PEPPOL participant ID
         */
        public ParticipantId(String participantId)
        {
            peppolParticipantIdValue = parse(participantId);
        }

        /**
         * Uses combination of SchemeId and Organisation identifier to create new instance.
         * The Organisation identifier is validated in accordance with the rules of the scheme.
         *
         * @param schemeId
         * @param organisationId
         */
        public ParticipantId(Icd schemeId, String organisationId)
        {

            if (schemeId == null)
            {
                throw new ArgumentException("SchemeId must be specified with a a valid ISO6523 code.");
            }

            if (organisationId == null)
            {
                throw new ArgumentException("The organisation id must be specified.");
            }

            if (organisationId.Length > INTERNATION_ORG_ID_MAX_LENGTH)
            {
                throw new ArgumentException(
                    String.Format(
                        "Invalid organisation id. '{0}' is longer than {1} characters",
                        organisationId,
                        INTERNATION_ORG_ID_MAX_LENGTH));
            }

            // Formats the organisation identifier in accordance with what PEPPOL expects.
            peppolParticipantIdValue = String.Format("{0}:{1}", schemeId.getCode(), organisationId);
        }

        /**
         * Parses the input string assuming it represents an organisation number or PEPPOL participant identifier in one
         * of these forms:
         * <ol>
         * <li>icd +':' + organisation identifier</li>
         * <li>National organisation number with at least two character prefix.</li>
         * </ol>
         * <p>
         * After parsing, the organisation identifier is validated in accordance with the rules of the scheme a
         * validator is found.
         * </p>
         *
         * @param participantId the string representing the participant identifier or organisation identifier
         * @return a string on the form [ISO6523 ICD]:[participantId];
         */
        static String parse(String participantId) // throws InvalidPeppolParticipantException
        {
            String organisationId = participantId.Trim().Replace("\\s", ""); // Squeezes out any white spaces
            Icd schemeId = null;

            MatchCollection matches = ISO6523_PATTERN.Matches(organisationId);

            if (matches.Count == 0)
            {
                throw new InvalidPeppolParticipantException(String.Format("ICD not found in '{0}'.", participantId));
            }

            // If the representation is in the form xxxx:yyyyyyyyy, we are good
            String icd = matches[0].Groups[1].Value;
            organisationId = matches[0].Groups[2].Value;

            try
            {
                schemeId = SchemeId.fromISO6523(icd); // Locates the associated scheme
            }
            catch (ArgumentException e)
            {
                // No action.
            }

            if (schemeId == null)
                throw new InvalidPeppolParticipantException("ICD " + icd + " is unknown");

            // Constructs the textual representation of the PEPPOL participant identifier
            return String.Format("{0}:{1}", schemeId.getCode(), organisationId);
        }


        /**
         * Parses the provided participant identifier into a validated instance
         * of {@link ParticipantId}
         *
         * @param participantId The organisation number as xxxx:yyyy or just an organisation number
         * @return validated instance of Participant Id
         */
        public static ParticipantId valueOf(String participantId)
        {
            return new ParticipantId(parse(participantId.Trim()));

        }

        /**
         * Simple syntax verifier, verifies icd prefix + code
         */
        public static bool isValidParticipantIdentifierPattern(String value)
        {
            if (value == null) return false;

            Match matcher = ISO6523_PATTERN.Match(value);
            return matcher.Success;
        }


        public override bool Equals(Object o)
        {
            if (this == o) return true;
            if (!(o is ParticipantId)) return false;
            
            ParticipantId that = (ParticipantId)o;
            if (peppolParticipantIdValue != null
                    ? !peppolParticipantIdValue.Equals(that.peppolParticipantIdValue)
                    : that.peppolParticipantIdValue != null)
            {
                return false;
            }

            return true;
        }


        public override int GetHashCode()
        {
            return peppolParticipantIdValue != null ? peppolParticipantIdValue.GetHashCode() : 0;
        }

        public String stringValue()
        {
            return peppolParticipantIdValue;
        }


        public override String ToString()
        {
            return peppolParticipantIdValue;
        }

        public ParticipantIdentifier toVefa()
        {
            return ParticipantIdentifier.of(peppolParticipantIdValue, ParticipantIdentifier.DEFAULT_SCHEME);
        }
    }

}
