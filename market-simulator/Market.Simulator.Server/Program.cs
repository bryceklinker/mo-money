using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;

namespace Market.Simulator.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseSerilog((context, config) =>
                {
                    config.MinimumLevel.Debug()
                        .Enrich.FromLogContext()
                        .WriteTo.Console()
                        .WriteTo.ApplicationInsightsEvents(""); 
                })
                .UseStartup<Startup>();
    }
}