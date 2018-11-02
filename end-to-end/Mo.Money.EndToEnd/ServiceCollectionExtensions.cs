using Microsoft.Extensions.DependencyInjection;
using Mo.Money.EndToEnd.Application;
using Serilog;

namespace Mo.Money.EndToEnd
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEndToEndServices(this IServiceCollection services)
        {
            services.AddLogging(configure => configure.AddSerilog());
            services.AddSingleton<ApplicationManager>();
            return services;
        }
    }
}