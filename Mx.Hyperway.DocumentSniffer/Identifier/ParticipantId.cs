namespace Mx.Hyperway.DocumentSniffer.Identifier
{
    using System;
    using System.Text.RegularExpressions;

    using Mx.Hyperway.DocumentSniffer.Lang;
    using Mx.Peppol.Common.Model;
    using Mx.Peppol.Icd.Api;

    public class ParticipantId
    {

        static readonly Regex Iso6523Pattern = new Regex("^(\\d{4}):([^\\s]+)$", RegexOptions.Compiled);

        // max length for international organisation number
        static readonly int InternationOrgIdMaxLength = 50;

        // Holds the textual representation of this PEPPOL participant id
        private readonly string peppolParticipantIdValue;

        /// <summary>
        /// Constructs a new instance based upon a match of the following patterns :
        /// <ol>
        /// <li>{@code xxxx:yyyyyy} - i.e.a 4 digit ICD followed by a ':' followed by the organisationID</li>
        /// <li>AB999999999 - i.e.a prefix of at least two characters followed by something</li>
        /// </ol>
        /// </summary>
        /// <param name="participantId">participant Id represented as a string</param>
        public ParticipantId(string participantId)
        {
            this.peppolParticipantIdValue = Parse(participantId);
        }

        /// <summary>
        /// Uses combination of SchemeId and Organisation identifier to create new instance.
        /// The Organisation identifier is validated in accordance with the rules of the scheme.
        /// </summary>
        /// <param name="schemeId"></param>
        /// <param name="organisationId"></param>
        public ParticipantId(Icd schemeId, string organisationId)
        {

            if (schemeId == null)
            {
                throw new ArgumentException("SchemeId must be specified with a a valid ISO6523 code.");
            }

            if (organisationId == null)
            {
                throw new ArgumentException("The organisation id must be specified.");
            }

            if (organisationId.Length > InternationOrgIdMaxLength)
            {
                throw new ArgumentException(
                    string.Format(
                        "Invalid organisation id. '{0}' is longer than {1} characters",
                        organisationId,
                        InternationOrgIdMaxLength));
            }

            // Formats the organisation identifier in accordance with what PEPPOL expects.
            this.peppolParticipantIdValue = string.Format("{0}:{1}", schemeId.getCode(), organisationId);
        }

        /// <summary>
        /// Parses the input string assuming it represents an organisation number or PEPPOL participant identifier in one
        /// of these forms:
        /// <ol>
        /// <li>icd +':' + organisation identifier</li>
        /// <li>National organisation number with at least two character prefix.</li>
        /// </ol>
        /// <p>
        /// After parsing, the organisation identifier is validated in accordance with the rules of the scheme a
        /// validator is found.
        /// </p>
        /// </summary>
        /// <param name="participantId">the string representing the participant identifier or organisation identifier</param>
        /// <returns>a string on the form [ISO6523 ICD]:[participantId];</returns>
        static string Parse(string participantId) // throws InvalidPeppolParticipantException
        {
            string organisationId = participantId.Trim().Replace("\\s", ""); // Squeezes out any white spaces
            Icd schemeId = null;

            MatchCollection matches = Iso6523Pattern.Matches(organisationId);

            if (matches.Count == 0)
            {
                throw new InvalidPeppolParticipantException(string.Format("ICD not found in '{0}'.", participantId));
            }

            // If the representation is in the form xxxx:yyyyyyyyy, we are good
            string icd = matches[0].Groups[1].Value;
            organisationId = matches[0].Groups[2].Value;

            try
            {
                schemeId = SchemeId.FromIso6523(icd); // Locates the associated scheme
            }
            catch (ArgumentException)
            {
                // No action.
            }

            if (schemeId == null)
                throw new InvalidPeppolParticipantException("ICD " + icd + " is unknown");

            // Constructs the textual representation of the PEPPOL participant identifier
            return $"{schemeId.getCode()}:{organisationId}";
        }

        /// <summary>
        /// Parses the provided participant identifier into a validated instance
        /// of {@link ParticipantId}
        /// </summary>
        /// <param name="participantId">The organisation number as xxxx:yyyy or just an organisation number</param>
        /// <returns>validated instance of Participant Id</returns>
        public static ParticipantId ValueOf(string participantId)
        {
            return new ParticipantId(Parse(participantId.Trim()));

        }

        /// <summary>
        /// Simple syntax verifier, verifies icd prefix + code 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsValidParticipantIdentifierPattern(string value)
        {
            if (value == null) return false;

            Match matcher = Iso6523Pattern.Match(value);
            return matcher.Success;
        }


        public override bool Equals(object o)
        {
            if (this == o) return true;
            if (!(o is ParticipantId)) return false;

            ParticipantId that = (ParticipantId)o;
            if (this.peppolParticipantIdValue != null
                    ? !this.peppolParticipantIdValue.Equals(that.peppolParticipantIdValue)
                    : that.peppolParticipantIdValue != null)
            {
                return false;
            }

            return true;
        }


        public override int GetHashCode()
        {
            return this.peppolParticipantIdValue != null ? this.peppolParticipantIdValue.GetHashCode() : 0;
        }

        public string StringValue()
        {
            return this.peppolParticipantIdValue;
        }


        public override string ToString()
        {
            return this.peppolParticipantIdValue;
        }

        public ParticipantIdentifier ToVefa()
        {
            return ParticipantIdentifier.Of(this.peppolParticipantIdValue, ParticipantIdentifier.DefaultScheme);
        }
    }
}
