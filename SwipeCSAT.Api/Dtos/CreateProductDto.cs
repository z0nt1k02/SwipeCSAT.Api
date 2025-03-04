using System.ComponentModel.DataAnnotations;

namespace SwipeCSAT.Api.Dtos
{
    public record class CreateProductDto(
        [Required]int Id,
        [Required]int CategoryId,
        [Required]string Name   
    );
    
}
