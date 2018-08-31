namespace Mx.Hyperway.DocumentSniffer.Identifier
{
    using System;
    using System.Linq;

    using Mx.Peppol.Icd;
    using Mx.Peppol.Icd.Api;
    using Mx.Peppol.Icd.Code;

    /// <summary>
    /// Provides a binding between the attributes schemeAgencyId and the corresponding ISO6523 prefix(ICD).
    /// The ENUM is taken from Policy for use of Identifiers version 3.0 dated 2014-02-03.
    /// The ICD's should be 4 digits, a list can be found : http://www.oid-info.com/doc/ICD-list.pdf
    /// <p>
    /// Possible improvements are:
    /// <ul>
    /// <li>Add an attribute with the literal prefix of the organisation identifiers for each scheme.
    /// This would make it easier to identify which scheme an organisation identifier belongs to. This could be
    /// combined with a regexp</li>
    /// </ul>
    /// </p>
    /// </summary>
    public class SchemeId
    {

        private static readonly Icds Icds = Icds.Of(
            PeppolIcd.Values().OfType<IIcd>().ToArray()
        );

        /// <summary>
        /// Tries to find the Party id with the given schemeId
        /// e.g. "ES:VAT" --&gt; ES_VAT
        /// </summary>
        /// <param name="schemeId">textual representation of scheme, i.e. NO:ORGNR</param>
        /// <returns>instance of SchemeId if found</returns>
        public static IIcd Parse(String schemeId)
        {
            return Icds.FindByIdentifier(schemeId);
        }

        /// <summary>
        /// Tries to find the Party id from the ISO652 code
        /// e.g. "9919" --&gt; AT_KUR
        /// </summary>
        /// <param name="code"></param>
        /// <returns>the scheme id if found null otherwise.</returns>
        public static IIcd FromIso6523(String code)
        {
            return Icds.FindByCode(code);
        }
    }

}
