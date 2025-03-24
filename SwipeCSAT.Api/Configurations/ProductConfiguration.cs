using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SwipeCSAT.Api.Entities;

namespace SwipeCSAT.Api.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<ProductEntity>
{
    public void Configure(EntityTypeBuilder<ProductEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Category)
            .WithMany(x => x.Products);


        builder.HasMany(x => x.Criterions)
            .WithMany(p => p.Products);

        builder.HasMany(x => x.Reviews)
            .WithOne(x => x.ProductEntity);
    }
}