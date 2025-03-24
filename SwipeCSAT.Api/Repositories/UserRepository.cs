using Microsoft.EntityFrameworkCore;
using SwipeCSAT.Api.Entities;

namespace SwipeCSAT.Api.Repositories;

public class UserRepository
{
    private readonly SwipeCsatDbContext _context;

    public UserRepository(SwipeCsatDbContext context)
    {
        _context = context;
    }

    public async Task Add(UserEntity user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task<UserEntity> GetByEmail(string email)
    {
        var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email)
                   ?? throw new Exception("Пользователь не найден");
        return user;
    }
}