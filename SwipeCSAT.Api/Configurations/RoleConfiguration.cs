﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SwipeCSAT.Api.Entities;
using SwipeCSAT.Api.Enums;

namespace SwipeCSAT.Api.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<RoleEntity>
{
    public void Configure(EntityTypeBuilder<RoleEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasMany(r => r.Permissions).WithMany(p => p.Roles)
            .UsingEntity<RolePermissionEntity>(
                l=>l.HasOne<PermissionEntity>().WithMany().HasForeignKey(e=>e.PermissionId),
                r=>r.HasOne<RoleEntity>().WithMany().HasForeignKey(e=>e.RoleId));

        var roles = Enum.GetValues<Role>()
            .Select(r => new RoleEntity
            {
                Id = (int)r,
                Name = r.ToString(),
            });
        builder.HasData(roles);

    }
}