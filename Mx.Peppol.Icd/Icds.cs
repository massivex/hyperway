using System;
using System.Collections.Generic;

namespace Mx.Peppol.Icd
{
    using Mx.Peppol.Common.Lang;
    using Mx.Peppol.Common.Model;
    using Mx.Peppol.Icd.Api;
    using Mx.Peppol.Icd.Model;

    public class Icds
    {
        private readonly List<IIcd> values;

        public static Icds Of(params IIcd[] values)
        {
            return new Icds(values);
        }

        private Icds(params IEnumerable<IIcd>[] values)
        {
            List<IIcd> icds = new List<IIcd>();
            foreach (IEnumerable<IIcd> v in values)
            {
                icds.AddRange(v);
            }

            this.values = icds;
        }

        public IcdIdentifier Parse(string s)
        {
            return this.Parse(ParticipantIdentifier.Parse(s));
        }

        public IcdIdentifier Parse(ParticipantIdentifier participantIdentifier)
        {
            try
            {
                string[] parts = participantIdentifier.Identifier.Split(new [] { ":" }, 2, StringSplitOptions.None);
                return IcdIdentifier.Of(
                    this.FindBySchemeAndCode(participantIdentifier.Scheme, parts[0]),
                    parts[1]);
            }
            catch (ArgumentException e)
            {
                throw new PeppolParsingException(e.Message, e);
            }
        }

        public IcdIdentifier Parse(string icd, string identifier)
        {
            try
            {
                return IcdIdentifier.Of(this.FindByIdentifier(icd), identifier);
            }
            catch (ArgumentException e)
            {
                throw new PeppolParsingException(e.Message, e);
            }
        }

        public IIcd FindBySchemeAndCode(Scheme scheme, string code)
        {
            foreach (IIcd v in this.values)
                if (v.Code.Equals(code) && v.Scheme.Equals(scheme))
                    return v;

            throw new ArgumentException($"Value '{scheme}::{code}' is not valid ICD.");
        }

        public IIcd FindByIdentifier(string identifier)
        {
            foreach (IIcd v in this.values)
            {
                if (v.Identifier.Equals(identifier))
                {
                    return v;
                }

            }

            throw new ArgumentException($"Value '{identifier}' is not valid ICD.");
        }

        public IIcd FindByCode(string code)
        {
            foreach (IIcd v in this.values)
            {
                if (v.Code.Equals(code))
                {
                    return v;
                }
            }

            throw new ArgumentException($"Value '{code}' is not valid ICD.");
        }
    }

}
