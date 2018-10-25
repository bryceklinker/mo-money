using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Market.Simulator.Models.Subscribers;
using Market.Simulator.Server.Common;
using Market.Simulator.Server.Subscribers.Entities;
using Microsoft.EntityFrameworkCore;

namespace Market.Simulator.Server.Subscribers
{
    public interface ISubscribersService
    {
        Task<SubscriberModel> Add(SubscriberModel model);
        Task<SubscriberModel[]> GetAll();
        Task<SubscriberModel> GetById(long id);
        Task Update(long id, SubscriberModel model);
        Task DeleteAsync(long id);
    }
    
    public class SubscribersService : ISubscribersService
    {
        private readonly IContext _context;
        private readonly IMapper _mapper;

        public SubscribersService(IContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SubscriberModel> Add(SubscriberModel model)
        {
            var entity = _context.Add(_mapper.Map<Subscriber>(model));
            await _context.SaveAsync();
            return _mapper.Map<SubscriberModel>(entity);
        }

        public async Task<SubscriberModel[]> GetAll()
        {
            return await _context.GetAll<Subscriber>()
                .ProjectTo<SubscriberModel>(_mapper.ConfigurationProvider)
                .ToArrayAsync();
        }

        public async Task<SubscriberModel> GetById(long id)
        {
            return await _context.GetAll<Subscriber>()
                .Where(s => s.Id == id)
                .ProjectTo<SubscriberModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task Update(long id, SubscriberModel model)
        {
            var subscriber = await _context.GetAll<Subscriber>().SingleAsync(s => s.Id == id);
            subscriber.Name = model.Name;
            subscriber.Url = model.Url;
            await _context.SaveAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var subscriber = await _context.GetAll<Subscriber>().SingleAsync(s => s.Id == id);
            _context.Remove(subscriber);
            await _context.SaveAsync();
        }
    }
}