using System.ComponentModel.DataAnnotations;

namespace SwipeCSAT.Api.Dtos
{
    public record class CreateCategoryDto
    (
        [Required]int Id,
        [Required]int CategoryId,
        [Required,StringLength(30)]string Name,
        List<string> Properties
    );
}
