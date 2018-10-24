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
        private readonly SimulatorContext _context;
        private readonly IMapper _mapper;

        public SubscribersService(SimulatorContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SubscriberModel> Add(SubscriberModel model)
        {
            var entry = _context.Add(_mapper.Map<Subscriber>(model));
            await _context.SaveChangesAsync();
            return _mapper.Map<SubscriberModel>(entry.Entity);
        }

        public async Task<SubscriberModel[]> GetAll()
        {
            return await _context.Set<Subscriber>()
                .ProjectTo<SubscriberModel>(_mapper.ConfigurationProvider)
                .ToArrayAsync();
        }

        public async Task<SubscriberModel> GetById(long id)
        {
            return await _context.Set<Subscriber>()
                .Where(s => s.Id == id)
                .ProjectTo<SubscriberModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task Update(long id, SubscriberModel model)
        {
            var subscriber = await _context.Set<Subscriber>().SingleAsync(s => s.Id == id);
            subscriber.Name = model.Name;
            subscriber.Url = model.Url;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var subscriber = await _context.Set<Subscriber>().SingleAsync(s => s.Id == id);
            _context.Remove(subscriber);
            await _context.SaveChangesAsync();
        }
    }
}