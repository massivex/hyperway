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

        private readonly List<Icd> values;

        public static Icds of(params Icd[] values)
        {
            return new Icds(values);
        }

        private Icds(params IEnumerable<Icd>[] values)
        {
            List<Icd> icds = new List<Icd>();
            foreach (IEnumerable<Icd> v in values)
            {
                icds.AddRange(v);
            }

            this.values = icds;
        }

        public IcdIdentifier parse(String s) // throws PeppolParsingException
        {
            return this.parse(ParticipantIdentifier.parse(s));
        }

        public IcdIdentifier parse(ParticipantIdentifier participantIdentifier) // throws PeppolParsingException
        {
            try
            {
                String[] parts = participantIdentifier.getIdentifier().Split(new [] { ":" }, 2, StringSplitOptions.None);
                return IcdIdentifier.of(
                    this.findBySchemeAndCode(participantIdentifier.getScheme(), parts[0]),
                    parts[1]);
            }
            catch (ArgumentException e)
            {
                throw new PeppolParsingException(e.Message, e);
            }
        }

        public IcdIdentifier parse(String icd, String identifier) // throws PeppolParsingException
        {
            try
            {
                return IcdIdentifier.of(this.findByIdentifier(icd), identifier);
            }
            catch (ArgumentException e)
            {
                throw new PeppolParsingException(e.Message, e);
            }
        }

        public Icd findBySchemeAndCode(Scheme scheme, String code)
        {
            foreach (Icd v in this.values)
                if (v.getCode().Equals(code) && v.getScheme().Equals(scheme))
                    return v;

            throw new ArgumentException($"Value '{scheme}::{code}' is not valid ICD.");
        }

        public Icd findByIdentifier(String identifier)
        {
            foreach (Icd v in this.values)
            {
                if (v.getIdentifier().Equals(identifier))
                {
                    return v;
                }

            }

            throw new ArgumentException($"Value '{identifier}' is not valid ICD.");
        }

        public Icd findByCode(String code)
        {
            foreach (Icd v in this.values)
            {
                if (v.getCode().Equals(code))
                {
                    return v;
                }
            }

            throw new ArgumentException($"Value '{code}' is not valid ICD.");
        }
    }

}
