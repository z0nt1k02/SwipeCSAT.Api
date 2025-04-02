using SwipeCSAT.Api.Dtos;
using SwipeCSAT.Api.Entities;

namespace SwipeCSAT.Api.Mapping;

public static class ReviewMapping
{
    public static ReviewDto ToDto(this ReviewEntity reviewEntity)
    {
        Dictionary<string, int> criterionRatings = new();

        foreach (var criterionRating in reviewEntity.CriterionRatings)
            criterionRatings.Add(criterionRating.CriterionName, criterionRating.Rating);
        return new ReviewDto(reviewEntity.Id, criterionRatings, reviewEntity.ProductEntity!.Name,reviewEntity.UserId.ToString());
    }
}