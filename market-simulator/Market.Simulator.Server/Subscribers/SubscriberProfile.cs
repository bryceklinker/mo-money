using AutoMapper;
using Market.Simulator.Client.Subscribers;
using Market.Simulator.Server.Subscribers.Entities;

namespace Market.Simulator.Server.Subscribers
{
    public class SubscriberProfile : Profile
    {
        public SubscriberProfile()
        {
            CreateMap<Subscriber, SubscriberModel>()
                .ReverseMap();
        }
    }
}