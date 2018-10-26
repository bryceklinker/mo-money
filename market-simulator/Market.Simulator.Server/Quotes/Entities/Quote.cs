using System;
using Market.Simulator.Server.Common.Entities;
using Market.Simulator.Server.Companies.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Market.Simulator.Server.Quotes.Entities
{
    public class Quote : IEntity
    {
        public long Id { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public decimal Price { get; set; }

        public virtual Company Company { get; set; }
    }
    
    public class QuoteConfiguration : EntityTypeConfiguration<Quote>
    {
        public override void ConfigureProperties(EntityTypeBuilder<Quote> builder)
        {
            builder.Property(p => p.Price).IsRequired();
            builder.Property(p => p.Timestamp).IsRequired().ValueGeneratedOnAdd();

            builder.HasOne(p => p.Company).WithMany(p => p.Quotes).IsRequired();
        }
    }
}