using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Management.Server.Common
{
    public static class IdentityInitializer
    {
        private static readonly Client[] Clients = 
        {
            new Client()
        };

        private static readonly IdentityResource[] IdentityResources =
        {
            new IdentityResource()
        };

        private static readonly ApiResource[] ApiResources =
        {
            new ApiResource()
        };
        
        public static void InitializerDatabase(IServiceScopeFactory scopeFactory)
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var configContext = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                AddClients(configContext);
                AddIdentityResources(configContext);
                AddApiResources(configContext);
            }
        }
        
        private static void AddClients(ConfigurationDbContext context)
        {
            foreach (var client in Clients)
                context.Add(client.ToEntity());
            
            context.SaveChanges();
        }

        private static void AddIdentityResources(ConfigurationDbContext context)
        {
            foreach (var identityResource in IdentityResources)
                context.Add(identityResource.ToEntity());

            context.SaveChanges();
        }

        private static void AddApiResources(ConfigurationDbContext context)
        {
            foreach (var apiResource in ApiResources)
                context.Add(apiResource.ToEntity());

            context.SaveChanges();
        }
    }
}