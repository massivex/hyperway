namespace Mx.Hyperway.Outbound.Lookup
{
    using Autofac;

    using Mx.Hyperway.Api.Lookup;
    using Mx.Peppol.Lookup.Api;
    using Mx.Peppol.Lookup.Util;

    public class LookupModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CachedLookupService>()
                .Keyed<ILookupService>("cached")
                .As<ILookupService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<DefaultLookupService>()
                .Keyed<ILookupService>("default")
                .As<ILookupService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<HyperwayHttpFetcher>().As<MetadataFetcher>();

            builder.RegisterType<MultiReader>().As<MetadataReader>();
        }
    }
}
