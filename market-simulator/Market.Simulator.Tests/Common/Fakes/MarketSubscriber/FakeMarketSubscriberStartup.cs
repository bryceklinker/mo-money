using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Market.Simulator.Tests.Common.Fakes.MarketSubscriber
{
    public class FakeMarketSubscriberStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }
            
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc();
        }
    }
}