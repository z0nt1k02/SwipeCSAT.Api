using Microsoft.AspNetCore.Authorization;
using SwipeCSAT.Api.Enums;

namespace SwipeCSAT.Api.Authorization;

public class PermissionRequirment : IAuthorizationRequirement
{
    public PermissionRequirment(Permission[] permissions)
    {
        Permissions = permissions;
    }
    public Permission[] Permissions { get; set; } = [];
}