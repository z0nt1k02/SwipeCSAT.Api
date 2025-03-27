using SwipeCSAT.Api.Enums;

namespace SwipeCSAT.Api.Interfaces;

public interface IPermissionService
{
    Task<HashSet<Permission>> GetPermissionsAsync(Guid userId);
}