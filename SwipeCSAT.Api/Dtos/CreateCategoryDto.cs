using System.ComponentModel.DataAnnotations;

namespace SwipeCSAT.Api.Dtos
{
    public record class CreateCategoryDto
    (
        [Required,StringLength(30)] string Name,
        [Required] List<string> CriterionsNames
    );
    
}
