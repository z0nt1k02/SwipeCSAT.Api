using SwipeCSAT.Api.Dtos;
using SwipeCSAT.Api.Dtos.Criterions;
using SwipeCSAT.Api.Entities;

namespace SwipeCSAT.Api.Mapping;

public static class CriterionsMapping
{
    public static CriterionShortDto ToShortDto(this CriterionEntity criterionEntity)
    {
        //var categories = criterionEntity.Categories.Select(x=>x.Name).ToList();

        return new CriterionShortDto
        (
            criterionEntity.Name
            //categories
        );
    }

    public static CriterionFullDto ToFullDto(this CriterionEntity criterionEntity)
    {
        var categories = criterionEntity.Categories.Select(x => x.Name).ToList();

        return new CriterionFullDto(
            criterionEntity.Name,
            categories
        );
    }

    public static CriterionRatingDto ToDto(this CriterionRatingEntity criterionRatingEntity)
    {
        return new CriterionRatingDto(criterionRatingEntity.CriterionName, criterionRatingEntity.Rating);
    }
}