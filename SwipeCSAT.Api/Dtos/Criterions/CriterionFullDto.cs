namespace SwipeCSAT.Api.Dtos.Criterions;

public record class CriterionFullDto(
    string Name,
    List<string> CategoriesNames
);