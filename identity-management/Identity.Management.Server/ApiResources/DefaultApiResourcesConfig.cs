using IdentityServer4.Models;

namespace Identity.Management.Server.ApiResources
{
    public static class DefaultApiResourcesConfig
    {
        public static readonly ApiResource IdentityApiResource = new ApiResource("mo.money.identity", "Identity");

        public static readonly ApiResource[] AllApiResources =
        {
            IdentityApiResource
        };
    }
}