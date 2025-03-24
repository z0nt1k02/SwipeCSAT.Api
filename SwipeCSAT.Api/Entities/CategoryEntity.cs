namespace SwipeCSAT.Api.Entities;

public class CategoryEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<ProductEntity> Products { get; set; } = [];
    public List<CriterionEntity> Criterions { get; set; } = [];
}