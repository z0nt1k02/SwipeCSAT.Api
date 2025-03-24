
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SwipeCSAT.Api.Configurations;
using SwipeCSAT.Api.Entities;
using AuthorizationOptions = SwipeCSAT.Api.Repositories.AuthorizationOptions;

namespace SwipeCSAT.Api;

public class SwipeCsatDbContext(DbContextOptions<SwipeCsatDbContext> options,IOptions<AuthorizationOptions> authOptions) : DbContext(options)
{
    public DbSet<CategoryEntity> Categories { get; set; }
    public DbSet<ProductEntity> Products { get; set; }
    public DbSet<CriterionRatingEntity> CriterionRatings { get; set; }
    public DbSet<CriterionEntity> Criterions { get; set; }
    public DbSet<ReviewEntity> Reviews { get; set; }

    public DbSet<UserEntity> Users { get; set; }
    public DbSet<RoleEntity> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.ApplyConfiguration(new CriterionRatingConfiguration());
        modelBuilder.ApplyConfiguration(new CriterionConfiguration());
        modelBuilder.ApplyConfiguration(new ReviewConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new PermissionConfiguration());
        

        modelBuilder.ApplyConfiguration(new RolePermissionConfiguration(authOptions.Value));
    }
}