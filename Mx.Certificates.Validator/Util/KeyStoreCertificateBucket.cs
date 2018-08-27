using System;
using System.Collections.Generic;

namespace Mx.Certificates.Validator.Util
{
    using System.Collections;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;

    using Mx.Certificates.Validator.Api;

    using Org.BouncyCastle.Asn1.X509;
    using Org.BouncyCastle.Pkcs;
    using Org.BouncyCastle.X509;

    /// <summary>
    /// Reads a keystore from input stream and keeps it in memory. 
    /// </summary>
    public class KeyStoreCertificateBucket : ICertificateBucket
    {

        protected Pkcs12Store KeyStore;

        public KeyStoreCertificateBucket(Pkcs12Store keyStore)
        {
            this.KeyStore = keyStore;
        }

        public KeyStoreCertificateBucket(Stream inputStream, string password) : this("PCKS12", inputStream, password)
        {
            
        }

        public KeyStoreCertificateBucket(
            string type,
            Stream inputStream,
            string password)
        {
            try
            {
                if (type != "PCKS12")
                {
                    throw new NotSupportedException($"Store type {type} not supported");
                }
                this.KeyStore = new Pkcs12Store(inputStream, password.ToCharArray());
                inputStream.Close();
                inputStream.Dispose();
            }
            catch (Exception e)
            {
                throw new CertificateBucketException(e.Message, e);
            }
        }

        public X509Certificate FindBySubject(X509Name principal)
        {
            foreach (X509Certificate certificate in this)
            {
                if (certificate.SubjectDN.Equals(principal))
                {
                    return certificate;
                }
            }

            return null;
        }


        /// <summary>
        /// Adding certificates identified by aliases from key store to a SimpleCertificateBucket. 
        /// </summary>
        /// <param name="certificates"></param>
        /// <param name="aliases"></param>
        public void ToSimple(SimpleCertificateBucket certificates, params string[] aliases)
        {
            try
            {
                List<string> aliasesList = aliases.ToList();

                Pkcs12Store keyStore = this.GetKeyStore();
                IEnumerator aliasesEnumeration = keyStore.Aliases.GetEnumerator();
                while (aliasesEnumeration.MoveNext())
                {
                    string alias = (string) aliasesEnumeration.Current;
                    if (aliasesList.Contains(alias))
                    {
                        certificates.Add(keyStore.GetCertificate(alias).Certificate);
                    }
                }
            }
            catch (Exception e)
            {
                throw new CertificateBucketException(e.Message, e);
            }
        }

        /// <summary>
        /// Create a new SimpleCertificateBucket and adding certificates based on aliases.
        /// </summary>
        public SimpleCertificateBucket ToSimple(params string[] aliases)
        {
            SimpleCertificateBucket certificates = new SimpleCertificateBucket();
            this.ToSimple(certificates, aliases);
            return certificates;
        }

        /// <summary>
        /// Adding certificates identified by prefix(es) from key store to a SimpleCertificateBucket.
        /// </summary>
        public void StartsWith(SimpleCertificateBucket certificates, string[] prefix)
        {
            try
            {
                Pkcs12Store keyStore = this.GetKeyStore();
                IEnumerator aliasesEnumeration = keyStore.Aliases.GetEnumerator();
                while (aliasesEnumeration.MoveNext())
                {
                    string alias = (string) aliasesEnumeration.Current;
                    foreach (string p in prefix)
                    {
                        Debug.Assert(alias != null, nameof(alias) + " != null");
                        if (alias.StartsWith(p))
                        {
                            certificates.Add(keyStore.GetCertificate(alias).Certificate);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new CertificateBucketException(e.Message, e);
            }
        }

        /// <summary>
        /// Create a new SimpleCertificateBucket and adding certificates based on prefix(es).
        /// </summary>
        public SimpleCertificateBucket StartsWith(params string[] prefix)
        {
            SimpleCertificateBucket certificates = new SimpleCertificateBucket();
            this.StartsWith(certificates, prefix);
            return certificates;
        }

        /// <summary>
        /// Allows for overriding method of fetching key store when used.
        /// </summary>
        protected Pkcs12Store GetKeyStore()
        {
            return this.KeyStore;
        }

        public IEnumerator<X509Certificate> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }

}
