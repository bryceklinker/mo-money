using System.Linq;
using System.Threading.Tasks;
using Market.Simulator.Client;
using Market.Simulator.Models.Companies;
using Market.Simulator.Models.Publishing;
using Market.Simulator.Tests.Common;
using Market.Simulator.Tests.Common.Fakes.MarketSubscriber;
using Xunit;

namespace Market.Simulator.Tests
{
    [Collection(MarketServerCollection.Name)]
    public class CompanyTests
    {
        private readonly FakeMarketSubscriber _fakeMarketSubscriber;
        private readonly MarketSimulatorClient _client;

        public CompanyTests(MarketServerFixture fixture)
        {
            fixture.Reset();

            _fakeMarketSubscriber = fixture.AddFakeSubscriber();
            _client = fixture.CreateClient();
        }
        
        [Fact]
        public async Task ShouldAddCompany()
        {
            var id = await _client.AddCompanyAsync("Microsoft");

            var companies = await _client.GetCompaniesAsync();
            Assert.Single(companies);
            Assert.Equal("Microsoft", companies[0].Name);
            Assert.Equal(id, companies[0].Id);
        }

        [Fact]
        public async Task ShouldAddTwoCompanies()
        {
            await _client.AddCompanyAsync("Three");
            await _client.AddCompanyAsync("Idk");

            var companies = await _client.GetCompaniesAsync();
            Assert.Equal(2, companies.Length);
        }

        [Fact]
        public async Task ShouldUpdateExistingCompany()
        {
            var id = await _client.AddCompanyAsync("Bill");
            await _client.UpdateCompanyAsync(id, new CompanyModel
            {
                Name = "New Hotness"
            });

            var company = await _client.GetCompanyAsync(id);
            Assert.Equal("New Hotness", company.Name);
        }

        [Fact]
        public async Task ShouldPublishThatACompanyIsAvailable()
        {
            await _client.AddCompanyAsync("Jack");
            await Task.Delay(1000);

            var marketEvent = _fakeMarketSubscriber.GetMarketEvents(MarketEventType.NewCompany).Single();
            Assert.Equal("Jack", marketEvent.PayloadAs<CompanyModel>().Name);
        }

        [Fact]
        public async Task ShouldPublishThatACompanyWasUpdated()
        {
            var id = await _client.AddCompanyAsync("Jack");
            await _client.UpdateCompanyAsync(id, new CompanyModel {Name = "Google"});
            await Task.Delay(1000);

            var marketEvent = _fakeMarketSubscriber.GetMarketEvents(MarketEventType.CompanyUpdate).Single();
            Assert.Equal("Google", marketEvent.PayloadAs<CompanyModel>().Name);
        }
    }
}