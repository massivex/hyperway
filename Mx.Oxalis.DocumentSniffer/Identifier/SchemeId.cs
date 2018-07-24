namespace Mx.Hyperway.DocumentSniffer.Identifier
{
    using System;
    using System.Linq;

    using Mx.Peppol.Icd;
    using Mx.Peppol.Icd.Api;
    using Mx.Peppol.Icd.Code;

    /**
     * Provides a binding between the attributes schemeAgencyId and the corresponding ISO6523 prefix (ICD).
     * The ENUM is taken from Policy for use of Identifiers version 3.0 dated 2014-02-03.
     * The ICD's should be 4 digits, a list can be found : http://www.oid-info.com/doc/ICD-list.pdf
     * <p>
     * Possible improvements are:
     * <ul>
     * <li>Add an attribute with the literal prefix of the organisation identifiers for each scheme.
     * This would make it easier to identify which scheme an organisation identifier belongs to. This could be
     * combined with a regexp</li>
     * </ul>
     *
     */
    public class SchemeId
    {

        private static readonly Icds ICDS = Icds.of(
            PeppolIcd.Values().OfType<Icd>().ToArray()
        );

        /**
         * Tries to find the Party id with the given schemeId
         * e.g. "ES:VAT" --&gt; ES_VAT
         *
         * @param schemeId textual representation of scheme, i.e. NO:ORGNR
         * @return instance of SchemeId if found
         * @throws IllegalStateException if not found, i.e. unknown scheme
         */
        public static Icd parse(String schemeId)
        {
            return ICDS.findByIdentifier(schemeId);
        }

        /**
         * Tries to find the Party id from the ISO652 code
         * e.g. "9919" --&gt; AT_KUR
         *
         * @param code
         * @return the scheme id if found null otherwise.
         */
        public static Icd fromISO6523(String code)
        {
            return ICDS.findByCode(code);
        }
    }

}
