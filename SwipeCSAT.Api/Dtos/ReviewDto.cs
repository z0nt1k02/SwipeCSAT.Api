namespace SwipeCSAT.Api.Dtos;

public record class ReviewDto(
    Guid Id,
    Dictionary<string, int> Data,
    string ProductName
);