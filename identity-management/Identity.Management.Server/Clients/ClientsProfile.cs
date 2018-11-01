using AutoMapper;
using Identity.Management.Client.Clients;
using IdentityServer4.Models;
using Secret = IdentityServer4.Models.Secret;

namespace Identity.Management.Server.Clients
{
    public class ClientsProfile : Profile
    {
        public ClientsProfile()
        {
            CreateMap<ClientModel, IdentityServer4.Models.Client>()
                .ForMember(c => c.AllowedGrantTypes, config => config.MapFrom(c => c.GrantTypes))
                .ForMember(c => c.AllowedScopes, config => config.MapFrom(c => c.Scopes))
                .ReverseMap();

            CreateMap<ClientSecretModel, Secret>()
                .ForMember(s => s.Value, config => config.MapFrom(s => s.Value.Sha512()))
                .ReverseMap()
                .ForMember(s => s.Value, config => config.Ignore());
        }
    }
}