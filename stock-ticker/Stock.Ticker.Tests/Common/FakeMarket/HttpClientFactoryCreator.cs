using System;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Stock.Ticker.Tests.Common.FakeMarket
{
    public static class HttpClientFactoryCreator
    {
        private static readonly IServiceProvider Provider = new ServiceCollection()
            .AddHttpClient()
            .BuildServiceProvider();
        
        public static IHttpClientFactory Create()
        {
            return Provider.GetRequiredService<IHttpClientFactory>();
        }
    }
}