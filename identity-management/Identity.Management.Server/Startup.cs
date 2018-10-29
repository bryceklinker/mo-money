using System;
using Identity.Management.Server.Common;
using Identity.Management.Server.Users.Entities;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Management.Server
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MoMoneyIdentityContext>(o => o.UseInMemoryDatabase("MoMoneyContext"));
            services.AddIdentity<MoMoneyUser, MoMoneyRole>()
                .AddEntityFrameworkStores<MoMoneyIdentityContext>()
                .AddDefaultTokenProviders();
            
            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddAspNetIdentity<MoMoneyUser>()
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = dbOptionsBuilder => dbOptionsBuilder.UseInMemoryDatabase("MoMoneyConfiguration");
                })
                .AddOperationalStore(o =>
                {
                    o.ConfigureDbContext = dbOptionsBuilder => dbOptionsBuilder.UseInMemoryDatabase("MoMoneyOperational");
                });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            IdentityInitializer.InitializerDatabase(app.ApplicationServices.GetRequiredService<IServiceScopeFactory>());
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer();
        }
    }
}