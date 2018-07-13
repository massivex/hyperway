using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Oxalis.Standalone
{
    using Autofac;

    using Mx.Oxalis.Api.Lookup;
    using Mx.Oxalis.Api.Outbound;
    using Mx.Oxalis.Api.Statistics;
    using Mx.Oxalis.Api.Transformer;
    using Mx.Oxalis.Api.Transmission;
    using Mx.Oxalis.Commons.Transmission;
    using Mx.Oxalis.DocumentSniffer.Document;
    using Mx.Oxalis.Outbound;
    using Mx.Oxalis.Outbound.Lookup;
    using Mx.Oxalis.Outbound.Transmission;
    using Mx.Oxalis.Statistics.Service;

    class IoC
    {
        public static IContainer Container { get; set; }

        public static void Setup()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<OxalisOutboundComponent>().AsSelf();
            builder.RegisterType<TransmissionRequestBuilder>().AsSelf();
            builder.RegisterType<NoSbdhParser>().As<ContentDetector>();
            builder.RegisterType<DefaultLookupService>().As<LookupService>();
            builder.RegisterType<DefaultTransmitter>().As<Transmitter>();
            builder.RegisterType<MessageSenderFactory>().AsSelf();
            builder.RegisterType<DefaultStatisticsService>().As<StatisticsService>();
            builder.RegisterType<DefaultTransmissionVerifier>().As<TransmissionVerifier>();
            Container = builder.Build();
        }
    }
}
