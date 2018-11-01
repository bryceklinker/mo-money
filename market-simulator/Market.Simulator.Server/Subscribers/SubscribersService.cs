using System.Threading.Tasks;
using AutoMapper;
using Market.Simulator.Client.Subscribers;
using Market.Simulator.Server.Common;
using Market.Simulator.Server.Common.Services;
using Market.Simulator.Server.Subscribers.Entities;

namespace Market.Simulator.Server.Subscribers
{
    public interface ISubscribersService
    {
        Task<SubscriberModel> Add(SubscriberModel model);
        Task<SubscriberModel[]> GetAll();
        Task<SubscriberModel> GetById(long id);
        Task Update(long id, SubscriberModel model);
        Task Delete(long id);
    }
    
    public class SubscribersService : EntitiesService<Subscriber, SubscriberModel>, ISubscribersService
    {
        public SubscribersService(IContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        protected override void UpdateEntity(Subscriber entity, SubscriberModel model)
        {
            entity.Name = model.Name;
            entity.Url = model.Url;
        }
    }
}