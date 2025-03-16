namespace SwipeCSAT.Api.Dtos
{
    public record class ProductDto(
        Guid Id,
        string Name,
        List<string> Criterions,
        string CategoryName
    );
    
}
