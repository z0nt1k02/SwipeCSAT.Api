using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using SwipeCSAT.Api.Dtos;
using SwipeCSAT.Api.Entities;
using SwipeCSAT.Api.Mapping;
using SwipeCSAT.Api.Repositories;

namespace SwipeCSAT.Api.Endpoints
{
    public static class ReviewsEndpoints
    {
        public static RouteGroupBuilder MapReviewsEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("api/reviews");

            group.MapGet("/", async (ReviewsRepository repository) =>
            {
                var reviews = await repository.GetAll();
                return Results.Ok(reviews.Select(x=>x.ToDto()));
            });


            group.MapPost("/{productName}", async (SwipeCSATDbContext context,string productName,CreateReviewDto createReviewDto, ReviewsRepository repository) =>
            {
                var product = await context.Products.Include(x=>x.Criterions).AsNoTracking().FirstOrDefaultAsync(x => x.Name == productName)
                    ?? throw new Exception("Данный продукт не найден");
                var criterions = product.Criterions.Select(x => x.Name).ToList();


                var review = await repository.Add(productName, createReviewDto.ratings);
                return Results.Ok(createReviewDto.ratings.Count);

            });


            //group.MapDelete("/{name}", async (string name, CategoryRepository repository) =>
            //{
            //    await repository.DeleteCategory(name);
            //    return Results.NoContent();
            //});

            //group.MapPatch("/{name}", async (string name, UpdatedCategoryDto updatedCategory, CategoryRepository categoryRepository) =>
            //{
            //    await categoryRepository.UpdateCategory(name, updatedCategory.NewName);
            //});

            return group;
        }
    }
}
