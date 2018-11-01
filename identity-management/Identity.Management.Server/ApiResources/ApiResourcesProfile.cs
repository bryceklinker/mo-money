using AutoMapper;
using Identity.Management.Client.ApiResources;
using IdentityServer4.Models;

namespace Identity.Management.Server.ApiResources
{
    public class ApiResourcesProfile : Profile
    {
        public ApiResourcesProfile()
        {
            CreateMap<ApiResourceModel, ApiResource>()
                .ReverseMap();
        }
    }
}