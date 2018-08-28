using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Peppol.Icd.Code
{
    using System.ComponentModel;

    using Mx.Peppol.Common.Model;
    using Mx.Peppol.Icd.Api;

    public class PeppolIcd : Icd
    {

        // @Deprecated
        // NO_VAT("NO:VAT", "9909", "Norwegian VAT number (Deprecated)")

        public static readonly PeppolIcd FR_SIRENE = new PeppolIcd(
            "FR:SIRENE",
            "0002",
            "Institut National de la Statistique et des Etudes Economiques, (I.N.S.E.E.)");

        public static readonly PeppolIcd SE_ORGNR = new PeppolIcd("SE:ORGNR", "0007", "The National Tax Board");

        public static readonly PeppolIcd FR_SIRET = new PeppolIcd("FR:SIRET", "0009", "DU PONT DE NEMOURS");

        public static readonly PeppolIcd FI_OVT = new PeppolIcd(
            "FI:OVT",
            "0037",
            "National Board of Taxes, (Verohallitus)");

        public static readonly PeppolIcd DUNS = new PeppolIcd("DUNS", "0060", "Dun and Bradstreet Ltd");

        public static readonly PeppolIcd GLN = new PeppolIcd("GLN", "0088", "GS1 GLN");

        public static readonly PeppolIcd DK_P = new PeppolIcd("DK:P", "0096", "Danish Chamber of Commerce");

        public static readonly PeppolIcd IT_FTI = new PeppolIcd("IT:FTI", "0097", "FTI - Ediforum Italia");

        public static readonly PeppolIcd NL_KVK = new PeppolIcd(
            "NL:KVK",
            "0106",
            "Vereniging van Kamers van Koophandel en Fabrieken in Nederland, Scheme");

        public static readonly PeppolIcd IT_SIA = new PeppolIcd(
            "IT:SIA",
            "0135",
            "SIA-Società Interbancaria per l'Automazione S.p.A.");

        public static readonly PeppolIcd IT_SECETI = new PeppolIcd(
            "IT:SECETI",
            "0142",
            "Servizi Centralizzati SECETI S.p.A.");

        public static readonly PeppolIcd DIGST = new PeppolIcd("DIGST", "0184", "DIGSTORG");

        public static readonly PeppolIcd DK_CPR = new PeppolIcd(
            "DK:CPR",
            "9901",
            "Danish Ministry of the Interior and Health");

        public static readonly PeppolIcd DK_CVR = new PeppolIcd(
            "DK:CVR",
            "9902",
            "The Danish Commerce and Companies Agency");

        public static readonly PeppolIcd DK_SE = new PeppolIcd(
            "DK:SE",
            "9904",
            "Danish Ministry of Taxation, Central Customs and Tax Administration");

        public static readonly PeppolIcd DK_VANS = new PeppolIcd("DK:VANS", "9905", "Danish VANS providers");

        public static readonly PeppolIcd IT_VAT = new PeppolIcd(
            "IT:VAT",
            "9906",
            "Ufficio responsabile gestione partite IVA");

        public static readonly PeppolIcd IT_CF = new PeppolIcd("IT:CF", "9907", "TAX Authority");

        public static readonly PeppolIcd NO_ORGNR = new PeppolIcd(
            "NO:ORGNR",
            "9908",
            "Enhetsregisteret ved Bronnoysundregisterne");

        public static readonly PeppolIcd HU_VAT = new PeppolIcd("HU:VAT", "9910", "Hungarian VAT number");

        public static readonly PeppolIcd EU_REID = new PeppolIcd("EU:REID", "9913", "Business Registers Network");

        public static readonly PeppolIcd AT_VAT = new PeppolIcd(
            "AT:VAT",
            "9914",
            "Österreichische Umsatzsteuer-Identifikationsnummer");

        public static readonly PeppolIcd AT_GOV = new PeppolIcd(
            "AT:GOV",
            "9915",
            "Österreichisches Verwaltungs bzw. Organisationskennzeichen");

        public static readonly PeppolIcd IS_KT = new PeppolIcd("IS:KT", "9917", "Icelandic National Registry");

        public static readonly PeppolIcd IBAN = new PeppolIcd(
            "IBAN",
            "9918",
            "SOCIETY FOR WORLDWIDE INTERBANK FINANCIAL, TELECOMMUNICATION S.W.I.F.T");

        public static readonly PeppolIcd AT_KUR = new PeppolIcd(
            "AT:KUR",
            "9919",
            "Kennziffer des Unternehmensregisters");

        public static readonly PeppolIcd ES_VAT = new PeppolIcd(
            "ES:VAT",
            "9920",
            "Agencia Española de Administración Tributaria");

        public static readonly PeppolIcd IT_IPA = new PeppolIcd(
            "IT:IPA",
            "9921",
            "Indice delle Pubbliche Amministrazioni");

        public static readonly PeppolIcd AD_VAT = new PeppolIcd("AD:VAT", "9922", "Andorra VAT number");

        public static readonly PeppolIcd AL_VAT = new PeppolIcd("AL:VAT", "9923", "Albania VAT number");

        public static readonly PeppolIcd BA_VAT = new PeppolIcd("BA:VAT", "9924", "Bosnia and Herzegovina VAT number");

        public static readonly PeppolIcd BE_VAT = new PeppolIcd("BE:VAT", "9925", "Belgium VAT number");

        public static readonly PeppolIcd BG_VAT = new PeppolIcd("BG:VAT", "9926", "Bulgaria VAT number");

        public static readonly PeppolIcd CH_VAT = new PeppolIcd("CH:VAT", "9927", "Switzerland VAT number");

        public static readonly PeppolIcd CY_VAT = new PeppolIcd("CY:VAT", "9928", "Cyprus VAT number");

        public static readonly PeppolIcd CZ_VAT = new PeppolIcd("CZ:VAT", "9929", "Czech Republic VAT number");

        public static readonly PeppolIcd DE_VAT = new PeppolIcd("DE:VAT", "9930", "Germany VAT number");

        public static readonly PeppolIcd EE_VAT = new PeppolIcd("EE:VAT", "9931", "Estonia VAT number");

        public static readonly PeppolIcd GB_VAT = new PeppolIcd("GB:VAT", "9932", "United Kingdom VAT number");

        public static readonly PeppolIcd GR_VAT = new PeppolIcd("GR:VAT", "9933", "Greece VAT number");

        public static readonly PeppolIcd HR_VAT = new PeppolIcd("HR:VAT", "9934", "Croatia VAT number");

        public static readonly PeppolIcd IE_VAT = new PeppolIcd("IE:VAT", "9935", "Ireland VAT number");

        public static readonly PeppolIcd LI_VAT = new PeppolIcd("LI:VAT", "9936", "Liechtenstein VAT number");

        public static readonly PeppolIcd LT_VAT = new PeppolIcd("LT:VAT", "9937", "Lithuania VAT number");

        public static readonly PeppolIcd LU_VAT = new PeppolIcd("LU:VAT", "9938", "Luxemburg VAT number");

        public static readonly PeppolIcd LV_VAT = new PeppolIcd("LV:VAT", "9939", "Latvia VAT number");

        public static readonly PeppolIcd MC_VAT = new PeppolIcd("MC:VAT", "9940", "Monaco VAT number");

        public static readonly PeppolIcd ME_VAT = new PeppolIcd("ME:VAT", "9941", "Montenegro VAT number");

        public static readonly PeppolIcd MK_VAT = new PeppolIcd(
            "MK:VAT",
            "9942",
            "Macedonia, the former Yugoslav Republic of VAT number");

        public static readonly PeppolIcd MT_VAT = new PeppolIcd("MT:VAT", "9943", "Malta VAT number");

        public static readonly PeppolIcd NL_VAT = new PeppolIcd("NL:VAT", "9944", "Netherlands VAT number");

        public static readonly PeppolIcd PL_VAT = new PeppolIcd("PL:VAT", "9945", "Poland VAT number");

        public static readonly PeppolIcd PT_VAT = new PeppolIcd("PT:VAT", "9946", "Portugal VAT number");

        public static readonly PeppolIcd RO_VAT = new PeppolIcd("RO:VAT", "9947", "Romania VAT number");

        public static readonly PeppolIcd RS_VAT = new PeppolIcd("RS:VAT", "9948", "Serbia VAT number");

        public static readonly PeppolIcd SI_VAT = new PeppolIcd("SI:VAT", "9949", "Slovenia VAT number");

        public static readonly PeppolIcd SK_VAT = new PeppolIcd("SK:VAT", "9950", "Slovakia VAT number");

        public static readonly PeppolIcd SM_VAT = new PeppolIcd("SM:VAT", "9951", "San Marino VAT number");

        public static readonly PeppolIcd TR_VAT = new PeppolIcd("TR:VAT", "9952", "Turkey VAT number");

        public static readonly PeppolIcd VA_VAT = new PeppolIcd(
            "VA:VAT",
            "9953",
            "Holy See (Vatican City State) VAT number");

        public static readonly PeppolIcd NL_ION = new PeppolIcd(
            "NL:OIN",
            "9954",
            "Dutch Originator's Identification Number");

        public static readonly PeppolIcd SE_VAT = new PeppolIcd("SE:VAT", "9955", "Swedish VAT number");

        public static readonly PeppolIcd BE_CBE = new PeppolIcd(
            "BE:CBE",
            "9956",
            "Belgian Crossroad Bank of Enterprises");

        public static readonly PeppolIcd FR_VAT = new PeppolIcd("FR:VAT", "9957", "French VAT number");

        private static readonly Scheme SCHEME = Scheme.Of("iso6523-actorid-upis");

        private static List<PeppolIcd> allIcds;

        private readonly String identifier;

        private readonly String code;

        private readonly String issuingAgency;

        private PeppolIcd(String identifier, String code, String issuingAgency)
        {
            this.identifier = identifier;
            this.code = code;
            this.issuingAgency = issuingAgency;
        }

        public String getIdentifier()
        {
            return identifier;
        }

        public String getCode()
        {
            return code;
        }

        public Scheme getScheme()
        {
            return SCHEME;
        }

        public String getIssuingAgency()
        {
            return issuingAgency;
        }

        public static List<PeppolIcd> Values()
        {
            if (allIcds == null)
            {
                allIcds = new List<PeppolIcd>()
                              {
                                  FR_SIRENE,
                                  SE_ORGNR,
                                  FR_SIRET,
                                  FI_OVT,
                                  DUNS,
                                  GLN,
                                  DK_P,
                                  IT_FTI,
                                  NL_KVK,
                                  IT_SIA,
                                  IT_SECETI,
                                  DIGST,
                                  DK_CPR,
                                  DK_CVR,
                                  DK_SE,
                                  DK_VANS,
                                  IT_VAT,
                                  IT_CF,
                                  NO_ORGNR,
                                  HU_VAT,
                                  EU_REID,
                                  AT_VAT,
                                  AT_GOV,
                                  IS_KT,
                                  IBAN,
                                  AT_KUR,
                                  ES_VAT,
                                  IT_IPA,
                                  AD_VAT,
                                  AL_VAT,
                                  BA_VAT,
                                  BE_VAT,
                                  BG_VAT,
                                  CH_VAT,
                                  CY_VAT,
                                  CZ_VAT,
                                  DE_VAT,
                                  EE_VAT,
                                  GB_VAT,
                                  GR_VAT,
                                  HR_VAT,
                                  IE_VAT,
                                  LI_VAT,
                                  LT_VAT,
                                  LU_VAT,
                                  LV_VAT,
                                  MC_VAT,
                                  ME_VAT,
                                  MK_VAT,
                                  MT_VAT,
                                  NL_VAT,
                                  PL_VAT,
                                  PT_VAT,
                                  RO_VAT,
                                  RS_VAT,
                                  SI_VAT,
                                  SK_VAT,
                                  SM_VAT,
                                  TR_VAT,
                                  VA_VAT,
                                  NL_ION,
                                  SE_VAT,
                                  BE_CBE,
                                  FR_VAT
                              };
            }

            return allIcds;
        }

        public static Icd findByCode(String icd)
        {
            foreach (PeppolIcd v in Values())
            {
                if (v.code.Equals(icd))
                {
                    return v;
                }
            }

            throw new ArgumentException($"Value '{icd}' is not valid ICD.");
        }

        public static Icd findByIdentifier(String icd)
        {
            foreach (PeppolIcd v in Values())
            {
                if (v.identifier.Equals(icd))
                {
                    return v;
                }
            }

            throw new ArgumentException($"Identifier '{icd}' is not valid ICD.");
        }
    }
}