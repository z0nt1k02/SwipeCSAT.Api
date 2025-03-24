
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SwipeCSAT.Api.Entities;
using SwipeCSAT.Api.Enums;
using SwipeCSAT.Api.Repositories;


namespace SwipeCSAT.Api.Configurations;

public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermissionEntity>
{
    private readonly AuthorizationOptions _authorizationOptions;

    public RolePermissionConfiguration(AuthorizationOptions authorization)
    {
        _authorizationOptions = authorization;
    }
    public void Configure(EntityTypeBuilder<RolePermissionEntity> builder)
    {
        builder.HasKey(r => new { r.RoleId, r.PermissionId });
        builder.HasData(ParseRolePermissions());
    }

    private RolePermissionEntity[] ParseRolePermissions()
    {
        return _authorizationOptions.RolePermissionss.
            SelectMany(rp=>rp.Permissions
                .Select(p=>new RolePermissionEntity
                {
                    RoleId = (int)Enum.Parse<Role>(rp.Role),
                    PermissionId = (int)Enum.Parse<Permission>(p)
                })).ToArray();

    }
}