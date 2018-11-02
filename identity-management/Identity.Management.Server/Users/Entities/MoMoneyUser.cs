using Microsoft.AspNetCore.Identity;

namespace Identity.Management.Server.Users.Entities
{
    public class MoMoneyUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}