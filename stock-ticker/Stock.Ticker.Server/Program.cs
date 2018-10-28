using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;

namespace Stock.Ticker.Server
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
                        .WriteTo.Console()
                        .WriteTo.ApplicationInsightsEvents("")
                        .WriteTo.ApplicationInsightsTraces("");
                })
                .UseStartup<Startup>();
    }
}