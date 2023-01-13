namespace WebAPI.Features.CurrentGames;

public sealed class Request
{
    public int ItemsCount { get; set; }
    public int PageNumber { get; set; }
}