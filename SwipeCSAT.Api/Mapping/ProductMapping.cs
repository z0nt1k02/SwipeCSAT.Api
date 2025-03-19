using SwipeCSAT.Api.Dtos;
using SwipeCSAT.Api.Entities;

namespace SwipeCSAT.Api.Mapping
{
    public static class ProductMapping
    {
        public static ProductDto ToDto(this ProductEntity product)
        {
            return new ProductDto
            (
                product.Id,
                product.Name,
                product.Criterions.Select(x=> x.Name).ToList(),
                product.Category!.Name,
                product.Reviews.Select(x=>x.Id).ToList()
            );
        }

        
    }
}
