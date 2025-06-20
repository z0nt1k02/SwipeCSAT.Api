﻿namespace SwipeCSAT.Api.Entities;

public class ReviewEntity
{
    public Guid Id { get; set; }
    public List<CriterionRatingEntity> CriterionRatings { get; set; } = [];
    public ProductEntity? ProductEntity { get; set; }
    
    public Guid UserId { get; set; }
}