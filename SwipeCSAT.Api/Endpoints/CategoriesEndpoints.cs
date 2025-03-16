using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using SwipeCSAT.Api.Dtos;
using SwipeCSAT.Api.Entities;

using SwipeCSAT.Api.Repositories;
using Microsoft.AspNetCore.Http;
using SwipeCSAT.Api.Mapping;

namespace SwipeCSAT.Api.Endpoints
{
    public static class CategoriesEndpoints
    {
        public static RouteGroupBuilder MapCategoriesEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("api/categories");

            group.MapGet("/", async (CategoryRepository repository) =>
            {
                var categories = await repository.GetAllCategories();
                return Results.Ok(categories.Select(x=> x.ToDto()).ToList());
            });


            group.MapGet("/{name}", async (string name, CategoryRepository repository) =>
            {
                var category = await repository.GetCategoryByName(name);
                return Results.Ok(category.ToDto());
            }).WithName("GetCategoryByName");


            group.MapPost("/", async (CreateCategoryDto createCategoryDto, CategoryRepository repository) =>
            {
                var category = await repository.Add(createCategoryDto.Name, createCategoryDto.CriterionsNames);
                return Results.CreatedAtRoute("GetCategoryByName", new { name = category.Name }, category);
            });


            group.MapDelete("/{name}", async (string name, CategoryRepository repository) =>
            {
                await repository.DeleteCategory(name);
                return Results.NoContent();
            });

            //group.MapPatch("/{name}", async (string name, UpdatedCategoryDto updatedCategory, CategoryRepository categoryRepository) =>
            //{
            //    await categoryRepository.UpdateCategory(name, updatedCategory.NewName);
            //});

            return group;
        }

    }
}
