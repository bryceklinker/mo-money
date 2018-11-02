using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.DependencyInjection;

namespace Portal.Server
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSpaPrerenderer();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSpa(b =>
            {
                b.Options.SourcePath = "client";
                if (env.IsDevelopment())
                {
                    b.UseAngularCliServer("start");
                }
            });
            app.UseStaticFiles()
                .UseMvc(r =>
                {
                    r.MapSpaFallbackRoute("spa-fallback", defaults: new
                    {
                        controller = "Home",
                        action = "Index"
                    });
                });
        }
    }
}
