
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SwipeCSAT.Api.Authorization;
using SwipeCSAT.Api.Dtos;
using SwipeCSAT.Api.Entities;
using SwipeCSAT.Api.Enums;
using SwipeCSAT.Api.Extensions;
using SwipeCSAT.Api.Mapping;
using SwipeCSAT.Api.Repositories;

namespace SwipeCSAT.Api.Endpoints;

public static class ReviewsEndpoints
{
    public static RouteGroupBuilder MapReviewsEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("api/reviews");

        group.MapGet("/", async (ReviewsRepository repository) =>
        {
            var reviews = await repository.GetAll();
            return Results.Ok(reviews.Select(x => x.ToDto()));
        }).RequirePermissions(Permission.Read);
        group.MapGet("/userId", (HttpContext httpContext) =>
        {
            var userId = httpContext.User.FindFirst(CustomClaims.userId)?.Value;
            return Results.Ok(userId);
        }).RequirePermissions(Permission.Create);

        group.MapPost("/{productName}",
            async (SwipeCsatDbContext context, string productName, CreateReviewDto createReviewDto,
                ReviewsRepository repository,HttpContext httpContext) =>
            {
                var userId = httpContext.User.FindFirst(CustomClaims.userId)?.Value;
                var product = await context.Products.Include(x => x.Criterions).AsNoTracking()
                                  .FirstOrDefaultAsync(x => x.Name == productName)
                              ?? throw new Exception("Данный продукт не найден");
                var criterions = product.Criterions.Select(x => x.Name).ToList();


                var review = await repository.Add(productName, createReviewDto.ratings,Guid.Parse(userId!));
                return Results.Ok(createReviewDto.ratings.Count);
            }).RequirePermissions(Permission.CreateReview);


        group.MapDelete("/{name}", async (string name, CategoryRepository repository) =>
        {
            await repository.DeleteCategory(name);
            return Results.NoContent();
        });

        group.MapPatch("/{reviewId}", async (string reviewId,HttpContext httpContext,ReviewsRepository repository,SwipeCsatDbContext context ) =>
        {
            var review = await repository.GetById(reviewId);
            if (review.UserId != Guid.Parse(httpContext.User.FindFirst(CustomClaims.userId)!.Value))
            {
                return Results.Forbid();
            }
            using var reader = new StreamReader(httpContext.Request.Body);
            var body = await reader.ReadToEndAsync();
            var patchReview = JsonConvert.DeserializeObject<JsonPatchDocument<ReviewEntity>>(body);
            patchReview!.ApplyTo(review);
            context.Reviews.Update(review);
            await context.SaveChangesAsync();
            return Results.Ok("Отзыв обновлен");
            
            
            
        });

        return group;
    }
}