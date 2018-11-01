using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Market.Simulator.Models.Companies;
using Market.Simulator.Models.Subscribers;
using Microsoft.Extensions.DependencyInjection;
using Mo.Money.Common;
using Mo.Money.Common.Http;
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
            return long.TryParse(response.GetIdFromLocationHeader(), out var id)
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
            return await AddCompanyAsync(new CompanyModel {Name = name});
        }

        public async Task<long> AddCompanyAsync(CompanyModel model)
        {
            var response = await _client.PostAsync("/companies", model);
            return long.TryParse(response.GetIdFromLocationHeader(), out var id)
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

        public async Task UpdateCompanyAsync(long id, CompanyModel model)
        {
            await _client.PutAsync($"/companies/{id}", model);
        }

        public async Task<CompanyModel> GetCompanyAsync(long id)
        {
            return await _client.GetAsync<CompanyModel>($"/companies/{id}");
        }
    }
}