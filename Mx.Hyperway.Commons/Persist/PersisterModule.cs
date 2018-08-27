namespace Mx.Hyperway.Commons.Persist
{
    using Autofac;

    using Mx.Hyperway.Api.Persist;

    public class PersisterModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // default
            builder.RegisterType<DefaultPersister>()
                .Keyed<IPayloadPersister>("default")
                .As<IPayloadPersister>()
                .InstancePerLifetimeScope();

            builder.RegisterType<DefaultPersister>()
                .Keyed<IReceiptPersister>("default")
                .As<IReceiptPersister>()
                .InstancePerLifetimeScope(); 

            builder.RegisterType<DefaultPersisterHandler>()
                .Keyed<IPersisterHandler>("default")
                .As<IPersisterHandler>()
                .InstancePerLifetimeScope();

            builder.RegisterType<NoopPersister>()
                .Keyed<IPayloadPersister>("noop")
                .As<IPayloadPersister>()
                .InstancePerLifetimeScope();

            builder.RegisterType<NoopPersister>()
                .Keyed<IReceiptPersister>("noop")
                .As<IReceiptPersister>()
                .InstancePerLifetimeScope();

            builder.RegisterType<NoopPersister>()
                .Keyed<IPersisterHandler>("noop")
                .As<IPersisterHandler>()
                .InstancePerLifetimeScope();
        }
    }
}
