using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Mo.Money.Common
{
    public static class WebHostBuilderExtensions
    {
        public static IWebHostBuilder ConfigureForMoMoney(this IWebHostBuilder builder,
            string applicationInsightsKey = "", string[] commandLineArgs = null)
        {
            commandLineArgs = commandLineArgs ?? Array.Empty<string>();
            return builder
                .ConfigureAppConfiguration((context, config) => config.AddCommandLine(commandLineArgs))
                .UseSerilog((context, config) =>
                {
                    config.MinimumLevel.Debug()
                        .WriteTo.Console()
                        .WriteTo.ApplicationInsightsEvents(applicationInsightsKey)
                        .WriteTo.ApplicationInsightsTraces(applicationInsightsKey);
                });
        }
    }
}