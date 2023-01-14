using Application.Queries.Rating.Objects;

namespace Application.Queries.Rating;

public sealed class RatingQueryResult
{
    public required IEnumerable<UserRatingObject> UserRatings { get; set; }
}