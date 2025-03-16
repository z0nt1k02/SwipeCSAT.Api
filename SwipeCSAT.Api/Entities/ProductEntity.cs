namespace SwipeCSAT.Api.Entities
{
    public class ProductEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<CriterionEntity> Criterions { get; set; } = [];
        //public Guid CategoryId { get; set; }
        public CategoryEntity? Category { get; set; }

    }
}
