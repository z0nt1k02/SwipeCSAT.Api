using System.ComponentModel.DataAnnotations;
using SwipeCSAT.Api.Entities;

namespace SwipeCSAT.Api.Dtos;

public record class RegisterUserRequest(
    string UserName,
    [EmailAddress]string Email,
    [StringLength(50,MinimumLength = 7)]string Password);