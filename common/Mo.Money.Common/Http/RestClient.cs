using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Mo.Money.Common.Http
{
    public interface IRestClient
    {
        Task<T> GetAsync<T>(string path);
        Task<HttpResponseMessage> PostAsync<T>(string path, T model);
        Task<HttpResponseMessage> PutAsync<T>(string path, T model);
        Task<HttpResponseMessage> DeleteAsync(string path);
    }

    public class RestClient : IRestClient
    {
        private readonly IHttpClientCreator _httpClientCreator;
        private readonly Uri _baseUrl;

        public RestClient(Uri baseUrl)
            : this(baseUrl, new DefaultHttpClientCreator())
        {
        }

        public RestClient(Uri baseUrl, IHttpClientCreator httpClientCreator)
        {
            _baseUrl = baseUrl;
            _httpClientCreator = httpClientCreator;
        }
        
        public async Task<T> GetAsync<T>(string path)
        {
            using (var client = await CreateClient())
            {
                var response = await client.GetAsync(GetFullUrl(path)).ConfigureAwait(false);
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(json);
            }
        }

        public async Task<HttpResponseMessage> PostAsync<T>(string path, T model)
        {
            using (var client = await CreateClient())
            {
                return await client.PostAsync(GetFullUrl(path), new JsonContent<T>(model)).ConfigureAwait(false);
            }
        }

        public async Task<HttpResponseMessage> PutAsync<T>(string path, T model)
        {
            using (var client = await CreateClient())
            {
                return await client.PutAsync(GetFullUrl(path), new JsonContent<T>(model)).ConfigureAwait(false);
            }
        }

        public async Task<HttpResponseMessage> DeleteAsync(string path)
        {
            using (var client = await CreateClient())
            {
                return await client.DeleteAsync(GetFullUrl(path)).ConfigureAwait(false);
            }
        }

        private async Task<HttpClient> CreateClient()
        {
            return await _httpClientCreator.CreateClient();
        }

        private string GetFullUrl(string path)
        {
            return _baseUrl.AbsoluteUri.EndsWith("/")
                ? $"{_baseUrl.AbsoluteUri.TrimEnd('/')}{path}"
                : $"{_baseUrl}{path}";
        }
    }
}