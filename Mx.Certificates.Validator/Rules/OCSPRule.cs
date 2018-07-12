using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Certificates.Validator.Rules
{
    using System.Collections;
    using System.IO;
    using System.Net;
    using System.Resources;

    using Mx.Certificates.Validator.Api;
    using Mx.Tools;

    using Org.BouncyCastle.Asn1;
    using Org.BouncyCastle.Asn1.Ocsp;
    using Org.BouncyCastle.Asn1.X509;
    using Org.BouncyCastle.Math;
    using Org.BouncyCastle.Ocsp;
    using Org.BouncyCastle.X509;

    using CertStatus = Org.BouncyCastle.Asn1.Ocsp.CertStatus;

    /**
     * Validation of certificate using OCSP. Requires intermediate certificates.
     */
    public class OCSPRule : ValidatorRule
    {

        private CertificateBucket intermediateCertificates;

        public OCSPRule(CertificateBucket intermediateCertificates)
        {
            this.intermediateCertificates = intermediateCertificates;
        }

        public void validate(X509Certificate certificate) // throws CertificateValidationException
        {
            try
            {
                // Certificates without OCSP information is not subject to OCSP validation.
                if (certificate.GetExtensionValue(X509Extensions.AuthorityInfoAccess) == null)
                {
                    return;
                }

                X509Certificate issuer = this.intermediateCertificates.findBySubject(certificate.IssuerDN);
                if (issuer == null)
                {
                    throw new FailedValidationException(
                        String.Format(
                            "Unable to find issuer certificate '{0}'",
                            certificate.IssuerDN));
                }

                OcspCheckStatus ocspStatus = this.getRevocationStatus(certificate, issuer);
                if (!ocspStatus.Equals(OcspCheckStatus.Good))
                {
                    throw new FailedValidationException(
                        String.Format("Certificate status is reported as {0} by OCSP.", ocspStatus));
                }
            }
            catch (Exception e) when (e is IOException || e is NullReferenceException)
            {
                throw new CertificateValidationException(e.Message, e);
            }
        }

        public OcspCheckStatus getRevocationStatus(
                X509Certificate cert,
                X509Certificate issuer) // throws IOException, CertPathValidatorException
        {
            var uri = this.getOcspUrlFromCertificate(issuer);
            var request = getOcspPackage(cert.SerialNumber, issuer);
            var response = this.getOcspResponse(uri, request);
            return this.parseOcspResponse(response);
        }

        private string getOcspUrlFromCertificate(X509Certificate cert)
        {
            var derId = new DerObjectIdentifier(X509Extensions.AuthorityInfoAccess.Id);
            byte[] extensionValue = cert.GetExtensionValue(derId).GetOctets();

            Asn1Sequence asn1Seq = (Asn1Sequence)Asn1Object.FromByteArray(extensionValue); // AuthorityInfoAccessSyntax
            // Enumeration <?> objects = asn1Seq.Objects;
            string result = null;
            foreach (Asn1Sequence obj in asn1Seq)
            {
                DerObjectIdentifier oid = (DerObjectIdentifier)obj[0]; // accessMethod
                DerTaggedObject location = (DerTaggedObject)obj[1]; // accessLocation

                if (location.TagNo == GeneralName.UniformResourceIdentifier)
                {
                    DerOctetString uri = (DerOctetString)location.GetObject();
                    String str = Encoding.Default.GetString(uri.GetOctets());
                    if (oid.Equals(X509ObjectIdentifiers.IdADOcsp))
                    {
                        result = str;
                        break;
                    }
                }

            }
            //while (objects.hasMoreElements())
            //{
            //    ASN1Sequence obj = (ASN1Sequence)objects.nextElement(); // AccessDescription
            //}

            return result;
        }

        private static byte[] getOcspPackage(BigInteger serialNr, X509Certificate cacert)
        {
            OcspReqGenerator gen = new OcspReqGenerator();
            try
            {
                CertificateID certId = new CertificateID(CertificateID.HashSha1, cacert, serialNr);
                gen.AddRequest(certId);
                gen.SetRequestExtensions(getExtentions());
                OcspReq req;
                req = gen.Generate();
                return req.GetEncoded();
            }
            catch (OcspException e)
            {
                throw new CertificateValidationException(e.Message, e);
            }
        }

        private static X509Extensions getExtentions()
        {
            var millis = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            BigInteger nonce = BigInteger.ValueOf(millis);
            List<DerObjectIdentifier> oids = new List<DerObjectIdentifier>();
            List<X509Extension> values = new List<X509Extension>();

            oids.Add(OcspObjectIdentifiers.PkixOcspNonce);
            values.Add(new X509Extension(false, new DerOctetString(nonce.ToByteArray())));

            return new X509Extensions(oids, values);
        }

        private byte[] getOcspResponse(string url, byte[] ocspRequest)
        {
            HttpWebRequest req = WebRequest.CreateHttp(url);
            req.Method = "POST";
            req.ContentType = "application/ocsp-request";
            using (var rs = req.GetRequestStream())
            {
                rs.Write(ocspRequest, 0, ocspRequest.Length);
            }

            using (var res = req.GetResponse())
            {
                var result = res.GetResponseStream().ToBuffer();
                return result;
            }
        }

        private OcspCheckStatus parseOcspResponse(byte[] raw)
        {
            BasicOcspResp brep;
            OcspResp response = new OcspResp(raw);
            if (response.Status == OcspRespStatus.Unauthorized)
            {
                return OcspCheckStatus.Unauthorized;
            } 
            else if (response.Status != OcspResponseStatus.Successful)
            {
                return OcspCheckStatus.Error;
            }

            brep = (BasicOcspResp)response.GetResponseObject();
            SingleResp[] singleResps = brep.Responses;
            SingleResp singleResp = singleResps[0];
            Object status = singleResp.GetCertStatus();

            if (status == null)
            {
                return OcspCheckStatus.Good;
            }

            if (status is RevokedStatus)
            {
                return OcspCheckStatus.Revoked;
            }

            if (status is UnknownStatus)
            {
                return OcspCheckStatus.Unknown;
            }

            return OcspCheckStatus.Error;
        }
    }

    public enum OcspCheckStatus
    {
        Unauthorized,
        Good,
        Revoked,
        Unknown,
        Error
    }

}
