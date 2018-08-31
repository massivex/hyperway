using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Peppol.Icd.Code
{
    using Mx.Peppol.Common.Model;
    using Mx.Peppol.Icd.Api;

    public class GenericIcd : IIcd
    {
        public static IIcd Of(string identifier, string code, Scheme scheme)
        {
            return new GenericIcd(identifier, code, scheme, null);
        }

        public static IIcd Of(string identifier, string code, Scheme scheme, string issuingAgency)
        {
            return new GenericIcd(identifier, code, scheme, issuingAgency);
        }

        private GenericIcd(string identifier, string code, Scheme scheme, string issuingAgency)
        {
            this.Identifier = identifier;
            this.Code = code;
            this.Scheme = scheme;
            this.IssuingAgency = issuingAgency;
        }

        public string Identifier { get; }

        public string Code { get; }

        public Scheme Scheme { get; }

        public string IssuingAgency { get; }
    }
}
