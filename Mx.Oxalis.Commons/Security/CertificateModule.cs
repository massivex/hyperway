using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Oxalis.Commons.Security
{
    using System.ComponentModel;
    using System.IO;

    using Autofac;

    using Mx.Oxalis.Api.Lang;
    using Mx.Peppol.Mode.Configuration;

    using Org.BouncyCastle.Crypto;
    using Org.BouncyCastle.Pkcs;
    using Org.BouncyCastle.X509;

    public class CertificateModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(KeyStoreProvider)
                .As<Pkcs12Store>()
                .InstancePerLifetimeScope();

            builder.Register(PrivateKeyProvider)
                .As<AsymmetricKeyParameter>()
                .InstancePerLifetimeScope();

            builder.Register(CertificateProvider)
                .As<X509Certificate>()
                .InstancePerLifetimeScope();
        }

        private static Pkcs12Store KeyStoreProvider(IComponentContext c)
        {
            var settings = c.Resolve<Setting<KeyStoreConf>>();
            var storePath = settings.Get(KeyStoreConf.Path);
            var storePassword = settings.Get(KeyStoreConf.Password);

            var fi = new FileInfo(storePath);
            if (!fi.Exists)
            {
                throw new OxalisLoadingException(
                    String.Format("Unable to find keystore at '{0}'.", fi.FullName));
            }

            Pkcs12Store store;
            using (var fs = fi.OpenRead())
            {
                store = new Pkcs12Store(fs, storePassword.ToCharArray());
            }

            return store;
        }

        private static AsymmetricKeyParameter PrivateKeyProvider(IComponentContext c)
        {
            var keyStore = c.Resolve<Pkcs12Store>();
            var settings = c.Resolve<Setting<KeyStoreConf>>();

            var keyAlias = settings.Get(KeyStoreConf.KeyAlias);
            var keyPassword = settings.Get(KeyStoreConf.KeyPassword);

            if (!keyStore.ContainsAlias(keyAlias))
            {
                throw new OxalisLoadingException($"Key alias '{keyAlias}' is not found in the key store.");
            }

            AsymmetricKeyEntry privateKey = keyStore.GetKey(keyAlias);
            if (privateKey == null)
            {
                throw new OxalisLoadingException("Unable to load private key due to wrong password.");
            }

            return privateKey.Key;
        }

        private static X509Certificate CertificateProvider(IComponentContext c)
        {
            var keyStore = c.Resolve<Pkcs12Store>();
            var settings = c.Resolve<Setting<KeyStoreConf>>();
            var keyAlias = settings.Get(KeyStoreConf.KeyAlias);


            if (!keyStore.ContainsAlias(keyAlias))
            {
                throw new OxalisLoadingException($"Key alias '{keyAlias}' is not found in the key store.");
            }

            return keyStore.GetCertificate(keyAlias).Certificate;
        }
        

    }
}
