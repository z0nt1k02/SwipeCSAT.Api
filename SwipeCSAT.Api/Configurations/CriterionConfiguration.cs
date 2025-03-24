using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SwipeCSAT.Api.Entities;

namespace SwipeCSAT.Api.Configurations;

public class CriterionConfiguration : IEntityTypeConfiguration<CriterionEntity>
{
    public void Configure(EntityTypeBuilder<CriterionEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasMany(x => x.Products)
            .WithMany(x => x.Criterions);


        builder.HasMany(x => x.Categories)
            .WithMany(x => x.Criterions);
    }
}