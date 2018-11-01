using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Identity.Management.Client.ApiResources;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using Microsoft.EntityFrameworkCore;

namespace Identity.Management.Server.ApiResources
{
    public interface IApiResourcesService
    {
        Task<ApiResourceModel> AddAsync(ApiResourceModel model);
        Task<ApiResourceModel[]> GetAllAsync();
        Task DeleteAsync(string name);
        Task<ApiResourceModel> GetByIdAsync(string id);
    }

    public class ApiResourcesService : IApiResourcesService
    {
        private readonly ConfigurationDbContext _context;
        private readonly IMapper _mapper;

        public ApiResourcesService(ConfigurationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ApiResourceModel> AddAsync(ApiResourceModel model)
        {
            var apiResource = _mapper.Map<ApiResource>(model);
            var entry = _context.Add(apiResource.ToEntity());
            await _context.SaveChangesAsync();
            return _mapper.Map<ApiResourceModel>(entry.Entity.ToModel());
        }

        public async Task<ApiResourceModel[]> GetAllAsync()
        {
            return await _context.Set<IdentityServer4.EntityFramework.Entities.ApiResource>()
                .Select(ApiResourceModelMappers.FromEntityExpression)
                .ToArrayAsync();
        }

        public async Task<ApiResourceModel> GetByIdAsync(string id)
        {
            return await _context.Set<IdentityServer4.EntityFramework.Entities.ApiResource>()
                .Select(ApiResourceModelMappers.FromEntityExpression)
                .SingleAsync(a => a.Name == id);
        }

        public async Task DeleteAsync(string name)
        {
            var entity = await _context.Set<IdentityServer4.EntityFramework.Entities.ApiResource>()
                .SingleAsync(a => a.Name == name);
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}