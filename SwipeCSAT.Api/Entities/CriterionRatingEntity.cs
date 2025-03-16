namespace SwipeCSAT.Api.Entities
{
    public class CriterionRatingEntity
    {
        public Guid Id { get; set; }
        public ProductEntity? Product { get; set; }
        //public Guid ProductId { get; set; }
        public CriterionEntity? Criterion { get; set; }
        //public Guid CriterionId { get; set; }
        public int Rating { get; set; } =0;

    }
}
