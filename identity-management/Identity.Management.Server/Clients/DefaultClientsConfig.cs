using System;
using System.Collections.Generic;
using Identity.Management.Server.ApiResources;
using IdentityServer4.Models;

namespace Identity.Management.Server.Clients
{
    public static class DefaultClientsConfig
    {
        public static readonly Client IdentityClient = new Client
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
                DefaultApiResourcesConfig.IdentityApiResource.Name
            }
        };

        public static readonly Client[] AllClients =
        {
            IdentityClient
        };
    }
}