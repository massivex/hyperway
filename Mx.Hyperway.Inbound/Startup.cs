namespace Mx.Hyperway.Inbound
{
    using Autofac;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

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

    using zipkin4net;
    using zipkin4net.Middleware;
    using zipkin4net.Tracers.Zipkin;
    using zipkin4net.Transport.Http;

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
            builder.RegisterType<DefaultTransmissionVerifier>().As<ITransmissionVerifier>();
            builder.RegisterType<DefaultPersisterHandler>().As<IPersisterHandler>();
            builder.RegisterType<DifiCertificateValidator>().As<CertificateValidator>();
            builder.RegisterType<SMimeMessageFactory>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<As2InboundHandler>().AsSelf();
            builder.RegisterType<NoopStatisticsService>().As<IStatisticsService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            var appName = this.Configuration["applicationName"];
            var zipkinUrl = this.Configuration["zipkinUrl"];
            if (!string.IsNullOrWhiteSpace(zipkinUrl))
            {
                var lifetime = app.ApplicationServices.GetService<IApplicationLifetime>();
                lifetime.ApplicationStarted.Register(
                    () =>
                        {
                            TraceManager.SamplingRate = 1.0f;
                            var logger = new TracingLogger(loggerFactory, "zipkin4net");
                            var httpSender = new HttpZipkinSender("http://localhost:9411", "application/json");
                            var tracer = new ZipkinTracer(httpSender, new JSONSpanSerializer());
                            TraceManager.RegisterTracer(tracer);
                            TraceManager.Start(logger);
                        });
                lifetime.ApplicationStopped.Register(() => TraceManager.Stop());
                app.UseTracing(appName);
            }
        }
    }
}
