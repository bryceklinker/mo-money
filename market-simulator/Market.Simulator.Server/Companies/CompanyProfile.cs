using AutoMapper;
using Market.Simulator.Client.Companies;
using Market.Simulator.Server.Companies.Entities;

namespace Market.Simulator.Server.Companies
{
    public class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            CreateMap<Company, CompanyModel>()
                .ReverseMap();
        }
    }
}