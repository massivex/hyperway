using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Oxalis.As2.Util
{
    using Mx.Peppol.Common.Code;
    using Mx.Peppol.Common.Model;

    using Org.BouncyCastle.Asn1;
    using Org.BouncyCastle.Asn1.Nist;
    using Org.BouncyCastle.Asn1.Oiw;

    public class SMimeDigestMethod
    {
        // md5("md5", "MD5"),
        // rsa_md5("rsa-md5", "MD5"),
        public static readonly SMimeDigestMethod sha1 = new SMimeDigestMethod(
            new[] { "sha1", "sha-1", "rsa-sha1" },
            "SHA1withRSA",
            "SHA-1",
            OiwObjectIdentifiers.IdSha1,
            DigestMethod.SHA1,
            TransportProfile.AS2_1_0);

        // sha256("sha256", "SHA256withRSA", "SHA-256", NISTObjectIdentifiers.id_sha256, DigestMethod.SHA256, null),
        // sha384("sha384", "SHA-384"),
        public static readonly SMimeDigestMethod sha512 = new SMimeDigestMethod(
            new String[] { "sha512", "sha-512" },
            "SHA512withRSA",
            "SHA-512",
            NistObjectIdentifiers.IdSha512,
            DigestMethod.SHA512,
            TransportProfile.of("busdox-transport-as2-ver1p0r1"));

        private readonly String[] identifier;

        private readonly String method;

        private readonly String algorithm;

        private readonly Asn1Object oid;

        private readonly DigestMethod digestMethod;

        private readonly TransportProfile transportProfile;

        SMimeDigestMethod(
            String[] identifier,
            String method,
            String algorithm,
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

        public String getIdentifier()
        {
            return identifier[0];
        }

        public String getMethod()
        {
            return method;
        }

        public String getAlgorithm()
        {
            return algorithm;
        }

        public Asn1Object getOid()
        {
            return oid;
        }

        public DigestMethod getDigestMethod()
        {
            return digestMethod;
        }

        public TransportProfile getTransportProfile()
        {
            return transportProfile;
        }

        public static IEnumerable<SMimeDigestMethod> values()
        {
            return new[] { sha1, sha512 };
        }
        public static SMimeDigestMethod findByIdentifier(String identifier)
        {
            String provided = identifier.ToLowerInvariant();

            foreach (SMimeDigestMethod digestMethod in values())
            {
                foreach (System.String ident in digestMethod.identifier)
                {
                    if (ident.Equals(provided))
                    {
                        return digestMethod;
                    }
                }
            }

            throw new ArgumentException(System.String.Format("Digest method '{0}' not known.", identifier));
        }

        public static SMimeDigestMethod findByTransportProfile(TransportProfile transportProfile)
        {
            foreach (SMimeDigestMethod digestMethod in values())
            {
                if (digestMethod.transportProfile.Equals(transportProfile))
                {
                    return digestMethod;
                }
            }

            throw new ArgumentException(
                System.String.Format("Digest method for transport profile '{0}' not known.", transportProfile));
        }

        public static SMimeDigestMethod findByDigestMethod(DigestMethod digestMethod)
        {
            foreach (SMimeDigestMethod method in values())
            {
                if (method.digestMethod.Equals(digestMethod))
                {
                    return method;
                }
            }

            throw new ArgumentException(System.String.Format("Digest method '{0}' not known.", digestMethod));
        }
    }
}
