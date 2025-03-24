using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SwipeCSAT.Api.Entities;

namespace SwipeCSAT.Api.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<CategoryEntity>
{
    public void Configure(EntityTypeBuilder<CategoryEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasMany(x => x.Products)
            .WithOne(x => x.Category);


        builder.HasMany(x => x.Criterions)
            .WithMany(x => x.Categories).UsingEntity(j => j.ToTable("CategoryCriterion"));
    }
}