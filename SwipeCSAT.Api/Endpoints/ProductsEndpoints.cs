using SwipeCSAT.Api.Dtos;
using SwipeCSAT.Api.Mapping;

namespace SwipeCSAT.Api.Endpoints
{
    public static class ProductsEndpoints
    {
        public static List<ProductDto> Products = new List<ProductDto>();
        public static RouteGroupBuilder MapProductsEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("api/products");

            group.MapGet("/", () =>
            {
                return Results.Ok(Products);
            });

            group.MapPost("/", (CreateProductDto createProductDto) =>
            {
                ProductDto NewProduct = createProductDto.ToDto();
                Products.Add(NewProduct);
                return Results.Created($"/api/products/{NewProduct.Id}", NewProduct);
            });
            group.MapPost("{id}/properties/{propertyName}",(int id,string propertyName,int raiting)=>
            {
                ProductDto product = Products.Find(x => x.Id == id);
                if (product == null)
                {
                    return Results.NotFound();
                }
                if (!product.Properties.ContainsKey(propertyName))
                {
                    return Results.NotFound();
                }
                product.Properties[propertyName] = raiting;
                return Results.Ok(product);

            });

            return group;
        }
    }
}
