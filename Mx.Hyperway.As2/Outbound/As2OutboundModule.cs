namespace Mx.Hyperway.As2.Outbound
{
    using Autofac;

    using Mx.Hyperway.Api.Outbound;
    using Mx.Hyperway.As2.Util;

    public class As2OutboundModule: Module {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<HyperwaySecureMimeContext>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<As2MessageSender>().Keyed<IMessageSender>("hyperway-as2").As<IMessageSender>();
        }
    }
}
