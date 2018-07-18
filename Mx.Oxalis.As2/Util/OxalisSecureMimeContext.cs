using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Oxalis.As2.Util
{
    using System.Collections;

    using MimeKit;
    using MimeKit.Cryptography;

    using Org.BouncyCastle.Asn1.X509;
    using Org.BouncyCastle.Crypto;
    using Org.BouncyCastle.X509;
    using Org.BouncyCastle.X509.Store;

    public class OxalisSecureMimeContext : DefaultSecureMimeContext
    {
        public OxalisSecureMimeContext() : base(new X509Context())
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
        public ICollection GetMatches(IX509Selector selector)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
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

        public IEnumerable<X509CertificateRecord> Find(MailboxAddress mailbox, DateTime now, bool requirePrivateKey, X509CertificateRecordFields fields)
        {
            throw new NotImplementedException();
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
