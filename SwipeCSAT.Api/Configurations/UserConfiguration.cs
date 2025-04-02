using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SwipeCSAT.Api.Entities;

namespace SwipeCSAT.Api.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.HasKey(user => user.Id);

        builder.HasMany(u => u.Roles).WithMany(r => r.Users)
            .UsingEntity<UserRoleEntity>(
                l => l.HasOne<RoleEntity>().WithMany().HasForeignKey("RoleId"),
                r => r.HasOne<UserEntity>().WithMany().HasForeignKey("UserId") 
            );
        
    }
}