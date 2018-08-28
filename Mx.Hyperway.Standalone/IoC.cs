namespace Mx.Hyperway.Standalone
{
    using System.Collections.Generic;
    using System.Linq;

    using Autofac;

    using MimeKit.Cryptography;

    using Mx.Hyperway.Api.Statistics;
    using Mx.Hyperway.Api.Transformer;
    using Mx.Hyperway.Api.Transmission;
    using Mx.Hyperway.As2.Outbound;
    using Mx.Hyperway.As2.Util;
    using Mx.Hyperway.Commons.Persist;
    using Mx.Hyperway.Commons.Security;
    using Mx.Hyperway.Commons.Statistics;
    using Mx.Hyperway.Commons.Timestamp;
    using Mx.Hyperway.Commons.Transmission;
    using Mx.Hyperway.DocumentSniffer.Document;
    using Mx.Hyperway.Outbound;
    using Mx.Hyperway.Outbound.Lookup;
    using Mx.Hyperway.Outbound.Transmission;
    using Mx.Peppol.Common.Model;
    using Mx.Peppol.Lookup;
    using Mx.Peppol.Lookup.Api;
    using Mx.Peppol.Lookup.Locator;
    using Mx.Peppol.Lookup.Reader;
    using Mx.Peppol.Lookup.Util;
    using Mx.Peppol.Mode;
    using Mx.Peppol.Security.Api;
    using Mx.Peppol.Security.Util;

    class IoC
    {
        public static IContainer Container { get; set; }

        public static void Setup()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<HyperwayOutboundComponent>().AsSelf();
            builder.RegisterType<TransmissionRequestBuilder>().AsSelf();
            builder.RegisterType<NoSbdhParser>().As<IContentDetector>();

            // Lookup module registration
            builder.RegisterModule(new ModeModule());               // Configuration
            builder.RegisterModule(new CertificateModule());        // Keystore
            builder.RegisterModule(new LookupModule());
            builder.RegisterModule(new TransmissionModule());
            builder.RegisterModule(new As2OutboundModule());
            builder.RegisterModule(new TimestampModule());
            builder.RegisterModule(new PersisterModule());



            // Manual registration
            builder.RegisterType<SMimeMessageFactory>().AsSelf().InstancePerLifetimeScope();
            //

            builder.RegisterType<LookupClient>().AsSelf();
            builder.RegisterType<BdxlLocator>().As<MetadataLocator>();
            builder.RegisterType<MultiReader>().As<MetadataReader>();
            builder.RegisterType<DifiCertificateValidator>().As<CertificateValidator>();

            builder.RegisterType<MetadataProvider>().AsSelf();
            

            builder.RegisterType<MessageSenderFactory>().AsSelf();
            builder.RegisterType<NoopStatisticsService>().As<IStatisticsService>();
            builder.RegisterType<DefaultTransmissionVerifier>().As<ITransmissionVerifier>();
            builder.RegisterType<TransmissionRequestBuilder>().AsSelf();

            builder.RegisterType<Mx.Peppol.Lookup.Provider.DefaultProvider>().As<MetadataProvider>();

            builder.Register(
                (c) =>
                    {
                        var config = c.Resolve<Mode>();
                        return config.Defaults.Transports.Where(x => x.Enabled).OrderBy(x => x.Weight)
                            .Select(x => TransportProfile.Of(x.Profile)).ToList();
                    }).Keyed<List<TransportProfile>>("prioritized")
                .As<List<TransportProfile>>();
            // prioritized

            builder.RegisterType<Bdxr201605Reader>()
                .Keyed<Bdxr201605Reader>("reader-protocols")
                .As<MetadataReader>();


            Container = builder.Build();

            CryptographyContext.Register(() => Container.Resolve<HyperwaySecureMimeContext>());
        }
    }
}

