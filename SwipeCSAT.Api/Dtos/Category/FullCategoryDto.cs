namespace SwipeCSAT.Api.Dtos;

public record class FullCategoryDto(
    Guid Id,
    string Name,
    List<string> CriterionsNames,
    List<string> Products
);