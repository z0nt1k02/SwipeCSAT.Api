using SwipeCSAT.Api.Enums;
using SwipeCSAT.Api.Interfaces;
using SwipeCSAT.Api.Repositories;

namespace SwipeCSAT.Api.Services;

public class PermissionService : IPermissionService
{
    private readonly UserRepository _userRepository;

    public PermissionService(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<HashSet<Permission>> GetPermissionsAsync(Guid userId)
    {
        return await _userRepository.GetUserPermissions(userId);
    }
}