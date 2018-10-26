using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Market.Simulator.Models.Companies;
using Market.Simulator.Server.Common;
using Market.Simulator.Server.Common.Entities;
using Market.Simulator.Server.Common.Services;
using Market.Simulator.Server.Companies.Entities;
using Microsoft.EntityFrameworkCore;

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
        public CompaniesService(IContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        protected override void UpdateEntity(Company entity, CompanyModel model)
        {
            entity.Name = model.Name;
        }
    }
}