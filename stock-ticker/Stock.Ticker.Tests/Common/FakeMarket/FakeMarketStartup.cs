using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Stock.Ticker.Tests.Common.FakeMarket
{
    public class FakeMarketStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }
            
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage()
                .UseMvc();
        }
    }
}