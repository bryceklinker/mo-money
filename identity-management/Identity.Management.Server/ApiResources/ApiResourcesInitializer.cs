using Identity.Management.Server.Common;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Management.Server.ApiResources
{
    public class ApiResourcesInitializer : IInitializer
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public ApiResourcesInitializer(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public void Initialize()
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                foreach (var resource in DefaultApiResourcesConfig.AllApiResources)
                    context.Add(resource.ToEntity());
                context.SaveChanges();
            }
        }
    }
}