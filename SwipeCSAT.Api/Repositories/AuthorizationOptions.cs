namespace SwipeCSAT.Api.Repositories;

public class AuthorizationOptions
{
    public RolePermissions[] RolePermissionss { get; set; } = [];
}

public class RolePermissions
{
    public string Role { get; set; } = string.Empty;
    public string[] Permissions { get; set; } = [];
}