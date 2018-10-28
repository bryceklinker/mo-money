using Microsoft.AspNetCore.Hosting;
using Serilog;

namespace Mo.Money.Common
{
    public static class WebHostBuilderExtensions
    {
        public static IWebHostBuilder ConfigureForMoMoney(this IWebHostBuilder builder, string applicationInsightsKey = "")
        {
            return builder.UseSerilog((context, config) =>
            {
                config.MinimumLevel.Debug()
                    .WriteTo.Console()
                    .WriteTo.ApplicationInsightsEvents(applicationInsightsKey)
                    .WriteTo.ApplicationInsightsTraces(applicationInsightsKey);
            });
        }
    }
}