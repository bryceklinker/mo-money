using AutoMapper;
using Market.Simulator.Server.Common;
using Market.Simulator.Server.Common.Services;
using Market.Simulator.Server.Companies;
using Market.Simulator.Server.Quotes.Publishing;
using Market.Simulator.Server.Quotes.Services;
using Market.Simulator.Server.Subscribers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Polly;

namespace Market.Simulator.Server
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddDbContext<SimulatorContext>(o => o.UseInMemoryDatabase("Simulator"));
            services.AddTransient<IContext>(p => p.GetRequiredService<SimulatorContext>());
            services.AddTransient<ISubscribersService, SubscribersService>();
            services.AddTransient<ICompaniesService, CompaniesService>();
            services.AddAutoMapper(config => config.AddProfiles(typeof(Startup).Assembly));
            services.AddTransient<IQuoteGenerator, QuoteGenerator>();
            services.AddTransient<IMarketEventPublisher, MarketEventPublisher>();
            services.AddTransient<IQuotesPublisher, QuotesPublisher>();
            services.AddHostedService<QuotePublishingBackgroundService>();
            services.AddHttpClient()
                .AddHttpClient(MarketEventPublisher.HttpClientName)
                .AddTransientHttpErrorPolicy(p => p.RetryAsync(3));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMvc();
        }
    }
}