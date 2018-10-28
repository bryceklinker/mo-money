using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Market.Simulator.Client.Common
{
    internal interface IRestClient
    {
        Task<T> GetAsync<T>(string path);
        Task<HttpResponseMessage> PostAsync<T>(string path, T model);
        Task<HttpResponseMessage> PutAsync<T>(string path, T model);
        Task<HttpResponseMessage> DeleteAsync(string path);
    }
    
    internal class RestClient : IRestClient
    {
        private readonly Uri _baseUrl;
        private readonly IHttpClientFactory _httpClientFactory;

        public RestClient(Uri baseUrl)
        {
            _baseUrl = baseUrl;
            _httpClientFactory = new ServiceCollection()
                .AddHttpClient()
                .BuildServiceProvider()
                .GetRequiredService<IHttpClientFactory>();
        }
        
        public async Task<T> GetAsync<T>(string path)
        {
            using (var client = CreateClient())
            {
                var response = await client.GetAsync(GetFullUrl(path)).ConfigureAwait(false);
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(json);
            }
        }

        public async Task<HttpResponseMessage> PostAsync<T>(string path, T model)
        {
            using (var client = CreateClient())
            {
                return await client.PostAsync(GetFullUrl(path), new JsonContent<T>(model)).ConfigureAwait(false);
            }
        }

        public async Task<HttpResponseMessage> PutAsync<T>(string path, T model)
        {
            using (var client = CreateClient())
            {
                return await client.PutAsync(GetFullUrl(path), new JsonContent<T>(model)).ConfigureAwait(false);
            }
        }

        public async Task<HttpResponseMessage> DeleteAsync(string path)
        {
            using (var client = CreateClient())
            {
                return await client.DeleteAsync(GetFullUrl(path)).ConfigureAwait(false);
            }
        }

        private HttpClient CreateClient()
        {
            return _httpClientFactory.CreateClient();
        }

        private string GetFullUrl(string path)
        {
            return _baseUrl.AbsoluteUri.EndsWith("/")
                ? $"{_baseUrl.AbsoluteUri.TrimEnd('/')}{path}"
                : $"{_baseUrl}{path}";
        }
    }
}