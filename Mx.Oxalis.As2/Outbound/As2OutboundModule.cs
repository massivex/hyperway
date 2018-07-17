using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Oxalis.As2.Outbound
{
    using Autofac;

    using Mx.Oxalis.Api.Outbound;

    public class As2OutboundModule: Module {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<As2MessageSender>().Keyed<MessageSender>("oxalis-as2").As<MessageSender>();
        }
    }
}
