using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Identity.Management.Client.ApiResources;
using Identity.Management.Client.Clients;
using Identity.Management.Client.Users;
using IdentityModel.Client;
using Microsoft.Extensions.DependencyInjection;
using Mo.Money.Common;
using Mo.Money.Common.Http;
using Newtonsoft.Json;

namespace Identity.Management.Client
{
    public class IdentityManagementClient
    {
        private static readonly string[] Scopes = {"mo.money.identity"};
        private readonly IdentityManagementClientConfig _config;
        private readonly TokenClient _tokenClient;
        private readonly IRestClient _restClient;

        public Uri AuthorityBaseUrl => _config.AuthorityUrl;

        public IdentityManagementClient(Uri authorityUrl, string clientId, string clientSecret)
            : this(new IdentityManagementClientConfig(authorityUrl, clientId, clientSecret, Scopes))
        {
            
        }
        
        private IdentityManagementClient(IdentityManagementClientConfig config)
        {
            _config = config;
            _tokenClient = new TokenClient(config.TokenEndpoint, config.ClientId, config.ClientSecret);
            _restClient = new RestClient(AuthorityBaseUrl, new AuthenticatedHttpClientCreator(config.ToAuthConfig()));
        }

        public async Task<TokenResponse> GetUserAccessTokenAsync(string username, string password)
        {
            return await _tokenClient.RequestResourceOwnerPasswordAsync(username, password, string.Join(" ", Scopes));
        }

        public async Task<string> AddClientAsync(ClientModel model)
        {
            var response = await _restClient.PostAsync("/clients", model);
            return response.GetIdFromLocationHeader();
        }

        public async Task<ClientModel[]> GetClientsAsync()
        {
            return await _restClient.GetAsync<ClientModel[]>("/clients");
        }

        public async Task<ClientModel> GetClientAsync(string id)
        {
            return await _restClient.GetAsync<ClientModel>($"/clients/{id}");
        }

        public async Task DeleteClientAsync(string id)
        {
            await _restClient.DeleteAsync($"/clients/{id}");
        }

        public async Task<string> AddApiResourceAsync(ApiResourceModel model)
        {
            var response = await _restClient.PostAsync("/api-resources", model);
            return response.GetIdFromLocationHeader();
        }

        public async Task<ApiResourceModel[]> GetApiResourcesAsync()
        {
            return await _restClient.GetAsync<ApiResourceModel[]>("/api-resources");
        }

        public async Task<ApiResourceModel> GetApiResourceAsync(string id)
        {
            return await _restClient.GetAsync<ApiResourceModel>($"/api-resources/{id}");
        }

        public async Task DeleteApiResourceAsync(string id)
        {
            await _restClient.DeleteAsync($"/api-resources/{id}");
        }

        public async Task<string> AddUserAsync(UserModel model)
        {
            var response = await _restClient.PostAsync("/users", model);
            return response.GetIdFromLocationHeader();
        }

        public async Task<UserModel[]> GetUsersAsync()
        {
            return await _restClient.GetAsync<UserModel[]>("/users");
        }

        public async Task DeleteUserAsync(string id)
        {
            await _restClient.DeleteAsync($"/users/{id}");
        }

        public async Task<UserModel> GetUserByIdAsync(string id)
        {
            return await _restClient.GetAsync<UserModel>($"/users/{id}");
        }
    }
}