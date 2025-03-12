using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SwipeCSAT.Api.Entities;

namespace SwipeCSAT.Api.Configurations;

public class CriterionRatingConfiguration : IEntityTypeConfiguration<CriterionRatingEntity>
{
    public void Configure(EntityTypeBuilder<CriterionRatingEntity> builder)
    {
        builder.HasKey(x => x.CriterionName);

        builder.HasOne(builder => builder.Product)
            .WithMany()
            .HasForeignKey(builder => builder.ProductId);
    }
}


