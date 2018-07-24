namespace Mx.Hyperway.DocumentSniffer
{
    using Autofac;

    using Mx.Hyperway.Api.Transformer;
    using Mx.Hyperway.Commons.IoC;
    using Mx.Hyperway.DocumentSniffer.Document;

    public class SnifferModule : HyperwayModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<NoSbdhParser>().Named<ContentDetector>("legacy").InstancePerLifetimeScope();
        }
    }
}
