using System.Threading.Tasks;
using AutoMapper;
using Market.Simulator.Client.Companies;
using Market.Simulator.Client.Publishing;
using Market.Simulator.Server.Common;
using Market.Simulator.Server.Common.Services;
using Market.Simulator.Server.Companies.Entities;

namespace Market.Simulator.Server.Companies
{
    public interface ICompaniesService
    {
        Task<CompanyModel> Add(CompanyModel model);
        Task<CompanyModel[]> GetAll();
        Task Delete(long id);
        Task<CompanyModel> GetById(long id);
        Task Update(long id, CompanyModel model);
    }

    public class CompaniesService : EntitiesService<Company, CompanyModel>, ICompaniesService
    {
        private readonly IMarketEventPublisher _eventPublisher;

        public CompaniesService(IContext context, IMapper mapper, IMarketEventPublisher eventPublisher)
            : base(context, mapper)
        {
            _eventPublisher = eventPublisher;
        }

        protected override void UpdateEntity(Company entity, CompanyModel model)
        {
            entity.Name = model.Name;
        }

        protected override async Task PostAddAsync(CompanyModel model)
        {
            await _eventPublisher.Publish(model, MarketEventType.NewCompany);
        }

        protected override async Task PostUpdateAsync(CompanyModel model)
        {
            await _eventPublisher.Publish(model, MarketEventType.CompanyUpdate);
        }
    }
}