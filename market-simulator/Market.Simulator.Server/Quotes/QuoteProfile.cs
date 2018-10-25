using AutoMapper;
using Market.Simulator.Models.Quotes;
using Market.Simulator.Server.Quotes.Entities;

namespace Market.Simulator.Server.Quotes
{
    public class QuoteProfile : Profile
    {
        public QuoteProfile()
        {
            CreateMap<Quote, QuoteModel>()
                .ForMember(q => q.CompanyId, config => config.MapFrom(q => q.Company.Id))
                .ForMember(q => q.CompanyName, config => config.MapFrom(q => q.Company.Name))
                .ReverseMap();
        }
    }
}