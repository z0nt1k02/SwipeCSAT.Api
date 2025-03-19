namespace SwipeCSAT.Api.Entities
{
    public class CriterionRatingEntity
    {
        public Guid Id { get; set; }

        public string CriterionName { get; set; } = string.Empty;
        
        public int Rating { get; set; } =0;

        public ReviewEntity? Review { get; set; }
        

    }
}
