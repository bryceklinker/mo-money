using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Identity.Management.Client.Clients;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Identity.Management.Server.Clients
{
    public interface IClientsService
    {
        Task<ClientModel> AddAsync(ClientModel model);
        Task<ClientModel[]> GetAllAsync();
        Task<ClientModel> GetByIdAsync(string id);
        Task DeleteAsync(string id);
    }
    
    public class ClientsService : IClientsService
    {
        private readonly ConfigurationDbContext _context;
        private readonly IMapper _mapper;

        public ClientsService(ConfigurationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ClientModel> AddAsync(ClientModel model)
        {
            var identityServerClient = _mapper.Map<IdentityServer4.Models.Client>(model);
            var entry = _context.Add(identityServerClient.ToEntity());
            await _context.SaveChangesAsync();
            return _mapper.Map<ClientModel>(entry.Entity.ToModel());
        }

        public async Task<ClientModel[]> GetAllAsync()
        {
            return await _context.Set<IdentityServer4.EntityFramework.Entities.Client>()
                .Select(ClientModel.FromEntityExpression)
                .ToArrayAsync();
        }

        public async Task<ClientModel> GetByIdAsync(string id)
        {
            return await _context.Set<IdentityServer4.EntityFramework.Entities.Client>()
                .Select(ClientModel.FromEntityExpression)
                .SingleOrDefaultAsync(c => c.ClientId == id);
        }

        public async Task DeleteAsync(string id)
        {
            var entity = await _context.Set<IdentityServer4.EntityFramework.Entities.Client>()
                .SingleAsync(c => c.ClientId == id);
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}