namespace Mx.Hyperway.Outbound.Transmission
{
    using Autofac;

    using Mx.Hyperway.Api.Outbound;
    using Mx.Hyperway.Api.Transformer;
    using Mx.Hyperway.Outbound.Transformer;

    public class TransmissionModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DefaultTransmitter>().As<ITransmitter>().InstancePerLifetimeScope();
            builder.RegisterType<TransmissionRequestFactory>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<DefaultTransmissionService>().As<ITransmissionService>();
            builder.RegisterType<XmlContentWrapper>().Keyed<IContentWrapper>("xml").As<IContentWrapper>();

        }
    }
}
