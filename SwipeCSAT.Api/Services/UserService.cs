using SwipeCSAT.Api.Entities;
using SwipeCSAT.Api.Infrastructure;
using SwipeCSAT.Api.Interfaces;
using SwipeCSAT.Api.Repositories;

namespace SwipeCSAT.Api.Services
{
    public class UserService
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly UserRepository _userRepository;
        private readonly IJwtProvider _jwtProvider;
        public UserService(IPasswordHasher passwordHasher,UserRepository userRepository,IJwtProvider jwtProvider)
        {
            _passwordHasher = passwordHasher;
            _userRepository = userRepository;
            _jwtProvider = jwtProvider;
        }
        public async Task Register(string userName,string email,string password)
        {
            var hashedPassword = _passwordHasher.Generate(password);

            var user = UserEntity.Create(Guid.NewGuid(), userName, hashedPassword, email);

            await _userRepository.Add(user);
        }

        public async Task<string> Login(string email,string password)
        {
            var user = await _userRepository.GetByEmail(email);
            var result = _passwordHasher.Verify(password, user.PasswordHash);

            if (!result)
                throw new Exception("Неправильный пароль");

            var token = _jwtProvider.GenerateToken(user);
            return token; 
        }
    }
}
