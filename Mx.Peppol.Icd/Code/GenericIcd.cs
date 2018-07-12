using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Peppol.Icd.Code
{
    using Mx.Peppol.Common.Model;
    using Mx.Peppol.Icd.Api;

    public class GenericIcd : Icd
    {

        private readonly String identifier;

        private readonly String code;

        private readonly Scheme scheme;

        private readonly String issuingAgency;

        public static Icd of(String identifier, String code, Scheme scheme)
        {
            return new GenericIcd(identifier, code, scheme, null);
        }

        public static Icd of(String identifier, String code, Scheme scheme, String issuingAgency)
        {
            return new GenericIcd(identifier, code, scheme, issuingAgency);
        }

        private GenericIcd(String identifier, String code, Scheme scheme, String issuingAgency)
        {
            this.identifier = identifier;
            this.code = code;
            this.scheme = scheme;
            this.issuingAgency = issuingAgency;
        }

        public string getIdentifier()
        {
            return identifier;
        }


        public String getCode()
        {
            return code;
        }

        public Scheme getScheme()
        {
            return scheme;
        }

        public String getIssuingAgency()
        {
            return null;
        }
    }
}
