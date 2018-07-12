using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Oxalis.Standalone
{
    using Autofac;

    using Mx.Oxalis.Api.Lookup;
    using Mx.Oxalis.Api.Transformer;
    using Mx.Oxalis.DocumentSniffer.Document;
    using Mx.Oxalis.Outbound;
    using Mx.Oxalis.Outbound.Transmission;

    class IoC
    {
        public static IContainer Container { get; set; }

        public static void Setup()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<OxalisOutboundComponent>().AsSelf();
            builder.RegisterType<TransmissionRequestBuilder>().AsSelf();
            builder.RegisterType<NoSbdhParser>().As<ContentDetector>();
            builder.RegisterType<LookupService>().AsSelf();
            Container = builder.Build();
        }
    }
}
