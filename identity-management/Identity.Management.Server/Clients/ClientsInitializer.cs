using Identity.Management.Server.Common;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Management.Server.Clients
{
    public class ClientsInitializer : IInitializer
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public ClientsInitializer(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public void Initialize()
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                foreach (var client in DefaultClientsConfig.AllClients)
                    context.Add(client.ToEntity());

                context.SaveChanges();
            }
        }
    }
}