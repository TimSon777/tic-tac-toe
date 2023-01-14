using WebAPI.Features.Rating.Models;

namespace WebAPI.Features.Rating;

public sealed class Response
{
    public required IEnumerable<UserRating> UserRatings { get; set; }
}