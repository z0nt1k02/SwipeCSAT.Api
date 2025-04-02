using Microsoft.AspNetCore.JsonPatch;
using Newtonsoft.Json;
using SwipeCSAT.Api.Dtos;
using SwipeCSAT.Api.Entities;
using SwipeCSAT.Api.Enums;
using SwipeCSAT.Api.Extensions;
using SwipeCSAT.Api.Mapping;
using SwipeCSAT.Api.Repositories;

namespace SwipeCSAT.Api.Endpoints;

public static class CategoriesEndpoints
{
    public static RouteGroupBuilder MapCategoriesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("api/categories").RequireAuthorization();

        group.MapGet("/", async (CategoryRepository repository) =>
        {
            var categories = await repository.GetAllCategories();
            return Results.Ok(categories.Select(x => x.ToDto()).ToList());
        }).RequirePermissions(Permission.Read);


        group.MapGet("/{name}", async (string name, CategoryRepository repository) =>
        {
            var category = await repository.GetByName(name);
            return Results.Ok(category.ToDto());
        }).WithName("GetCategoryByName").RequirePermissions(Permission.Read);


        group.MapPost("/", async (CreateCategoryDto createCategoryDto, CategoryRepository repository) =>
        {
            var category = await repository.Add(createCategoryDto.Name, createCategoryDto.CriterionsNames);
            return Results.CreatedAtRoute("GetCategoryByName", new { name = category.Name }, category);
        }).RequirePermissions(Permission.Create);


        group.MapDelete("/{name}", async (string name, CategoryRepository repository) =>
        {
            await repository.DeleteCategory(name);
            return Results.NoContent();
        }).RequirePermissions(Permission.Delete);


        group.MapPatch("/{name}",
            async (string name, HttpRequest request, CategoryRepository categoryRepository,
                SwipeCsatDbContext context) =>
            {
                var category = await categoryRepository.GetByName(name);


                using var reader = new StreamReader(request.Body);
                var body = await reader.ReadToEndAsync();
                var patchCategory = JsonConvert.DeserializeObject<JsonPatchDocument<CategoryEntity>>(body);
                patchCategory!.ApplyTo(category);
                context.Categories.Update(category);
                await context.SaveChangesAsync();
                return Results.Ok("Данные обновлены");
            }).RequirePermissions(Permission.Update);

        return group;
    }
}