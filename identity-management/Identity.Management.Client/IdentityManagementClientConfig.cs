using System;
using Mo.Money.Common.Http;

namespace Identity.Management.Client
{
    public class IdentityManagementClientConfig
    {
        public Uri AuthorityUrl { get; }
        public string ClientId { get; }
        public string ClientSecret { get; }
        public string[] Scopes { get; }
        public string TokenEndpoint => $"{AuthorityUrl.AbsoluteUri.TrimEnd('/')}/connect/token";

        public IdentityManagementClientConfig(Uri authorityUrl, string clientId, string clientSecret, string[] scopes)
        {
            AuthorityUrl = authorityUrl;
            ClientId = clientId;
            ClientSecret = clientSecret;
            Scopes = scopes;
        }

        public AuthenticatedHttpClientConfig ToAuthConfig()
        {
            return new AuthenticatedHttpClientConfig(TokenEndpoint, ClientId, ClientSecret, Scopes);
        }
    }
}