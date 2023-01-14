namespace Application.Queries.Rating;

public sealed class RatingQuery : IQuery<RatingQueryResult>
{
    public required int ItemsNumber { get; set; }
}