using Identity.Management.Server.Users.Entities;

namespace Identity.Management.Server.Users
{
    public static class DefaultUsersConfig
    {
        public const string AdminPassword = "CSrxQnlyJkucnhML4XcK2w==";
        public static readonly MoMoneyUser AdminUser = new MoMoneyUser
        {
            Email = "admin@momoney.com",
            EmailConfirmed = true,
            TwoFactorEnabled = false,
            UserName = "Admin",
            FirstName = "Big",
            LastName = "Red"
        };
    }
}