using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Market.Simulator.Models.Common;
using Market.Simulator.Server.Common.Entities;
using Microsoft.EntityFrameworkCore;

namespace Market.Simulator.Server.Common.Services
{
    public abstract class EntitiesService<TEntity, TModel>
        where TEntity : class, IEntity
        where TModel : class, IModel
    {
        private readonly IMapper _mapper;
        private readonly IContext _context;

        public EntitiesService(IContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        
        public async Task<TModel> Add(TModel model)
        {
            var entity = _context.Add(_mapper.Map<TEntity>(model));
            await _context.SaveAsync();
            var result = _mapper.Map<TModel>(entity);
            await PostAddAsync(result);
            return result;
        }

        public async Task<TModel[]> GetAll()
        {
            return await _context.GetAll<TEntity>()
                .ProjectTo<TModel>(_mapper.ConfigurationProvider)
                .ToArrayAsync();
        }

        public async Task Delete(long id)
        {
            var entity = await _context.GetAll<TEntity>().SingleAsync(c => c.Id == id);
            _context.Remove(entity);
            await _context.SaveAsync();
        }

        public async Task<TModel> GetById(long id)
        {
            return await _context.GetAll<TEntity>()
                .ProjectTo<TModel>(_mapper.ConfigurationProvider)
                .SingleAsync(c => c.Id == id);
        }

        public async Task Update(long id, TModel model)
        {
            var entity = await _context.GetById<TEntity>(id);
            UpdateEntity(entity, model);
            await _context.SaveAsync();
            await PostUpdateAsync(model);

        }

        protected abstract void UpdateEntity(TEntity entity, TModel model);

        protected virtual Task PostAddAsync(TModel model)
        {
            return Task.CompletedTask;
        }

        protected virtual Task PostUpdateAsync(TModel model)
        {
            return Task.CompletedTask;
        }
    }
}