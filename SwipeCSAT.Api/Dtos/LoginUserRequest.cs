using System.ComponentModel.DataAnnotations;

namespace SwipeCSAT.Api.Dtos;

public record LoginUserRequest(
    [Required] string Email,
    [Required] string Password);