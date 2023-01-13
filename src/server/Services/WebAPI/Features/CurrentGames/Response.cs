using WebAPI.Features.CurrentGames.Models;

namespace WebAPI.Features.CurrentGames;

public sealed class Response
{
    public required IEnumerable<GameItem> Games { get; set; }
    public required bool HasMore { get; set; }
    public required int TotalCount { get; set; }
}