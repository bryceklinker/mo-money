using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Mo.Money.Common.Http
{
    public class DefaultHttpClientCreator : IHttpClientCreator
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DefaultHttpClientCreator()
        {
            _httpClientFactory = new ServiceCollection()
                .AddHttpClient()
                .BuildServiceProvider()
                .GetRequiredService<IHttpClientFactory>();
        }
        
        public Task<HttpClient> CreateClient()
        {
            return Task.FromResult(_httpClientFactory.CreateClient());
        }
    }
}