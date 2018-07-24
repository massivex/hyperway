namespace Mx.Hyperway.As2.Util
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    using MimeKit;
    using MimeKit.Cryptography;

    using Mx.Tools;

    using Org.BouncyCastle.Asn1.X509;
    using Org.BouncyCastle.Crypto;
    using Org.BouncyCastle.Pkcs;
    using Org.BouncyCastle.X509;
    using Org.BouncyCastle.X509.Store;

    public class HyperwaySecureMimeContext : DefaultSecureMimeContext
    {
        public HyperwaySecureMimeContext(
            Pkcs12Store store
            ) : base(new X509Context(store))
        {
        }

        //static IX509CertificateDatabase OpenDatabase(string fileName)
        //{
        //    var builder = new SQLiteConnectionStringBuilder();
        //    builder.DateTimeFormat = SQLiteDateFormats.Ticks;
        //    builder.DataSource = fileName;

        //    if (!File.Exists(fileName))
        //        SQLiteConnection.CreateFile(fileName);

        //    var sqlite = new SQLiteConnection(builder.ConnectionString);
        //    sqlite.Open();

        //    return new SqliteCertificateDatabase(sqlite, "password");
        //}
    }

    public class X509Context : IX509CertificateDatabase
    {
        private readonly Pkcs12Store store;

        public X509Context(Pkcs12Store store)
        {
            this.store = store;
        }
        public ICollection GetMatches(IX509Selector selector)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            return;
        }

        public X509CertificateRecord Find(X509Certificate certificate, X509CertificateRecordFields fields)
        {
            throw new NotImplementedException();
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
            foreach (string alias in this.store.Aliases)
            {
                var certs = this.store.GetCertificateChain(alias);
                foreach (var certificate in certs)
                {
                    if (certificate.Certificate.GetCommonName() == mailbox.Address)
                    {
                        var record = new X509CertificateRecord(certificate.Certificate);
                        return new SingletonList<X509CertificateRecord>(record);
                    }
                }

            }

            return new List<X509CertificateRecord>();
        }

        public IEnumerable<X509CertificateRecord> Find(IX509Selector selector, bool trustedOnly, X509CertificateRecordFields fields)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
