using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;

namespace Mo.Money.Common.Http
{
    public class AuthenticatedHttpClientCreator : IHttpClientCreator
    {
        private readonly DefaultHttpClientCreator _defaultCreator;
        private readonly TokenClient _tokenClient;
        private readonly string _scope;

        public AuthenticatedHttpClientCreator(AuthenticatedHttpClientConfig config)
        {
            _defaultCreator = new DefaultHttpClientCreator();
            _scope = config.GetScope();
            _tokenClient = new TokenClient(config.TokenEndpoint.AbsoluteUri, config.ClientId, config.ClientSecret);
        }
        
        public async Task<HttpClient> CreateClient()
        {
            var token = await GetTokenAsync();
            var client = await _defaultCreator.CreateClient();
            client.SetBearerToken(token.AccessToken);
            return client;
        }

        private async Task<TokenResponse> GetTokenAsync()
        {
            return await _tokenClient.RequestClientCredentialsAsync(_scope);
        }
    }
}