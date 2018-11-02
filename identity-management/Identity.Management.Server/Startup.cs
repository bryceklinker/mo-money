using System;
using System.ComponentModel;
using System.Linq;
using AutoMapper;
using Identity.Management.Server.ApiResources;
using Identity.Management.Server.Clients;
using Identity.Management.Server.Common;
using Identity.Management.Server.Users;
using Identity.Management.Server.Users.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Identity.Management.Server
{
    public class Startup
    {
        private readonly string _authorityUrl;

        public Startup(IConfiguration configuration)
        {
            _authorityUrl = configuration.GetValue<string>("urls").Split(';')
                .First(u => new Uri(u).Scheme == "https");
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            
            services.AddAutoMapper(config => config.AddProfiles(typeof(Startup).Assembly));
            services.AddTransient<IClientsService, ClientsService>();
            services.AddTransient<IApiResourcesService, ApiResourcesService>();
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<IInitializer, ClientsInitializer>();
            services.AddTransient<IInitializer, ApiResourcesInitializer>();
            services.AddTransient<IInitializer, UsersInitializer>();
            services.AddDbContext<MoMoneyIdentityContext>(o => o.UseInMemoryDatabase("MoMoneyContext"));
            services.AddIdentity<MoMoneyUser, MoMoneyRole>()
                .AddEntityFrameworkStores<MoMoneyIdentityContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = _authorityUrl;
                options.Audience = DefaultApiResourcesConfig.IdentityApiResource.Name;
                options.RequireHttpsMetadata = false;
            });
            
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

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            InitializeIdentities(app.ApplicationServices);
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer()
                .UseAuthentication()
                .UseMvc();
        }

        private void InitializeIdentities(IServiceProvider serviceProvider)
        {
            var initializers = serviceProvider.GetServices<IInitializer>();
            foreach (var initializer in initializers)
            {
                initializer.Initialize();
            }
        }
    }
}