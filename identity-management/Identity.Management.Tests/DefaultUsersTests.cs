using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Identity.Management.Tests.Common;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Identity.Management.Tests
{
    [Collection(IdentityManagementCollection.Name)]
    public class DefaultUsersTests
    {
        private readonly Uri _identityUrl;
        private readonly IHttpClientFactory _httpClientFactory;

        public DefaultUsersTests(IdentityManagementFixture fixture)
        {
            _httpClientFactory = new ServiceCollection()
                .AddHttpClient()
                .BuildServiceProvider()
                .GetRequiredService<IHttpClientFactory>();

            _identityUrl = fixture.IdentityBaseUrl;
        }
        
        [Fact]
        public async Task ShouldAllowLoginUsingDefaultAdminUser()
        {
            using (var client = _httpClientFactory.CreateClient())
            {
                var response = await client.PostAsync($"{_identityUrl}connect/token", new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("username", "Admin"),
                    new KeyValuePair<string, string>("password", "password!"),
                    new KeyValuePair<string, string>("grant_type", "password"),
                }));

                var content = await response.Content.ReadAsAsync<JObject>();
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                Assert.Equal("", content.Value<string>("access_token"));
            }
        }
    }
}