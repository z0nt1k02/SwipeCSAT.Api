using Microsoft.AspNetCore.Authorization;
using SwipeCSAT.Api.Enums;

namespace SwipeCSAT.Api.Authorization;

public class PermissionRequirment(Permission[] permissions) : IAuthorizationRequirement
{
    
    public Permission[] Permissions { get; set; } = [];
}