using SwipeCSAT.Api.Dtos;
using SwipeCSAT.Api.Entities;

namespace SwipeCSAT.Api.Mapping;

public static class CategoriesMapping
{
    public static FullCategoryDto ToDto(this CategoryEntity entity)
    {
        return new FullCategoryDto
        (
            entity.Id,
            entity.Name,
            entity.Criterions.Select(x => x.Name).ToList(),
            entity.Products.Select(x => x.Name).ToList()
        );
    }
}