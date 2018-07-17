using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Oxalis.Standalone
{
    using System.Linq;

    using Autofac;

    using Microsoft.Extensions.Configuration.Json;

    using Mx.Oxalis.Api.Lookup;
    using Mx.Oxalis.Api.Outbound;
    using Mx.Oxalis.Api.Statistics;
    using Mx.Oxalis.Api.Transformer;
    using Mx.Oxalis.Api.Transmission;
    using Mx.Oxalis.As2.Outbound;
    using Mx.Oxalis.As2.Util;
    using Mx.Oxalis.Commons.Interop;
    using Mx.Oxalis.Commons.Security;
    using Mx.Oxalis.Commons.Statistics;
    using Mx.Oxalis.Commons.Timestamp;
    using Mx.Oxalis.Commons.Transmission;
    using Mx.Oxalis.DocumentSniffer.Document;
    using Mx.Oxalis.Outbound;
    using Mx.Oxalis.Outbound.Lookup;
    using Mx.Oxalis.Outbound.Transmission;
    using Mx.Oxalis.Statistics.Service;
    using Mx.Peppol.Common.Model;
    using Mx.Peppol.Lookup;
    using Mx.Peppol.Lookup.Api;
    using Mx.Peppol.Lookup.Locator;
    using Mx.Peppol.Lookup.Reader;
    using Mx.Peppol.Lookup.Util;
    using Mx.Peppol.Mode;
    using Mx.Peppol.Security.Api;
    using Mx.Peppol.Security.Util;

    using Org.BouncyCastle.Math.EC;

    class IoC
    {
        public static IContainer Container { get; set; }

        public static void Setup()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<OxalisOutboundComponent>().AsSelf();
            builder.RegisterType<TransmissionRequestBuilder>().AsSelf();
            builder.RegisterType<NoSbdhParser>().As<ContentDetector>();

            // Lookup module registration
            builder.RegisterModule(new ModeModule());               // Configuration
            builder.RegisterModule(new CertificateModule());        // Keystore
            builder.RegisterModule(new LookupModule());
            builder.RegisterModule(new TransmissionModule());
            builder.RegisterModule(new As2OutboundModule());
            builder.RegisterModule(new TimestampModule());
            


            // Manual registration
            builder.RegisterType<SMimeMessageFactory>().AsSelf().InstancePerLifetimeScope();
            //

            builder.RegisterType<LookupClient>().AsSelf();
            builder.RegisterType<BdxlLocator>().As<MetadataLocator>();
            builder.RegisterType<MultiReader>().As<MetadataReader>();
            builder.RegisterType<DifiCertificateValidator>().As<CertificateValidator>();

            builder.RegisterType<MetadataProvider>().AsSelf();
            

            builder.RegisterType<MessageSenderFactory>().AsSelf();
            builder.RegisterType<NoopStatisticsService>().As<StatisticsService>();
            builder.RegisterType<DefaultTransmissionVerifier>().As<TransmissionVerifier>();
            builder.RegisterType<TransmissionRequestBuilder>().AsSelf();

            builder.RegisterType<Mx.Peppol.Lookup.Provider.DefaultProvider>().As<MetadataProvider>();

            builder.Register(
                (c) =>
                    {
                        var config = c.Resolve<Mode>();
                        return config.Defaults.Transports.Where(x => x.Enabled).OrderBy(x => x.Weight)
                            .Select(x => TransportProfile.of(x.Profile)).ToList();
                    }).Keyed<List<TransportProfile>>("prioritized")
                .As<List<TransportProfile>>();
            // prioritized

            builder.RegisterType<Bdxr201605Reader>()
                .Keyed<Bdxr201605Reader>("reader-protocols")
                .As<MetadataReader>();

            Container = builder.Build();
        }
    }
}

