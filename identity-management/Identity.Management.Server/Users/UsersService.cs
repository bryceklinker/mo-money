using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Identity.Management.Client.Users;
using Identity.Management.Server.Users.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity.Management.Server.Users
{
    public interface IUsersService
    {
        Task<UserModel> AddAsync(UserModel model);
        Task<UserModel[]> GetAllAsync();
        Task<UserModel> GetByIdAsync(string id);
        Task DeleteAsync(string id);
    }

    public class UsersService : IUsersService
    {
        private readonly UserManager<MoMoneyUser> _userManager;
        private readonly IMapper _mapper;
        
        public UsersService(UserManager<MoMoneyUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<UserModel> AddAsync(UserModel model)
        {
            var user = _mapper.Map<MoMoneyUser>(model);
            await _userManager.CreateAsync(user, model.Password);
            return _mapper.Map<UserModel>(user);
        }

        public async Task<UserModel[]> GetAllAsync()
        {
            return await _userManager.Users
                .ProjectTo<UserModel>(_mapper.ConfigurationProvider)
                .ToArrayAsync();
        }

        public async Task<UserModel> GetByIdAsync(string id)
        {
            return await _userManager.Users
                .ProjectTo<UserModel>(_mapper.ConfigurationProvider)
                .SingleAsync(u => u.Id == id);
        }

        public async Task DeleteAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            await _userManager.DeleteAsync(user);
        }
    }
}