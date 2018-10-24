using Market.Simulator.Server.Subscribers.Entities;
using Microsoft.EntityFrameworkCore;

namespace Market.Simulator.Server.Common
{
    public class SimulatorContext : DbContext
    {
        public DbSet<Subscriber> Subscribers { get; set; }

        public SimulatorContext(DbContextOptions options) 
            : base(options)
        {
            
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SubscriberConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}