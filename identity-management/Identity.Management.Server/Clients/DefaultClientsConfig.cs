using System.Collections.Generic;
using Identity.Management.Server.ApiResources;
using IdentityServer4;
using IdentityServer4.Models;

namespace Identity.Management.Server.Clients
{
    public static class DefaultClientsConfig
    {
        public static readonly IdentityServer4.Models.Client IdentityClient = new IdentityServer4.Models.Client
        {
            ClientId = "Mo.Money.Identity",
            ClientName = "Identity",
            ClientSecrets = new List<Secret>
            {
                new Secret("Mo.Money.Identity.Secret".Sha512())
            },
            AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
            AllowedScopes = new List<string>
            {
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Profile,
                IdentityServerConstants.StandardScopes.Email,
                IdentityServerConstants.StandardScopes.Address,
                DefaultApiResourcesConfig.IdentityApiResource.Name
            }
        };

        public static readonly IdentityServer4.Models.Client[] AllClients =
        {
            IdentityClient
        };
    }
}