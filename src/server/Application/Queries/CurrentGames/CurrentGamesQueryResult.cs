using Application.Queries.CurrentGames.Models;

namespace Application.Queries.CurrentGames;

public sealed class CurrentGamesQueryResult
{
    public required IEnumerable<GameItem> Games { get; set; } = default!;
    public required bool HasMore { get; set; }
    public required int TotalCount { get; set; }
}