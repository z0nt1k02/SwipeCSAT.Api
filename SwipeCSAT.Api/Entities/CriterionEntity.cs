namespace SwipeCSAT.Api.Entities
{
    public class CriterionEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public List<CategoryEntity> Categories { get; set; } = [];
        public List<ProductEntity> Products { get; set; } = [];

    }
}
