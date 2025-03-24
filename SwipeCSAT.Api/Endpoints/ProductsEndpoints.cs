using Microsoft.AspNetCore.JsonPatch;
using Newtonsoft.Json;
using SwipeCSAT.Api.Dtos;
using SwipeCSAT.Api.Entities;
using SwipeCSAT.Api.Mapping;
using SwipeCSAT.Api.Repositories;

namespace SwipeCSAT.Api.Endpoints;

//Продукты
public static class ProductsEndpoints
{
    public static RouteGroupBuilder MapProductsEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("api/products");

        group.MapGet("/", async (ProductRepository repository) =>
        {
            var products = await repository.GetAllProducts();
            return Results.Ok(products.Select(x => x.ToDto()).ToList());
        });

        group.MapGet("/{name}", async (string name, ProductRepository repository) =>
        {
            var product = await repository.GetProductByName(name);
            return Results.Ok(product.ToDto());
        }).WithName("GetProductByName");

        group.MapGet("/category/{categoryName}", async (string categoryName, ProductRepository repository) =>
        {
            var products = await repository.GetProductsWithCategory(categoryName);
            return Results.Ok(products.Select(x => x.ToDto()).ToList());
        });

        group.MapPost("/", async (CreateProductDto createProductDto, ProductRepository productRepository) =>
        {
            var productEntity = await productRepository.AddProduct(createProductDto.Name, createProductDto.CategoryName,
                createProductDto.Description);
            return Results.CreatedAtRoute("GetProductByName", new { name = productEntity.Name }, productEntity.ToDto());
        });

        group.MapDelete("/{name}", async (string name, ProductRepository productRepository) =>
        {
            await productRepository.DeleteProduct(name);
            return Results.NoContent();
        });

        group.MapPatch("/{name}",
            async (string name, HttpRequest request, ProductRepository productRepository, SwipeCsatDbContext context) =>
            {
                var product = await productRepository.GetShortProductByName(name);


                using var reader = new StreamReader(request.Body);
                var body = await reader.ReadToEndAsync();
                var patchCategory = JsonConvert.DeserializeObject<JsonPatchDocument<ProductEntity>>(body);
                patchCategory!.ApplyTo(product);
                context.Products.Update(product);


                await context.SaveChangesAsync();
                return Results.Ok("Данные обновлены");
            });


        return group;
    }
}