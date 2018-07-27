using System;
using System.Collections.Generic;
using System.Text;

namespace Mx.Hyperway.Commons.Persist
{
    using Autofac;

    using Mx.Hyperway.Api.Persist;

    public class PersisterModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // default
            builder.RegisterType<DefaultPersister>()
                .Keyed<PayloadPersister>("default")
                .As<PayloadPersister>()
                .InstancePerLifetimeScope();

            builder.RegisterType<DefaultPersister>()
                .Keyed<ReceiptPersister>("default")
                .As<ReceiptPersister>()
                .InstancePerLifetimeScope(); 

            builder.RegisterType<DefaultPersisterHandler>()
                .Keyed<PersisterHandler>("default")
                .As<PersisterHandler>()
                .InstancePerLifetimeScope();

            builder.RegisterType<NoopPersister>()
                .Keyed<PayloadPersister>("noop")
                .As<PayloadPersister>()
                .InstancePerLifetimeScope();

            builder.RegisterType<NoopPersister>()
                .Keyed<ReceiptPersister>("noop")
                .As<ReceiptPersister>()
                .InstancePerLifetimeScope();

            builder.RegisterType<NoopPersister>()
                .Keyed<PersisterHandler>("noop")
                .As<PersisterHandler>()
                .InstancePerLifetimeScope();
        }
    }
}
