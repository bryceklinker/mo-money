using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Market.Simulator.Models.Companies;
using Market.Simulator.Server.Common;
using Market.Simulator.Server.Common.Entities;
using Market.Simulator.Server.Companies.Entities;
using Microsoft.EntityFrameworkCore;

namespace Market.Simulator.Server.Companies
{
    public interface ICompaniesService
    {
        Task<CompanyModel> Add(CompanyModel model);
        Task<CompanyModel[]> GetAll();
        Task Delete(long id);
    }

    public class CompaniesService : ICompaniesService
    {
        private readonly IContext _context;
        private readonly IMapper _mapper;

        public CompaniesService(IContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CompanyModel> Add(CompanyModel model)
        {
            var entity = _context.Add(_mapper.Map<Company>(model));
            await _context.SaveAsync();
            return _mapper.Map<CompanyModel>(entity);
        }

        public async Task<CompanyModel[]> GetAll()
        {
            return await _context.GetAll<Company>()
                .ProjectTo<CompanyModel>(_mapper.ConfigurationProvider)
                .ToArrayAsync();
        }

        public async Task Delete(long id)
        {
            var entity = await _context.GetAll<Company>().SingleAsync(c => c.Id == id);
            _context.Remove(entity);
            await _context.SaveAsync();
        }
    }
}