using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Certificates.Validator.Util
{
    using System.Collections;
    using System.IO;
    using System.Linq;

    using Mx.Certificates.Validator.Api;

    using Org.BouncyCastle.Asn1.X509;
    using Org.BouncyCastle.Pkcs;
    using Org.BouncyCastle.X509;

    /**
     * Reads a keystore from input stream and keeps it in memory.
     */
    public class KeyStoreCertificateBucket : CertificateBucket
    {

        protected Pkcs12Store keyStore;

        public KeyStoreCertificateBucket(Pkcs12Store keyStore)
        {
            this.keyStore = keyStore;
        }

        public KeyStoreCertificateBucket(Stream inputStream, String password) : this("PCKS12", inputStream, password)
        {
            
        }

        public KeyStoreCertificateBucket(
            String type,
            Stream inputStream,
            String password) // throws CertificateBucketException
        {
            try
            {
                // keyStore = KeyStore.getInstance(type);
                // keyStore.load(inputStream, password.toCharArray());
                this.keyStore = new Pkcs12Store(inputStream, password.ToCharArray());
                inputStream.Close();
                inputStream.Dispose();
            }
            catch (Exception e)
            {
                throw new CertificateBucketException(e.Message, e);
            }
        }

        public X509Certificate findBySubject(X509Name principal) // throws CertificateBucketException
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

        //public Iterator<X509Certificate> iterator()
        //{
        //    try
        //    {
        //        final KeyStore keyStore = getKeyStore();
        //        final Enumeration<String>
        //        aliases = keyStore.aliases();

        //        return new Iterator<X509Certificate>()
        //                   {
        //                       @Override
        //                       public boolean hasNext() { return aliases.hasMoreElements();
        //                   }

        //        @Override

        //        public X509Certificate next()
        //        {
        //            try
        //            {
        //                return (X509Certificate)keyStore.getCertificate(aliases.nextElement());
        //            }
        //            catch (KeyStoreException |

        //            NoSuchElementException e) {
        //                throw new IllegalStateException(e.getMessage(), e);
        //            }
        //        }

        //        @Override

        //        public void remove()
        //        {
        //            // No action.
        //        }

        //        };
        //    }
        //    catch (Exception e)
        //    {
        //        throw new IllegalStateException(e.getMessage(), e);
        //    }
        //}

        /**
         * Adding certificates identified by aliases from key store to a SimpleCertificateBucket.
         */
        public void toSimple(
            SimpleCertificateBucket certificates,
            params String[] aliases) // throws CertificateBucketException
        {
            try
            {
                List<String> aliasesList = aliases.ToList();

                Pkcs12Store keyStore = getKeyStore();
                IEnumerator aliasesEnumeration = keyStore.Aliases.GetEnumerator();
                while (aliasesEnumeration.MoveNext())
                {
                    String alias = (string) aliasesEnumeration.Current;
                    if (aliasesList.Contains(alias))
                    {
                        certificates.add(keyStore.GetCertificate(alias).Certificate);
                    }
                }
            }
            catch (Exception e)
            {
                throw new CertificateBucketException(e.Message, e);
            }
        }

        /**
         * Create a new SimpleCertificateBucket and adding certificates based on aliases.
         */
        public SimpleCertificateBucket toSimple(params String[] aliases) // throws CertificateBucketException
        {
            SimpleCertificateBucket certificates = new SimpleCertificateBucket();
            toSimple(certificates, aliases);
            return certificates;
        }

        /**
         * Adding certificates identified by prefix(es) from key store to a SimpleCertificateBucket.
         */
        public void startsWith(
            SimpleCertificateBucket certificates,
            String[] prefix) // throws CertificateBucketException
        {
            try
            {
                Pkcs12Store keyStore = getKeyStore();
                IEnumerator aliasesEnumeration = keyStore.Aliases.GetEnumerator();
                while (aliasesEnumeration.MoveNext())
                {
                    String alias = (string) aliasesEnumeration.Current;
                    foreach (String p in prefix)
                    {
                        if (alias.StartsWith(p))
                        {
                            certificates.add(keyStore.GetCertificate(alias).Certificate);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new CertificateBucketException(e.Message, e);
            }
        }

        /**
         * Create a new SimpleCertificateBucket and adding certificates based on prefix(es).
         */
        public SimpleCertificateBucket startsWith(params String[] prefix) // throws CertificateBucketException
        {
            SimpleCertificateBucket certificates = new SimpleCertificateBucket();
            startsWith(certificates, prefix);
            return certificates;
        }

        /**
         * Allows for overriding method of fetching key store when used.
         */
        protected Pkcs12Store getKeyStore() // throws CertificateBucketException
        {
            return keyStore;
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
