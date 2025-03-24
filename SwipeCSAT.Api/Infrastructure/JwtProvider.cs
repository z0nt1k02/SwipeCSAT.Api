using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SwipeCSAT.Api.Entities;
using SwipeCSAT.Api.Interfaces;

namespace SwipeCSAT.Api.Infrastructure;

public class JwtProvider(IOptions<JwtOptions> options) : IJwtProvider
{
    private readonly JwtOptions _options = options.Value;

    
    public string GenerateToken(UserEntity userEntity)
    {
        
        Claim[] claims = [
            new("userId", userEntity.Id.ToString()),
            new("Admin","true")
        ];
        if(string.IsNullOrEmpty(options.Value.SecretKey))
        {
            throw new Exception("Secret key is missing.");
        }
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
            SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            claims: claims,
            signingCredentials: signingCredentials,
            expires: DateTime.UtcNow.AddHours(_options.ExpiresHours)
        );
        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

        return tokenValue;
    }
}