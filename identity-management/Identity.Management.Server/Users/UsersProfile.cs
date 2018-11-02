using AutoMapper;
using Identity.Management.Client.Users;
using Identity.Management.Server.Users.Entities;

namespace Identity.Management.Server.Users
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            CreateMap<UserModel, MoMoneyUser>()
                .ReverseMap();
        }
    }
}