namespace WebAPI.Features.Rating.Models;

public sealed class UserRating
{
    public required string UserName { get; set; } = default!;
    public required int Rating { get; set; }
}