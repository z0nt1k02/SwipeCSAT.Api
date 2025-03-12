namespace SwipeCSAT.Api.Entities
{
    public class CriterionRatingEntity
    {
        public ProductEntity? Product { get; set; }
        public Guid ProductId { get; set; }
        public string? CriterionName { get; set; }
        public int Rating { get; set; }

    }
}
