using Microsoft.EntityFrameworkCore;
using SwipeCSAT.Api.Entities;
using SwipeCSAT.Api.Enums;

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
        var roleEntity = await _context.Roles.SingleOrDefaultAsync(r=>r.Id==(int)Role.User)
            ?? throw new InvalidOperationException("User not found");
        user.Roles = [roleEntity];
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    
    public async Task<UserEntity> GetByEmail(string email)
    {
        var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email)
                   ?? throw new Exception("Пользователь не найден");
        return user;
    }

    public async Task<HashSet<Permission>> GetUserPermissions(Guid userId)
    {
        var roles = await _context.Users
            .AsNoTracking()
            .Include(x => x.Roles)
            .ThenInclude(r=>r.Permissions)
            .Where(u => u.Id == userId)
            .Select(u => u.Roles)
            .ToArrayAsync();
        
        return roles
            .SelectMany(r=>r)
            .SelectMany(r=>r.Permissions)
            .Select(p=>(Permission)p.Id)
            .ToHashSet();
    }
}