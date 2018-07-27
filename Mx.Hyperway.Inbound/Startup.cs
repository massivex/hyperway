namespace Mx.Hyperway.Inbound
{
    using Autofac;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using Mx.Hyperway.Api.Persist;
    using Mx.Hyperway.Api.Statistics;
    using Mx.Hyperway.Api.Transmission;
    using Mx.Hyperway.As2.Inbound;
    using Mx.Hyperway.As2.Outbound;
    using Mx.Hyperway.As2.Util;
    using Mx.Hyperway.Commons.Persist;
    using Mx.Hyperway.Commons.Security;
    using Mx.Hyperway.Commons.Statistics;
    using Mx.Hyperway.Commons.Timestamp;
    using Mx.Hyperway.Commons.Transmission;
    using Mx.Peppol.Mode;
    using Mx.Peppol.Security.Api;
    using Mx.Peppol.Security.Util;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new ModeModule());               // Configuration
            builder.RegisterModule(new CertificateModule());
            builder.RegisterModule(new As2OutboundModule());
            builder.RegisterModule(new TimestampModule());
            builder.RegisterModule(new PersisterModule());
            builder.RegisterType<DefaultTransmissionVerifier>().As<TransmissionVerifier>();
            builder.RegisterType<DefaultPersisterHandler>().As<PersisterHandler>();
            builder.RegisterType<DifiCertificateValidator>().As<CertificateValidator>();
            builder.RegisterType<SMimeMessageFactory>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<As2InboundHandler>().AsSelf();
            builder.RegisterType<NoopStatisticsService>().As<StatisticsService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
