using SwipeCSAT.Api.Entities;

namespace SwipeCSAT.Api.Interfaces;

public interface IJwtProvider
{
    string GenerateToken(UserEntity userEntity);
}