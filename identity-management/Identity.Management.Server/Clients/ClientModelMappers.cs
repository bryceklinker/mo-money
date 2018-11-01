using System;
using System.Linq;
using System.Linq.Expressions;
using Identity.Management.Client.Clients;

namespace Identity.Management.Server.Clients
{
    public static class ClientModelMappers
    {
        public static readonly Expression<Func<IdentityServer4.EntityFramework.Entities.Client, ClientModel>> FromEntityExpression = c => new ClientModel
        {
            ClientId = c.ClientId,
            ClientName = c.ClientName,
            GrantTypes = c.AllowedGrantTypes.Select(g => g.GrantType).ToArray(),
            Scopes = c.AllowedScopes.Select(s => s.Scope).ToArray()
        };
    }
}