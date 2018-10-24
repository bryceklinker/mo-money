using Market.Simulator.Server.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Market.Simulator.Server.Subscribers.Entities
{
    public class Subscriber : Entity
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }
    
    public class SubscriberConfiguration : IEntityTypeConfiguration<Subscriber>
    {
        public void Configure(EntityTypeBuilder<Subscriber> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(250);
            builder.Property(p => p.Url).IsRequired();
        }
    }
}