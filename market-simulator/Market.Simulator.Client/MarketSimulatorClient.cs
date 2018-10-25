using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Market.Simulator.Client.Common;
using Market.Simulator.Models.Companies;
using Market.Simulator.Models.Subscribers;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Market.Simulator.Client
{
    public class MarketSimulatorClient
    {
        private readonly IRestClient _client;

        public MarketSimulatorClient(Uri baseUrl)
            : this(new RestClient(baseUrl))
        {
        }

        private MarketSimulatorClient(IRestClient client)
        {
            _client = client;
        }

        public async Task<long> AddSubscriberAsync(string name, string url)
        {
            return await AddSubscriberAsync(new SubscriberModel { Name = name, Url = url });
        }
        
        public async Task<long> AddSubscriberAsync(SubscriberModel subscriberModel)
        {
            var response = await _client.PostAsync("/subscribers", subscriberModel).ConfigureAwait(false);
            return long.TryParse(response.Headers.Location.Segments.Last(), out var id)
                ? id
                : default(long);
        }

        public async Task<SubscriberModel[]> GetSubscribersAsync()
        {
            return await _client.GetAsync<SubscriberModel[]>("/subscribers").ConfigureAwait(false);
        }

        public async Task UpdateSubscriberAsync(long id, SubscriberModel subscriberModel)
        {
            await _client.PutAsync($"/subscribers/{id}", subscriberModel).ConfigureAwait(false);
        }

        public async Task<SubscriberModel> GetSubscriberAsync(long id)
        {
            return await _client.GetAsync<SubscriberModel>($"/subscribers/{id}").ConfigureAwait(false);
        }

        public async Task DeleteSubscriberAsync(long id)
        {
            await _client.DeleteAsync($"/subscribers/{id}");
        }

        public async Task<long> AddCompanyAsync(string name)
        {
            var response = await _client.PostAsync("/companies", new CompanyModel {Name = name});
            return long.TryParse(response.Headers.Location.Segments.Last(), out var id)
                ? id
                : default(long);
        }

        public async Task<CompanyModel[]> GetCompaniesAsync()
        {
            return await _client.GetAsync<CompanyModel[]>("/companies").ConfigureAwait(false);
        }

        public async Task DeleteCompanyAsync(long id)
        {
            await _client.DeleteAsync($"/companies/{id}");
        }
    }
}