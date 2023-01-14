namespace Application.Queries.Rating.Objects;

public sealed class UserRatingObject
{
    public required string UserName { get; set; }
    public required int Rating { get; set; }
}