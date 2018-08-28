namespace Mx.Hyperway.As2.Util
{
    using System;
    using System.Collections.Generic;

    using Mx.Peppol.Common.Code;
    using Mx.Peppol.Common.Model;

    using Org.BouncyCastle.Asn1;
    using Org.BouncyCastle.Asn1.Nist;
    using Org.BouncyCastle.Asn1.Oiw;

    public class SMimeDigestMethod
    {
        public static readonly SMimeDigestMethod Sha1 = new SMimeDigestMethod(
            new[] { "sha1", "sha-1", "rsa-sha1" },
            "SHA1withRSA",
            "SHA-1",
            OiwObjectIdentifiers.IdSha1,
            DigestMethod.Sha1,
            TransportProfile.AS2_1_0);

        public static readonly SMimeDigestMethod Sha512 = new SMimeDigestMethod(
            new[] { "sha512", "sha-512" },
            "SHA512withRSA",
            "SHA-512",
            NistObjectIdentifiers.IdSha512,
            DigestMethod.Sha512,
            TransportProfile.Of("busdox-transport-as2-ver1p0r1"));

        private readonly string[] identifier;

        private readonly string method;

        private readonly string algorithm;

        private readonly Asn1Object oid;

        private readonly DigestMethod digestMethod;

        private readonly TransportProfile transportProfile;

        SMimeDigestMethod(
            string[] identifier,
            string method,
            string algorithm,
            Asn1Object oid,
            DigestMethod digestMethod,
            TransportProfile transportProfile)
        {

            this.identifier = identifier;
            this.method = method;
            this.algorithm = algorithm;
            this.oid = oid;
            this.digestMethod = digestMethod;
            this.transportProfile = transportProfile;
        }

        public string GetIdentifier()
        {
            return this.identifier[0];
        }

        public string GetMethod()
        {
            return this.method;
        }

        public string GetAlgorithm()
        {
            return this.algorithm;
        }

        public Asn1Object GetOid()
        {
            return this.oid;
        }

        public DigestMethod GetDigestMethod()
        {
            return this.digestMethod;
        }

        public TransportProfile GetTransportProfile()
        {
            return this.transportProfile;
        }

        public static IEnumerable<SMimeDigestMethod> Values()
        {
            return new[] { Sha1, Sha512 };
        }
        public static SMimeDigestMethod FindByIdentifier(string identifier)
        {
            string provided = identifier.ToLowerInvariant();

            foreach (SMimeDigestMethod digestMethod in Values())
            {
                foreach (string ident in digestMethod.identifier)
                {
                    if (ident.Equals(provided))
                    {
                        return digestMethod;
                    }
                }
            }

            throw new ArgumentException($"Digest method '{identifier}' not known.");
        }

        public static SMimeDigestMethod FindByTransportProfile(TransportProfile transportProfile)
        {
            foreach (SMimeDigestMethod digestMethod in Values())
            {
                if (digestMethod.transportProfile.Equals(transportProfile))
                {
                    return digestMethod;
                }
            }

            throw new ArgumentException($"Digest method for transport profile '{transportProfile}' not known.");
        }

        public static SMimeDigestMethod FindByDigestMethod(DigestMethod digestMethod)
        {
            foreach (SMimeDigestMethod method in Values())
            {
                if (method.digestMethod.Equals(digestMethod))
                {
                    return method;
                }
            }

            throw new ArgumentException(string.Format("Digest method '{0}' not known.", digestMethod));
        }
    }
}
