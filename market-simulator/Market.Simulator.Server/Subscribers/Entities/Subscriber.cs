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
    
    public class SubscriberConfiguration : EntityTypeConfiguration<Subscriber>
    {
        public override void ConfigureProperties(EntityTypeBuilder<Subscriber> builder)
        {
            builder.Property(p => p.Name).IsRequired().HasMaxLength(MaxStringLength);
            builder.Property(p => p.Url).IsRequired();
        }
    }
}