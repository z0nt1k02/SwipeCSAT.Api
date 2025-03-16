using SwipeCSAT.Api.Dtos;
using SwipeCSAT.Api.Entities;

namespace SwipeCSAT.Api.Mapping
{
    public static class ProductMapping
    {
        public static ProductDto ToDto(this ProductEntity product)
        {
            //var criterions = product.Criterions;
            //List<CriterionShortDto> criterionsDto = new();
            //foreach (var criterion in criterions)
            //{
            //    criterionsDto.Add(criterion.ToShortDto());
            //}
            return new ProductDto
            (
                product.Id,
                product.Name,
                product.Criterions.Select(x=> x.Name).ToList(),
                 product.Category!.Name
            );
        }

        
    }
}
