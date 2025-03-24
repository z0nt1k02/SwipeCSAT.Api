using Microsoft.EntityFrameworkCore;
using SwipeCSAT.Api.Mapping;

namespace SwipeCSAT.Api.Endpoints;

public static class CriterionRatingEndpoints
{
    public static RouteGroupBuilder MapCriterionRatingEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("api/ratings");

        group.MapGet("/", (SwipeCsatDbContext context) =>
        {
            var ratings = context.CriterionRatings.Include(x => x.Review).ToList();
            return Results.Ok(ratings.Select(x => x.ToDto()).ToList());
        });

        return group;
    }
}