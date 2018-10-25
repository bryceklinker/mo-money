using System.Linq;
using System.Threading.Tasks;
using Market.Simulator.Server.Companies.Entities;
using Market.Simulator.Server.Quotes.Entities;
using Market.Simulator.Server.Subscribers.Entities;
using Microsoft.EntityFrameworkCore;

namespace Market.Simulator.Server.Common
{
    public interface IContext
    {
        IQueryable<T> GetAll<T>() where T : class;
        T Add<T>(T entity) where T : class;
        void AddRange<T>(params T[] entities) where T : class;
        void Remove<T>(T entity) where T : class;
        Task SaveAsync();
    }
    
    public class SimulatorContext : DbContext, IContext
    {
        public SimulatorContext(DbContextOptions options) 
            : base(options)
        {
            
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SubscriberConfiguration())
                .ApplyConfiguration(new CompanyConfiguration())
                .ApplyConfiguration(new QuoteConfiguration());
            base.OnModelCreating(modelBuilder);
        }

        public IQueryable<T> GetAll<T>()
            where T : class
        {
            return Set<T>();
        }

        T IContext.Add<T>(T entity)
        {
            return Add(entity).Entity;
        }

        public void AddRange<T>(params T[] entities)
            where T : class
        {
            foreach (var entity in entities)
                Add(entity);
        }

        void IContext.Remove<T>(T entity)
        {
            Remove(entity);
        }

        public async Task SaveAsync()
        {
            await SaveChangesAsync();
        }
    }
}