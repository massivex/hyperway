namespace Mx.Hyperway.As2.Util
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using MimeKit;
    using MimeKit.Cryptography;

    using Mx.Tools;

    using Org.BouncyCastle.Asn1.X509;
    using Org.BouncyCastle.Crypto;
    using Org.BouncyCastle.Pkcs;
    using Org.BouncyCastle.X509;
    using Org.BouncyCastle.X509.Store;

    public class X509Context : IX509CertificateDatabase
    {
        private readonly Pkcs12Store store;

        private List<X509Certificate> certsDb;

        public X509Context(Pkcs12Store store)
        {
            this.store = store;
        }

        private List<X509Certificate> GetCerts()
        {
            if (this.certsDb == null)
            {
                this.certsDb = new List<X509Certificate>();
                lock (this.certsDb)
                {
                    foreach (string alias in this.store.Aliases)
                    {
                        if (alias.StartsWith("CN="))
                        {
                            continue;
                        }
                        var cert = this.store.GetCertificate(alias);
                        this.certsDb.Add(cert.Certificate);
                    }
                }
            }

            return this.certsDb;
        }

        public ICollection GetMatches(IX509Selector selector)
        {
            var list = new Collection<X509Certificate>();
            var certs = this.GetCerts();
            foreach (var cert in certs)
            {
                if (selector.Match(cert))
                {
                    list.Add(cert);
                    return list;

                }
            }

            return list;
        }

        public void Dispose()
        {
        }

        public X509CertificateRecord Find(X509Certificate certificate, X509CertificateRecordFields fields)
        {
            var certs = this.GetCerts();

            var cert = certs.IndexOf(certificate);
            if (cert >= 0)
            {
                var record = new X509CertificateRecord(certificate);
                return record;
            }

            return null;
        }

        public IEnumerable<X509Certificate> FindCertificates(IX509Selector selector)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AsymmetricKeyParameter> FindPrivateKeys(IX509Selector selector)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<X509CertificateRecord> Find(
            MailboxAddress mailbox,
            DateTime now,
            bool requirePrivateKey,
            X509CertificateRecordFields fields)
        {
            var certs = this.GetCerts();
            foreach (var certificate in certs)
            {
                if (certificate.GetCommonName() == mailbox.Address)
                {
                    var record = new X509CertificateRecord(certificate);
                    return new SingletonList<X509CertificateRecord>(record);
                }
            }

            return new List<X509CertificateRecord>();
        }

        public IEnumerable<X509CertificateRecord> Find(IX509Selector selector, bool trustedOnly, X509CertificateRecordFields fields)
        {
            var certs = this.GetCerts();
            foreach (var certificate in certs)
            {
                if (selector.Match(certificate))
                {
                    var record = new X509CertificateRecord(certificate);
                    return new SingletonList<X509CertificateRecord>(record);
                }
            }

            return new List<X509CertificateRecord>();
        }

        public void Add(X509CertificateRecord record)
        {
            throw new NotImplementedException();
        }

        public void Remove(X509CertificateRecord record)
        {
            throw new NotImplementedException();
        }

        public void Update(X509CertificateRecord record, X509CertificateRecordFields fields)
        {
            // throw new NotImplementedException();
        }

        public IEnumerable<X509CrlRecord> Find(X509Name issuer, X509CrlRecordFields fields)
        {
            throw new NotImplementedException();
        }

        public X509CrlRecord Find(X509Crl crl, X509CrlRecordFields fields)
        {
            throw new NotImplementedException();
        }

        public void Add(X509CrlRecord record)
        {
            throw new NotImplementedException();
        }

        public void Remove(X509CrlRecord record)
        {
            throw new NotImplementedException();
        }

        public void Update(X509CrlRecord record)
        {
            throw new NotImplementedException();
        }

        public IX509Store GetCrlStore()
        {
            // Empty Certificate Revocation List
            return new X509CertificateStore();
        }
    }
}