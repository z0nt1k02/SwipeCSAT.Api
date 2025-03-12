namespace SwipeCSAT.Api.Dtos
{
    public record class CategoryDto(
        Guid Id,
        string Name,
        List<string> CriterionsNames,
        List<ProductDto> Products
    );
    
}
