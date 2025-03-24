using System.ComponentModel.DataAnnotations;

namespace SwipeCSAT.Api.Dtos;

public record class CreateProductDto(
    [Required] string Name,
    [Required] string CategoryName,
    [Required] string Description
);