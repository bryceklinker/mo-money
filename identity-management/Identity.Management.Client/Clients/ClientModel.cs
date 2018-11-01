using System;
using System.Linq;
using System.Linq.Expressions;

namespace Identity.Management.Client.Clients
{
    public class ClientModel
    {
        public string ClientName { get; set; }
        public string ClientId { get; set; }
        public ClientSecretModel[] ClientSecrets { get; set; }
        public string[] GrantTypes { get; set; }
        public string[] Scopes { get; set; }

        public static readonly Expression<Func<IdentityServer4.EntityFramework.Entities.Client, ClientModel>> FromEntityExpression = c => new ClientModel
        {
            ClientId = c.ClientId,
            ClientName = c.ClientName,
            GrantTypes = c.AllowedGrantTypes.Select(g => g.GrantType).ToArray(),
            Scopes = c.AllowedScopes.Select(s => s.Scope).ToArray()
        };
    }
}