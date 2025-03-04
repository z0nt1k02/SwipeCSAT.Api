namespace SwipeCSAT.Api.Dtos
{
    public record class CategoryDto(
        int Id,
        string Name,
        List<string> Properties
    );
    
}
