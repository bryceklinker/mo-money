using Microsoft.Extensions.DependencyInjection;
using EndToEnd.Application;
using Serilog;

namespace EndToEnd
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