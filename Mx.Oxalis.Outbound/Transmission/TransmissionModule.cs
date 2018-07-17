using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Oxalis.Outbound.Transmission
{
    using Mx.Oxalis.Api.Outbound;
    using Mx.Oxalis.Api.Transformer;
    using Mx.Oxalis.Outbound.Transformer;

    public class TransmissionModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DefaultTransmitter>().As<Transmitter>().InstancePerLifetimeScope();
            builder.RegisterType<TransmissionRequestFactory>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<DefaultTransmissionService>().As<TransmissionService>();
            builder.RegisterType<XmlContentWrapper>().Keyed<ContentWrapper>("xml").As<ContentWrapper>();

        }
    }
}
