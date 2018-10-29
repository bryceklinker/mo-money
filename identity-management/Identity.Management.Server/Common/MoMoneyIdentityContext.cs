using Identity.Management.Server.Users.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity.Management.Server.Common
{
    public class MoMoneyIdentityContext : IdentityDbContext<MoMoneyUser, MoMoneyRole, string>
    {
        public MoMoneyIdentityContext(DbContextOptions options)
            : base(options)
        {
            
        }
    }
}