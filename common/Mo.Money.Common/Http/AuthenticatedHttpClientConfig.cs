using System;

namespace Mo.Money.Common.Http
{
    public class AuthenticatedHttpClientConfig
    {
        public Uri TokenEndpoint { get; }
        public string ClientId { get; }
        public string ClientSecret { get; }
        public string[] Scopes { get; }

        public AuthenticatedHttpClientConfig(
            string tokenEndpoint, 
            string clientId, 
            string clientSecret, 
            string[] scopes)
            : this(new Uri(tokenEndpoint), clientId, clientSecret, scopes)
        {
        }
        
        public AuthenticatedHttpClientConfig(
            Uri tokenEndpoint, 
            string clientId, 
            string clientSecret, 
            string[] scopes)
        {
            TokenEndpoint = tokenEndpoint;
            ClientId = clientId;
            ClientSecret = clientSecret;
            Scopes = scopes;
        }

        public string GetScope() => string.Join(" ", Scopes);
    }
}