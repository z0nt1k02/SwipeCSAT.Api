namespace SwipeCSAT.Api.Dtos;

public record class RegisterUserRequest(
    string UserName,
    string Email,
    string Password
);