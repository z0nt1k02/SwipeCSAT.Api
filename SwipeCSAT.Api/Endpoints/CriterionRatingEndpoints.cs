using Microsoft.EntityFrameworkCore;
using SwipeCSAT.Api.Mapping;
using SwipeCSAT.Api.Repositories;

namespace SwipeCSAT.Api.Endpoints
{
    public static class CriterionRatingEndpoints
    {
        public static RouteGroupBuilder MapCriterionRatingEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("api/ratings");

            group.MapGet("/",(SwipeCSATDbContext context) =>
            {
                var ratings = context.CriterionRatings.Include(x => x.Review).ToList();
                return Results.Ok(ratings.Select(x => x.ToDto()).ToList());
            });

            return group;
        }
    }
}
