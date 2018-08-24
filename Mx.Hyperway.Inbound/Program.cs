namespace Mx.Hyperway.Inbound
{
    using Autofac.Extensions.DependencyInjection;

    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;

    using zipkin4net;
    using zipkin4net.Tracers.Zipkin;
    using zipkin4net.Transport.Http;

    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
            var sender = new HttpZipkinSender("http://localhost:9411", "application/json");
            var tracer = new ZipkinTracer(sender, new JSONSpanSerializer());
            TraceManager.RegisterTracer(tracer);
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureServices(services => services.AddAutofac())
                .UseStartup<Startup>()
                .Build();
    }
}
