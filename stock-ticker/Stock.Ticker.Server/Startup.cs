﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Stock.Ticker.Server.Market;
using Stock.Ticker.Server.Quotes;

namespace Stock.Ticker.Server
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IQuotesService, QuotesService>();
            services.AddTransient<IMarketClientFactory, MarketClientFactory>();
            services.AddHostedService<MarketSubscriptionBackgroundService>();
            services.AddMvc();
            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc()
                .UseSignalR(route => route.MapHub<QuotesHub>("/quotes"));
        }
    }
}