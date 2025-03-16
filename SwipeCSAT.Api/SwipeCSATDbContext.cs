using Microsoft.EntityFrameworkCore;
using SwipeCSAT.Api.Configurations;
using SwipeCSAT.Api.Entities;

namespace SwipeCSAT.Api;

public class SwipeCSATDbContext:DbContext
{
    public SwipeCSATDbContext(DbContextOptions<SwipeCSATDbContext> options) : base(options)
    {

    }
    public DbSet<CategoryEntity> Categories { get; set; }
    public DbSet<ProductEntity> Products { get; set; }
    public DbSet<CriterionRatingEntity> CriterionRatings { get; set; }
    public DbSet<CriterionEntity> Criterions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.ApplyConfiguration(new CriterionRatingConfiguration());
        modelBuilder.ApplyConfiguration(new CriterionConfiguration());
    }
}

