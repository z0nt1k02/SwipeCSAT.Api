using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SwipeCSAT.Api.Dtos;

namespace SwipeCSAT.Api.Dtos
{
    public record class ProductDto(
        int Id,
        string Name,
        Dictionary<string,int> Properties,
        int CategoryId
    );
    
}
