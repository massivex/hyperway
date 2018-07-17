namespace Mx.Oxalis.Commons.Timestamp
{
    using Autofac;

    using Mx.Oxalis.Api.Timestamp;

    public class TimestampModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SystemTimestampProvider>()
                .Keyed<TimestampProvider>("system")
                .As<TimestampProvider>()
                .InstancePerLifetimeScope();
        }
    }
}
