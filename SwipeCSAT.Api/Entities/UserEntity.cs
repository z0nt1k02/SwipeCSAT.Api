namespace SwipeCSAT.Api.Entities;

public class UserEntity
{
    private UserEntity(Guid id, string userName, string passwordHash, string email)
    {
        Id = id;
        UserName = userName;
        PasswordHash = passwordHash;
        Email = email;
    }

    public Guid Id { get; set; }
    public string UserName { get; private set; }
    public string PasswordHash { get; private set; }

    public string Email { get; private set; }

    public ICollection<RoleEntity> Roles { get; set; } = [];
    public static UserEntity Create(Guid id, string username, string passwordHash, string email)
    {
        return new UserEntity(id, username, passwordHash, email);
    }
}