using Microsoft.AspNetCore.Authorization;
using SwipeCSAT.Api.Interfaces;

namespace SwipeCSAT.Api.Authorization;

public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirment>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public PermissionAuthorizationHandler(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, 
        PermissionRequirment requirement)
    {
           
        
        var userId = context.User.Claims.FirstOrDefault(c => c.Type == CustomClaims.userId);

        if (userId is null || !Guid.TryParse(userId.Value, out var id))
        {
            return;
        }
        using var scope = _serviceScopeFactory.CreateScope();
        var permissionService = scope.ServiceProvider.GetRequiredService<IPermissionService>();

        var permissions = await permissionService.GetPermissionsAsync(id);
        
        if (permissions.Intersect(requirement.Permissions).Any())
        {
            context.Succeed(requirement);
        }
    }
}