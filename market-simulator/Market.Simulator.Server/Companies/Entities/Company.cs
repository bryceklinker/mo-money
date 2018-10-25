using System.Collections.Generic;
using Market.Simulator.Server.Common.Entities;
using Market.Simulator.Server.Quotes.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Market.Simulator.Server.Companies.Entities
{
    public class Company : Entity
    {
        public string Name { get; set; }
        
        public virtual ICollection<Quote> Quotes { get; set; }
    }
    
    public class CompanyConfiguration : EntityTypeConfiguration<Company>
    {
        public override void ConfigureProperties(EntityTypeBuilder<Company> builder)
        {
            builder.Property(p => p.Name).IsRequired().HasMaxLength(MaxStringLength);
            builder.HasMany(p => p.Quotes).WithOne(p => p.Company).IsRequired();
        }
    }
}