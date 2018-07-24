namespace Mx.Hyperway.Inbound
{
    using Autofac;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using Mx.Hyperway.As2.Outbound;
    using Mx.Hyperway.As2.Util;
    using Mx.Hyperway.Commons.Security;
    using Mx.Peppol.Mode;

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
            builder.RegisterType<SMimeMessageFactory>().AsSelf().InstancePerLifetimeScope();
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
