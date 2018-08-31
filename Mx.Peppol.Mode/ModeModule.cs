namespace Mx.Peppol.Mode
{
    using Autofac;

    using Mx.Peppol.Mode.Configuration;

    public class ModeModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Mode>().AsSelf();
            builder.RegisterGeneric(typeof(Setting<>)).AsSelf();
        }
    }
}
