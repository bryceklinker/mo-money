using Identity.Management.Server.Common;
using Identity.Management.Server.Users.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Management.Server.Users
{
    public class UsersInitializer : IInitializer
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public UsersInitializer(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public void Initialize()
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<MoMoneyUser>>();
                userManager.CreateAsync(DefaultUsersConfig.AdminUser, DefaultUsersConfig.AdminPassword).Wait();    
            }
        }
    }
}