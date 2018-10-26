using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Market.Simulator.Server.Common.Entities
{
    public interface IEntity
    {
        long Id { get; set; }
    }
    
    public abstract class EntityTypeConfiguration<T> : IEntityTypeConfiguration<T>
        where T : class, IEntity
    {
        public const int MaxStringLength = 256;
        
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(e => e.Id);
            ConfigureProperties(builder);
        }

        public abstract void ConfigureProperties(EntityTypeBuilder<T> builder);
    }
}