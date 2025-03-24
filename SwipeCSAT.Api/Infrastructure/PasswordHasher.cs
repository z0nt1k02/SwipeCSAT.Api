using SwipeCSAT.Api.Interfaces;

namespace SwipeCSAT.Api.Infrastructure;

public class PasswordHasher : IPasswordHasher
{
    public string Generate(string password)
    {
        return BCrypt.Net.BCrypt.EnhancedHashPassword(password);
    }

    public bool Verify(string pasaword, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.EnhancedVerify(pasaword, hashedPassword);
    }
}