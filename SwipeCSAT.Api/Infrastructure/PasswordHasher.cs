using SwipeCSAT.Api.Interfaces;

namespace SwipeCSAT.Api.Infrastructure
{
    public class PasswordHasher : IPasswordHasher
    {
        public string Generate(string password)=>
            BCrypt.Net.BCrypt.EnhancedHashPassword(password);

        public bool Verify(string pasaword, string hashedPassword) =>
            BCrypt.Net.BCrypt.EnhancedVerify(pasaword, hashedPassword);
        
    }
}
